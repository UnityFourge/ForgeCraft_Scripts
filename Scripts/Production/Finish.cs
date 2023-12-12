using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    [SerializeField] private Image sword;
    [SerializeField] private Image bow;
    [SerializeField] private Image axe;
    [SerializeField] private Image shield;

    private Image currentWeapon;
    private int selectedWeapon;

    private void OnEnable()
    {
        HideAll();
        SelectedWeapon();
        StringBuilder sb = new StringBuilder();

        int score = ForgeManager.Instance.WeaponScore;

        if (score >= 80)
        {
            sb.Append("최상급");
        }
        else if (score >= 50)
        {
            sb.Append("상급");
        }
        else if (score >= 20)
        {
            sb.Append("중급");
        }
        else
        {
            sb.Append("하급");
        }

        sb.Append(" ").Append(ForgeManager.Instance.GetSelectedWeaponName()).Append(" 이(가)\n인벤토리에 추가되었습니다");

        ForgeManager.Instance.ShowErrorPopup("제작성공", sb.ToString(), Close);

        ForgeManager.Instance.ResetWeaponScore();
    }
    private void Close() 
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        gameObject.SetActive(false);
        
        GameManager.UI.ClosePopupUI();
        GameManager.UI.ShowPopupUI<UI_Blacksmith>();
    }

    private void SelectedWeapon()
    {

        selectedWeapon = ForgeManager.Instance.GetSelectedWeapon();

        switch (selectedWeapon)
        {
            case 1:
                currentWeapon = sword;
                SetWeaponImage(ForgeManager.Instance.GetselectedWeaponImage());
                sword.gameObject.SetActive(true);
                break;
            case 2:
                currentWeapon = bow;
                SetWeaponImage(ForgeManager.Instance.GetselectedWeaponImage());
                bow.gameObject.SetActive(true);
                break;
            case 3:
                currentWeapon = axe;
                SetWeaponImage(ForgeManager.Instance.GetselectedWeaponImage());
                axe.gameObject.SetActive(true);
                break;
            case 4:
                currentWeapon = shield;
                SetWeaponImage(ForgeManager.Instance.GetselectedWeaponImage());
                shield.gameObject.SetActive(true);
                break;
        }
    }
    private void SetWeaponImage(Sprite weaponSprite)
    {
        currentWeapon.sprite = weaponSprite;
    }
    private void HideAll() 
    {
        sword.gameObject.SetActive(false);
        bow.gameObject.SetActive(false);
        axe.gameObject.SetActive(false);
        shield.gameObject.SetActive(false);
    }

}
