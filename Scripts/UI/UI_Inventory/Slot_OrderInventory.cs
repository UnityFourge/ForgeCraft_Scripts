using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_OrderInventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image image;

    private GameObject button_Complete;
    private ItemSO itemData;

    public Modal_Order Modal { get; private set; }
    public UI_OrderInventory OrderInventory { get; private set; }

    [SerializeField] private GameObject slot_Selected;
    public GameObject Slot_Selected 
    { 
        get 
        { 
            return slot_Selected; 
        }
    }

    private Inventory.InventoryItem item;
    public Inventory.InventoryItem Item
    {
        get { return item; }
        set
        {
            item = value;
            SetItem();
        }
    }

    private void SetItem()
    {
        if (item == null || DataManager.Instance == null)
        {
            ClearSlot();
            return;
        }

        itemData = DataManager.Instance.GetItem(item.itemID);
        if (itemData == null || itemData.itemType != Enums.ItemType.Weapon)
        {
            ClearSlot();
            return;
        }

        image.color = new Color(1, 1, 1, 1);
        image.sprite = itemData.itemSprite;
    }

    public void SetModal(Modal_Order modal)
    {
        if (modal == null) return;

        this.Modal = modal; 
    }

    public void SetEquipInventory(UI_OrderInventory ui_OrderInventory)
    {
        if (ui_OrderInventory == null) return;

        OrderInventory = ui_OrderInventory;
        button_Complete = ui_OrderInventory.Button_Complete;
    }

    private void ActiveModal()
    {
        if (item == null) return;

        Modal.gameObject.SetActive(true);
        itemData = DataManager.Instance.GetItem(item.itemID);

        float[] status = new float[itemData.itemStatusLen];
        int rank = item.itemRank;
        status[0] = itemData.itemEnchantHP[rank];
        status[1] = itemData.itemEnchantAttack[rank];
        status[2] = itemData.itemEnchantAttackDelay[rank];
        status[3] = itemData.itemEnchantDefence[rank];
        status[4] = itemData.itemEnchantAttackRange[rank];

        Modal.SetModal(itemData.itemSprite, rank, itemData.itemName, itemData.itemDescription, status);
    }

    private void ClearSlot()
    {
        image.color = new Color(1, 1, 1, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ActiveModal();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Modal.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item == null) return;

        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        Modal.gameObject.SetActive(false);

        OrderInventory.InitSelect();
        Slot_Selected.SetActive(true);

        int orderID = NPCManager.Instance.Npc.weaponId;

        if (item.itemID == orderID)
        {
            button_Complete.SetActive(true);
        }
        else
        {
            button_Complete.SetActive(false);
        }
    }
}
