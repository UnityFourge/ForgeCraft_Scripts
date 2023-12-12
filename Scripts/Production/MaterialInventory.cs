using System.Collections.Generic;
using UnityEngine;

public class MaterialInventory : MonoBehaviour
{
    private const int InventorySlot = 5;
    [SerializeField] private Transform SlotParent;
    [SerializeField] private MaterialSlotUI UiSlotBase;


    public List<MaterialSlotUI> slots;
    private Dictionary<int, int> itemCounts = new Dictionary<int, int>();

    private List<ItemSO> materials = new List<ItemSO>();
    public int maxStackCount = 5;

    void Awake()
    {
        InventoryUI();
    }

    void InventoryUI()
    {
        slots = new List<MaterialSlotUI>();

        for (int i = 0; i < InventorySlot; i++)
        {
            MaterialSlotUI slot = Instantiate(UiSlotBase, SlotParent);
            slot.gameObject.SetActive(true);
            slots.Add(slot);
        }
    }
    public int GetMaterialCount(int materialID)
    {
        if (itemCounts.ContainsKey(materialID))
        {
            return itemCounts[materialID];
        }
        return 0;
    }

    public bool EnoughMaterial(int materialID, int requiredAmount)
    {
        return GetMaterialCount(materialID) == requiredAmount;
    }


    public bool AddMaterial(ItemSO materialItem)
    {
        if (materialItem == null || materialItem.itemType != Enums.ItemType.Material)
        {
            return false;
        }

        int itemID = materialItem.itemID;

        if (itemCounts.ContainsKey(itemID))
        {
            if (itemCounts[itemID] + 1 <= maxStackCount)
            {
                itemCounts[itemID]++;
                MaterialSlotUI slot = GetSlotByItemID(itemID);
                if (slot != null)
                {
                    slot.SetItem(materialItem, itemCounts[itemID]);
                }
            }
            else
            {
                ForgeManager.Instance.ShowErrorPopup("추가 실패","해당 아이템은 최대 스택 크기에 도달했습니다.", null);
                return false;
            }
        }
        else if (materials.Count < maxStackCount)
        {
            materials.Add(materialItem);
            itemCounts[itemID] = 1;
            MaterialSlotUI slot = GetEmptySlot();
            if (slot != null)
            {
                slot.SetItem(materialItem, itemCounts[itemID]);
            }
        }
        else
        {
            ForgeManager.Instance.ShowErrorPopup("추가 실패","재료 인벤토리가 가득 찼습니다.", null);
            return false;
        }

        return true;
    }

    MaterialSlotUI GetEmptySlot()
    {
        foreach (MaterialSlotUI slot in slots)
        {
            if (slot.IsEmpty())
            {
                return slot;
            }
        }
        return null;
    }

    MaterialSlotUI GetSlotByItemID(int itemID)
    {
        foreach (MaterialSlotUI slot in slots)
        {
            ItemSO item = slot.GetItem();
            if (item != null && item.itemID == itemID)
            {
                return slot;
            }
        }
        return null;
    }

    public void ClearInventory()
    {
        materials.Clear();
        itemCounts.Clear();

        foreach (MaterialSlotUI slot in slots)
        {
            slot.ClearItem();
        }
    }

    public void RemoveMaterial(int itemID) 
    {
        if (itemCounts.ContainsKey(itemID)) 
        {
            if (itemCounts[itemID] > 0) 
            {
                itemCounts[itemID]--;
            }
        }
    }

    public void ToPlayerInventory()
    {
        foreach (var item in materials) 
        {
            int itemID = item.itemID;

            if (itemCounts.ContainsKey(itemID) && itemCounts[itemID] > 0) 
            {
                int itemCountToAdd = itemCounts[itemID];

                for (int i = 0; i<itemCountToAdd; i++) 
                {
                    Player.Instance.inventory.AddItem(item);
                }

                itemCountToAdd -= itemCounts[itemID];
            }
        }
       
    }
   
}