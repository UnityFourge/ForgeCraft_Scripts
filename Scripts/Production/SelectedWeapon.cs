using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedWeapon : MonoBehaviour
{
    [Header("SwordRecipeSO")]
    [SerializeField] private List<RecipeSO> swordRecipe;
    [Header("ArrowRecipeSO")]
    [SerializeField] private List<RecipeSO> arrowRecipe;
    [Header("AxeRecipeSO")]
    [SerializeField] private List<RecipeSO> axeRecipe;
    [Header("ShieldRecipeSO")]
    [SerializeField] private List<RecipeSO> shieldRecipe;

    [SerializeField] private Button nextBtn;

    [SerializeField] private GameObject casting;

    public MaterialInventory MaterialInventory;

    private List<RecipeSO> selectedRecipeList;

    [SerializeField] private Button closeBtn;

    [SerializeField] private TextMeshProUGUI durabilityText;

    void Start()
    {
        nextBtn.onClick.AddListener(Next);  
        closeBtn.onClick.AddListener(Close);
    }
    private void OnEnable()
    {
        UpdateText();
    }

    void Next()
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        selectedRecipeList = GetSelectedRecipeList();
        
        int fatigue = ForgeManager.Instance.fatigueSystem.CurrentFatigue;

        if (ForgeManager.Instance.Durability > 4 && fatigue > 0)
        {
            if (CanCraftWeapon(selectedRecipeList))
            {
                CraftWeapon();
                gameObject.SetActive(false);
                casting.SetActive(true);
            }
            else
            {
                ForgeManager.Instance.ShowErrorPopup("제작 실패", "제작 가능한 무기가 없습니다", null);
            }
        }
        else if (ForgeManager.Instance.Durability > 4 && fatigue < 1)
        {
            ForgeManager.Instance.ShowErrorPopup("제작 실패", "피로도가 부족합니다",null);
        }
        else if (ForgeManager.Instance.Durability < 4 && fatigue >= 1)
        {
            ForgeManager.Instance.ShowErrorPopup("제작 실패", "내구도가 부족합니다", null);
        }
        else
        {
            ForgeManager.Instance.ShowErrorPopup("제작 실패", "내구도와 피로도가 부족합니다", null);
        }
    }
    void Close()
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        GameManager.UI.ClosePopupUI();
        gameObject.SetActive(false);
        //ForgeManager.Instance.ActiveLobby();

        MaterialInventory.ToPlayerInventory();

        MaterialInventory.ClearInventory();
    }
    void UpdateText() 
    {
        durabilityText.text = " 내구도 : " + ForgeManager.Instance.Durability.ToString() + "%";
    }

    List<RecipeSO> GetSelectedRecipeList()
    {
        int selectedWeapon = ForgeManager.Instance.GetSelectedWeapon();

        switch (selectedWeapon)
        {
            case 1:
                return swordRecipe;

            case 2:
                return arrowRecipe;

            case 3:
                return axeRecipe;

            case 4:
                return shieldRecipe;

            default:
                return new List<RecipeSO>();
        }
    }

    bool CanCraftWeapon(List<RecipeSO> recipes)
    {
        foreach (RecipeSO recipe in recipes)
        {
            bool canCraft = true;

            foreach (MaterialData material in recipe.Material)
            {
                int materialID = material.item.itemID;
                int requiredAmount = material.amount;

                if (!MaterialInventory.EnoughMaterial(materialID, requiredAmount))
                {
                    canCraft = false;
                    break;
                }
            }

            if (canCraft)
            {
                return true;
            }
        }

        return false;
    }

    void CraftWeapon()
    {

        foreach (RecipeSO recipe in selectedRecipeList)
        {
            bool canCraft = true;

            foreach (MaterialData material in recipe.Material)
            {
                int materialID = material.item.itemID;
                int requiredAmount = material.amount;

                if (!MaterialInventory.EnoughMaterial(materialID, requiredAmount))
                {
                    canCraft = false;
                    break;
                }
            }

            if (canCraft)
            {

                foreach (MaterialData material in recipe.Material)
                {
                    int materialID = material.item.itemID;
                    int requiredAmount = material.amount;

                    for (int i = 0; i < requiredAmount; i++)
                    {
                        MaterialInventory.ClearInventory();
                    }
                }
                ForgeManager.Instance.Durability -= 5;
                ForgeManager.Instance.fatigueSystem.DecreaseFatigue();


                int WeaponID = recipe.resultItem.itemID;
                string WeaponName = recipe.resultItem.itemName;
                Sprite WeaponImage = recipe.resultItem.weaponImage;

                ForgeManager.Instance.SetSelectedWeaponInfo(WeaponID, WeaponName, WeaponImage);
            }
        }
    }
}