using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Inventory;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private Transform slotParent;
    [SerializeField] private Slot[] slots;
    [SerializeField] private Slot slotPrefab;
    [SerializeField] private Modal_Inventory modal_Inventory;

    private int invenLength;
    private List<Slot> pool;

    private void Awake()
    {
        pool = new List<Slot>();
    }

    private void Init()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].gameObject.SetActive(false);
        }

        invenLength = Player.Instance.inventory.bag.items.Count;
        slots = new Slot[invenLength];

        for (int i = 0; i < invenLength; i++)
        {
            slots[i] = Get();
            slots[i].Modal = modal_Inventory;
        }
    }

    private Slot Get()
    {
        Slot select = null;

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
            select = Instantiate(slotPrefab, slotParent);
            pool.Add(select);
        }

        return select;
    }

    public void FreshSlot()
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
        if (slots == null)
        {
            Debug.LogError("slots is null");
            return;
        }

        Init();
        for (int i = 0; i < Player.Instance.inventory.bag.items.Count; i++)
        {
            slots[i].Item = Player.Instance.inventory.bag.items[i];
        }
    }
}
