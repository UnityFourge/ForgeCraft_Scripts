using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Xml;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textStack;

    public Modal_Inventory Modal { get; set; }

    ItemSO itemSO;

    private Inventory.InventoryItem item;
    public Inventory.InventoryItem Item
    {
        get { return item; }
        set
        {
            item = value;
            if (item != null)
            {
                itemSO = DataManager.Instance.GetItem(item.itemID);

                image.color = new Color(1, 1, 1, 1);
                image.sprite = itemSO.itemSprite;
                textStack.text = item.stack > 1 ? item.stack.ToString() : "";
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
                textStack.text = "";
            }        
        }
    }

    [SerializeField] private GameObject slot_Selected;
    public GameObject Slot_Selected
    {
        get
        {
            return slot_Selected;
        }
    }

    private void SetModal()
    {
        if (item == null) return;

        ItemSO itemSO = DataManager.Instance.GetItem(item.itemID);

        int rank = -1;
        float[] status = new float[5];

        if (itemSO.itemType == Enums.ItemType.Weapon)
        {
            rank = item.itemRank;

            status[0] = itemSO.itemEnchantHP[rank];
            status[1] = itemSO.itemEnchantAttack[rank];
            status[2] = itemSO.itemEnchantAttackDelay[rank];
            status[3] = itemSO.itemEnchantDefence[rank];
            status[4] = itemSO.itemEnchantAttackRange[rank];
        }
        else if (itemSO.itemType == Enums.ItemType.Material)
        {
            rank = itemSO.itemRank;

            status[0] = 0;
            status[1] = 0;
            status[2] = 0;
            status[3] = 0;
            status[4] = 0;
        }

        Modal.gameObject.SetActive(true);
        Modal.SetModal(itemSO.itemSprite, rank, itemSO.itemName, itemSO.itemDescription, status);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetModal();
        Slot_Selected.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Modal.gameObject.SetActive(false);
        Slot_Selected.SetActive(false);
    }
}
