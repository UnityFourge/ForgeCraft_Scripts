using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Slot_EquipInventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image image;
    private bool slotInteractable;

    public Modal_Equip Modal { get; private set; }
    public UI_EquipInventory EquipInventory { get; private set; }
    public Slot_Equip[] Slot_Equips { get; private set; }

    [SerializeField] private GameObject slot_Selected;
    public GameObject Slot_Selected
    {
        get
        {
            return slot_Selected;
        }
    }

    private ItemSO itemData;

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
        slotInteractable = true;
    }

    public void SetModal(Modal_Equip modal)
    {
        if (modal == null) return;

        Modal = modal;
    }

    public void SetEquipInventory(UI_EquipInventory uI_EquipInventory)
    {
        if (uI_EquipInventory == null) return;

        EquipInventory = uI_EquipInventory;
    }

    public void SetSlotEquips(Slot_Equip[] equip)
    {
        if (equip == null) return;

        Slot_Equips = equip;
    }

    private void ActiveModal()
    {
        if (item == null) return;

        Modal.gameObject.SetActive(true);
        Modal.transform.localPosition = new Vector2(-160, 0);
        itemData = DataManager.Instance.GetItem(item.itemID);

        float[] status = new float[itemData.itemStatusLen];
        int rank = item.itemRank;
        status[0] = itemData.itemEnchantHP[rank];
        status[1] = itemData.itemEnchantAttack[rank];
        status[2] = itemData.itemEnchantAttackDelay[rank];
        status[3] = itemData.itemEnchantDefence[rank];
        status[4] = itemData.itemEnchantAttackRange[rank];

        if (itemData.itemType == Enums.ItemType.Weapon)
        {
            Modal.SetModal(itemData.itemSprite, rank, itemData.itemName, EquipInventory.CheckEquipable(item), itemData.itemDescription, status);
            EquipInventory.SelectItem(itemData);
        }
    }

    private void ClearSlot()
    {
        image.color = new Color(1, 1, 1, 0);
        slotInteractable = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ActiveModal();
        Slot_Selected.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Modal.gameObject.SetActive(false);
        Slot_Selected.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item == null) return;

        if (eventData.button == PointerEventData.InputButton.Right && slotInteractable)
        {
            SoundManager.Instance.SfxPlay(Enums.SFX.Button);

            int index = Player.Instance.characterList.SelectIndexCharacters;
            if (index >= 0) 
            {
                Player.Instance.characterList.roster.characters[index].SetEquipID(item.itemType, item.itemID, item.itemRank);

                switch (item.itemType)
                {
                    case Enums.ItemType.Weapon:
                        Slot_Equips[0].SetItemEquip(item);
                        break;
                    default:
                        break;
                }

                Slot_Selected.SetActive(false);

                Player.Instance.inventory.SubItem(item);
                EquipInventory.FreshWeaponSlot();

                Modal.gameObject.SetActive(false);
            }
        }
    }

    public void SetInteractableSlot(bool flag)
    {
        slotInteractable = flag;
    }

}
