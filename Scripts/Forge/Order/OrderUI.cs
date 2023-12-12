using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderUI : MonoBehaviour
{
    public GameObject OrderPopupUI;
    public Image OrderImage;
    public Image NPCImage;
    public TextMeshProUGUI orderWeaponText; 
    public TextMeshProUGUI orderWeaponGold; 

    [SerializeField] private Button cancelBtn;
    [SerializeField] private Button confirm_Complete;
    [SerializeField] private Button confirm_Reject;

    private void Awake()
    {
        cancelBtn.onClick.AddListener(() => SoundManager.Instance.SfxPlay(Enums.SFX.Button));
        confirm_Complete.onClick.AddListener(() => SoundManager.Instance.SfxPlay(Enums.SFX.Button));
        confirm_Reject.onClick.AddListener(() => SoundManager.Instance.SfxPlay(Enums.SFX.Button));
    }

    public void Show(int weaponId)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        if (OrderPopupUI != null)
        {
            OrderPopupUI.SetActive(true);
            ItemSO weaponData = DataManager.Instance.GetItem(weaponId);

            if (weaponData != null)
            {
                SetOrderImage(weaponData.itemSprite);
            }
        }
    }

    public void SetOrderImage(Sprite sprite)
    {
        if (OrderImage != null)
        {
            OrderImage.sprite = sprite;
        }
    }

    public void SetOrderDetails(string weaponName, int weaponValue)
    {
        orderWeaponText.text = weaponName;
        orderWeaponGold.text = weaponValue + "G";
    }

    public void Hide()
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        gameObject.SetActive(false);
    }
}
