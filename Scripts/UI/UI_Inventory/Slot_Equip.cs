using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.EventSystems;

public class Slot_Equip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] public Image image_Equip;
    [SerializeField] private TextMeshProUGUI[] text_EquipStatus;
    [SerializeField] private UI_EquipInventory equipInventory;
    [SerializeField] private Modal_Equip modal;

    ItemSO itemSO;

    public Inventory.InventoryItem Item_Equip { get; private set; }
    [SerializeField] private GameObject slot_Selected;
    public GameObject Slot_Selected
    {
        get
        {
            return slot_Selected;
        }
    }

    public void SetItemEquip(Inventory.InventoryItem item)
    {
        if (image_Equip == null || item == null) return;

        if (Item_Equip == null)
        {
            Item_Equip = item;
            itemSO = DataManager.Instance.GetItem(Item_Equip.itemID);
            image_Equip.sprite = itemSO.itemSprite;
            image_Equip.color = new Color(1, 1, 1, 1);
            SetEquipStatus(itemSO, Item_Equip.itemRank);
        }
        else
        {
            itemSO = DataManager.Instance.GetItem(Item_Equip.itemID);
            SetEquipStatus(itemSO, Item_Equip.itemRank, false);
            Player.Instance.inventory.AddItem(itemSO);

            Item_Equip = item;
            itemSO = DataManager.Instance.GetItem(Item_Equip.itemID);
            image_Equip.sprite = itemSO.itemSprite;
            image_Equip.color = new Color(1, 1, 1, 1);
            SetEquipStatus(itemSO, Item_Equip.itemRank);
        }
    }

    public void SetItemEquip(ItemSO _itemSO)
    {
        if (image_Equip == null) return;
        if (_itemSO == null)
        {
            image_Equip.color = new Color(1, 1, 1, 0);
            Item_Equip = null;
            SetEquipStatus();
            return;
        }

        itemSO = _itemSO;
        Item_Equip = new Inventory.InventoryItem(itemSO.itemID, itemSO.itemType);

        image_Equip.sprite = this.itemSO.itemSprite;
        image_Equip.color = new Color(1, 1, 1, 1);
        SetEquipStatus();
    }

    public void InitItemEquip(ItemSO _itemSO)
    {
        if (image_Equip == null) return;
        if (_itemSO == null)
        {
            image_Equip.color = new Color(1, 1, 1, 0);
            SetEquipStatus();
            return;
        }

        this.itemSO = _itemSO;

        image_Equip.sprite = this.itemSO.itemSprite;
        image_Equip.color = new Color(1, 1, 1, 1);
        SetEquipStatus();
    }

    public void UnsetItemEquip()
    {
        if (image_Equip == null || Item_Equip == null) return;

        image_Equip.color = new Color(1, 1, 1, 0);
        SetEquipStatus(itemSO, Item_Equip.itemRank, false);
        Item_Equip = null;
        itemSO = null;
    }

    private void SetEquipStatus(ItemSO _itemSO = null, int _itemRank = -1, bool flag = true)
    {
        float[] equipStatus = equipInventory.status_Equip;

        if (_itemSO != null)
        {
            //float[] status = _itemSO.itemStatus;

            float[] status = new float[_itemSO.itemStatusLen];
            status[0] = _itemSO.itemEnchantHP[_itemRank];
            status[1] = _itemSO.itemEnchantAttack[_itemRank];
            status[2] = _itemSO.itemEnchantAttackDelay[_itemRank];
            status[3] = _itemSO.itemEnchantDefence[_itemRank];
            status[4] = _itemSO.itemEnchantAttackRange[_itemRank];

            if (flag)
            {
                for (int i = 0; i < equipStatus.Length; i++)
                {
                    equipStatus[i] += status[i];
                }
            }
            else
            {
                for (int i = 0; i < equipStatus.Length; i++)
                {
                    equipStatus[i] -= status[i];
                }
            }
        }

        SetTextEquipStatus(equipStatus);
    }

    private void SetTextEquipStatus(float[] equipStatus)
    {
        for (int i = 0; i < equipStatus.Length; i++)
        {
            if (equipStatus[i] > 0)
            {
                StringBuilder sb = new StringBuilder();

                text_EquipStatus[i].text = sb.Append("(+").Append(equipStatus[i]).Append(")").ToString();
            }
            else
            {
                text_EquipStatus[i].text = "";
            }
        }
    }

    private void ActiveModal()
    {
        if (Item_Equip == null) return;

        modal.gameObject.SetActive(true);
        modal.transform.localPosition = new Vector2(360, 0);
        itemSO = DataManager.Instance.GetItem(Item_Equip.itemID);

        float[] status = new float[itemSO.itemStatusLen];
        int rank = Item_Equip.itemRank;
        status[0] = itemSO.itemEnchantHP[rank];
        status[1] = itemSO.itemEnchantAttack[rank];
        status[2] = itemSO.itemEnchantAttackDelay[rank];
        status[3] = itemSO.itemEnchantDefence[rank];
        status[4] = itemSO.itemEnchantAttackRange[rank];

        if (itemSO.itemType == Enums.ItemType.Weapon)
        {
            modal.SetModal(itemSO.itemSprite, rank, itemSO.itemName, equipInventory.CheckEquipable(Item_Equip), itemSO.itemDescription, status);
            equipInventory.SelectItem(itemSO);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ActiveModal();
        Slot_Selected.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        modal.gameObject.SetActive(false);
        Slot_Selected.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Item_Equip == null) return;

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            SoundManager.Instance.SfxPlay(Enums.SFX.Button);

            int index = Player.Instance.characterList.SelectIndexCharacters;
            if (index >= 0)
            {
                Player.Instance.characterList.roster.characters[index].SetEquipID(Item_Equip.itemType, -1, -1);
                Player.Instance.inventory.AddWeapon(Item_Equip);
                UnsetItemEquip();
                equipInventory.FreshWeaponSlot();

                modal.gameObject.SetActive(false);
            }
        }
    }
}
