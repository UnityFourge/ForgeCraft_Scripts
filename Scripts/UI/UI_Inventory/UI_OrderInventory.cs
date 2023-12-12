using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;

public class UI_OrderInventory : MonoBehaviour
{
    [SerializeField] private Slot_OrderInventory slotPrefab;
    [SerializeField] private Transform slotParent_Order;
    [SerializeField] private Modal_Order modal;

    private Slot_OrderInventory[] slots_OrderInventory;

    private List<InventoryItem> orderItems;

    private List<Slot_OrderInventory> pool;

    [SerializeField] private GameObject button_Complete;
    public GameObject Button_Complete
    {
        get
        { 
            return button_Complete; 
        }
    }

    private void Awake()
    {
        slots_OrderInventory = slotParent_Order.GetComponentsInChildren<Slot_OrderInventory>();

        pool = new List<Slot_OrderInventory>();
    }

    private void Init()
    {
        for (int i = 0; i < slots_OrderInventory.Length; i++)
        {
            slots_OrderInventory[i].gameObject.SetActive(false);
        }

        orderItems = Player.Instance.inventory.GetEquipItems(Enums.ItemType.Weapon);
        slots_OrderInventory = new Slot_OrderInventory[orderItems.Count];

        for (int i = 0; i < orderItems.Count; i++)
        {
            slots_OrderInventory[i] = Get();
            slots_OrderInventory[i].SetModal(modal);
            slots_OrderInventory[i].SetEquipInventory(this);
        }
    }

    private Slot_OrderInventory Get()
    {
        Slot_OrderInventory select = null;

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
            select = Instantiate(slotPrefab, slotParent_Order);
            pool.Add(select);
        }

        return select;
    }

    public void FreshOrderSlot()
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
        if (slots_OrderInventory == null)
        {
            Debug.LogError("equipSlots is null");
            return;
        }

        Init();
        InitSelect();

        for (int i = 0; i < orderItems.Count; i++)
        {
            slots_OrderInventory[i].Item = orderItems[i];
        }
    }

    public void InitSelect()
    {
        for (int i = 0; i < orderItems.Count; i++)
        {
            slots_OrderInventory[i].Slot_Selected.SetActive(false);
        }
    }
}
