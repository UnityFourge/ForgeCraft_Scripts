using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MaterialSlotUI : MonoBehaviour
{
    public MaterialInventory materialInventory;

    [SerializeField] private Button btnSlot;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI quantityText;

    private ItemSO item;
    private int itemQuantity;

    void Start()
    {      
        btnSlot.onClick.AddListener(RemoveItem);
    }

    public void SetItem(ItemSO newItem, int quantity)
    {
        item = newItem;
        itemQuantity = quantity;
        if (item != null)
        {
            if (icon != null && quantityText != null)
            {
                icon.color = Color.white;
                icon.sprite = item.itemSprite;
                quantityText.text = itemQuantity.ToString(); 
            }
        }
        RefreshUI();
    }
    public ItemSO GetItem()
    {
        return item;
    }
    public bool IsEmpty()
    {
        return item == null;
    }

    public void RefreshUI()
    {
        if (item != null)
        {
            if (icon != null)
            {
                icon.sprite = item.itemSprite;
            }

            if (quantityText != null)
            {
                quantityText.text = itemQuantity.ToString();
            }
        }
    }
    public void ClearItem()
    {
        icon.color = new Color(1,1,1,0);
        quantityText.text = "";   
    }

    public void RemoveItem() 
    {
        if (!IsEmpty() && itemQuantity > 0) 
        {
            itemQuantity--;
            RefreshUI();
            int itemID = item.itemID;
            if (materialInventory != null)
            {
                materialInventory.RemoveMaterial(itemID);
                Player.Instance.inventory.AddItem(item);
            }

            if (itemQuantity == 0)
            {
                ClearItem();
            }

        }
    }
}
