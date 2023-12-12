using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MaterialChoice : MonoBehaviour
{
    [SerializeField] private Button copperBtn;
    [SerializeField] private Button tinBtn;
    [SerializeField] private Button steelBtn;
    [SerializeField] private Button mithrilBtn;
    [SerializeField] private Button orichalcumBtn;
    [SerializeField] private Button solariumBtn;
    [SerializeField] private Button lineBtn;
    [SerializeField] private Button rubberBtn;
    [SerializeField] private Button branchBtn;

    [SerializeField] private Button subMaterialBtn;
    [SerializeField] private Button closeSubMaterialBtn;

    [SerializeField] private Button materialInventoryBtn;
    [SerializeField] private Button closeMaterialIntentoryBtn;


    [SerializeField] private GameObject subMaterial;
    [SerializeField] private GameObject materialInventoryCanvas;
    
    public MaterialInventory MaterialInventory;
    

    public ItemSO[] itemSO;
    public Dictionary<int, ItemSO> MaterialSO;

    void Start()
    {      
        
        copperBtn.onClick.AddListener(() => AddMaterialToInventory(0));
        tinBtn.onClick.AddListener(() => AddMaterialToInventory(1));
        steelBtn.onClick.AddListener(() => AddMaterialToInventory(2));
        mithrilBtn.onClick.AddListener(() => AddMaterialToInventory(3));
        orichalcumBtn.onClick.AddListener(() => AddMaterialToInventory(4));
        solariumBtn.onClick.AddListener(() => AddMaterialToInventory(5));
        branchBtn.onClick.AddListener(() => AddMaterialToInventory(6));
        lineBtn.onClick.AddListener(() => AddMaterialToInventory(7));
        rubberBtn.onClick.AddListener(() => AddMaterialToInventory(8));

        materialInventoryBtn.onClick.AddListener(OnMaterialInventoryBtn);
        closeMaterialIntentoryBtn.onClick.AddListener(OffMaterialInventorybtn);
        subMaterialBtn.onClick.AddListener(OnSubMaterial);
        closeSubMaterialBtn.onClick.AddListener(OffSubMaterial);
        MaterialSO = new Dictionary<int, ItemSO>();

        for (int i = 0; i < itemSO.Length; i++) 
        {
            MaterialSO.Add(itemSO[i].itemID, itemSO[i]);
        }
    }

    void AddMaterialToInventory(int itemID)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        if (ForgeManager.Instance.ForgeLevel >= MaterialSO[itemID].UseLevel)
        {
            Inventory.InventoryItem playerItem = Player.Instance.inventory.bag.items.FirstOrDefault(item => item.itemID == itemID);

            if (playerItem != null)
            {
                Player.Instance.inventory.SubItem(MaterialSO[itemID]);
                MaterialInventory.AddMaterial(MaterialSO[itemID]);
            }
            else
            {
                string itemName = GetItemName(itemID);
                ForgeManager.Instance.ShowErrorPopup("추가 실패","플레이어 인벤토리에 " + GetItemName(itemID) + "이(가) 없습니다.", null);
            }
        }
        else
        {
            ForgeManager.Instance.ShowErrorPopup("추가 실패","대장간이 " + MaterialSO[itemID].UseLevel + " 레벨 이상이어야 합니다.", null);
        }
    }

    string GetItemName(int itemID)
    {
        if (MaterialSO.TryGetValue(itemID, out ItemSO item))
        {
            return item.itemName;
        }
        else
        { 
            return null;
        }
    }

    void OnMaterialInventoryBtn()
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        materialInventoryCanvas.SetActive(true);
    }

    void OffMaterialInventorybtn()
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        materialInventoryCanvas.SetActive(false);
        materialInventoryBtn.gameObject.SetActive(true);
    }

    void OnSubMaterial() 
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        subMaterial.SetActive(true);
    }
    void OffSubMaterial() 
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        subMaterial.SetActive(false);
    }
}



