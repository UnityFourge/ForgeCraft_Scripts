using UnityEngine;
using UnityEngine.UI;

public class WeaponBtn : MonoBehaviour
{
    [SerializeField] private Button swordBtn;
    [SerializeField] private Button arrowBtn;
    [SerializeField] private Button axeBtn;
    [SerializeField] private Button shieldBtn;

    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject shield;

    [SerializeField] private Button nextBtn;

    [SerializeField] private GameObject choiceMaterial;

    [SerializeField] private Button closeBtn;

    void Start()
    {
        closeBtn.onClick.AddListener(Close);

        swordBtn.onClick.AddListener(() => SelectWeapon(1));
        arrowBtn.onClick.AddListener(() => SelectWeapon(2));
        axeBtn.onClick.AddListener(() => SelectWeapon(3));
        shieldBtn.onClick.AddListener(() => SelectWeapon(4));

        nextBtn.interactable = false;
        nextBtn.onClick.AddListener(Next);

        HideAll();


    }

    void OnEnable()
    {
        HideAll();
    }

    void SelectWeapon(int weaponName)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        HideAll();

        switch (weaponName)
        {
            case 1:
                sword.SetActive(true);     
                break;
            case 2:
                arrow.SetActive(true);
                break;
            case 3:
                axe.SetActive(true);
                break;
            case 4:
                shield.SetActive(true);
                break;
        }

        ForgeManager.Instance.SetSelectedWeapon(weaponName);
        nextBtn.interactable = true;
    }
    void HideAll() 
    {
        sword.SetActive(false);
        arrow.SetActive(false);
        axe.SetActive(false);
        shield.SetActive(false);
    }

    void Next()
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        gameObject.SetActive(false);
        choiceMaterial.SetActive(true);
    }

    void Close()
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        gameObject.SetActive(false);
    }
}
