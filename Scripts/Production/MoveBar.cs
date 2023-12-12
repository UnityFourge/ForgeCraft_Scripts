using UnityEngine;
using UnityEngine.UI;

public class MoveBar : MonoBehaviour
{
    [SerializeField] private Button nextBtn;
    [SerializeField] private GameObject finish;

    [SerializeField] private Image weapon;
    private Image currentWeapon;

    void Start()
    {
        nextBtn.onClick.AddListener(Next);

        Camera mainCamera = Camera.main;

        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null && mainCamera != null)
        {
            canvas.worldCamera = mainCamera;
        }
    }
    private void OnEnable()
    {
        currentWeapon = weapon;
        SetWeaponImage(ForgeManager.Instance.GetselectedWeaponImage());
    }
    void Next() 
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        gameObject.SetActive(false);
        finish.SetActive(true);      
    }

    void SetWeaponImage(Sprite weaponSprite)
    {
        currentWeapon.sprite = weaponSprite;
    }
}
