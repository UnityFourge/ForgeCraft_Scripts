using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Inventory;

public class UI_EquipInventory : MonoBehaviour
{
    [SerializeField] private Slot_EquipInventory slotPrefab;
    [SerializeField] private Transform slotParent_Equip;
    private Slot_EquipInventory[] slots_EquipInventory;
    public float[] status_Equip;

    public static ItemSO selectedItem;

    [SerializeField] private Modal_Equip modal;
    [SerializeField] private Slot_Equip[] slot_Equips;

    private List<InventoryItem> equipItems;

    List<Slot_EquipInventory> pool;

    private void Awake()
    {
        slots_EquipInventory = slotParent_Equip.GetComponentsInChildren<Slot_EquipInventory>();

        pool = new List<Slot_EquipInventory>();
    }

    private void Init()
    {
        for (int i = 0; i < slots_EquipInventory.Length; i++)
        {
            slots_EquipInventory[i].gameObject.SetActive(false);
        }

        equipItems = Player.Instance.inventory.GetEquipItems(Enums.ItemType.Weapon);
        slots_EquipInventory = new Slot_EquipInventory[equipItems.Count];

        for (int i = 0; i < equipItems.Count; i++)
        {
            slots_EquipInventory[i] = Get();
            slots_EquipInventory[i].SetModal(modal);
            slots_EquipInventory[i].SetEquipInventory(this);
            slots_EquipInventory[i].SetSlotEquips(slot_Equips);
        }
    }

    private Slot_EquipInventory Get()
    {
        Slot_EquipInventory select = null;

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeSelf) 
            { 
                select = pool[i];
                select.gameObject.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(slotPrefab, slotParent_Equip);
            pool.Add(select);
        }

        return select;
    }

    public void FreshWeaponSlot()
    {
        if (Player.Instance == null)
        {
            Debug.LogError("Player.Instance is null");
            return;
        }
        if (Player.Instance.inventory == null)
        {
            Debug.LogError("Player.Instance.inventory is null");
            return;
        }
        if (Player.Instance.inventory.bag == null)
        {
            Debug.LogError("Player.Instance.inventory.bag is null");
            return;
        }
        if (slots_EquipInventory == null)
        {
            Debug.LogError("equipSlots is null");
            return;
        }

        Init();
        
        for (int i = 0; i < equipItems.Count; i++)
        {
            slots_EquipInventory[i].Item = equipItems[i];
            slots_EquipInventory[i].SetInteractableSlot(CheckEquipable(equipItems[i]));
        }
    }

    public ItemSO GetSelectedItem()
    {
        return selectedItem;
    }

    public void SelectItem(ItemSO item)
    {
        selectedItem = item;
    }

    public void InitStatusEquip(float[] status = null)
    {
        if (status == null) ResetStatusEquip();
        else
        {
            for (int i = 0; i < status_Equip.Length; i++)
            {
                status_Equip[i] += status[i];
            }
        }
    }

    private void ResetStatusEquip()
    {
        status_Equip = new float[5];

        for (int i = 0; i < status_Equip.Length; i++)
        {
            status_Equip[i] = 0;
        }
    }

    public bool CheckEquipable(InventoryItem equip)
    {
        if (equip == null) return false;

        int equipID = equip.itemID % 100;
        int characterId = Player.Instance.characterList.SelectID / 100;

        if (equipID == characterId) return true;

        return false;
    }
}
