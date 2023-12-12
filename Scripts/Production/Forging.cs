using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Inventory;

public class Forging : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Button hammerBtn;


    [SerializeField] private TextMeshProUGUI countText;

    [SerializeField] private Image sword;
    [SerializeField] private Image arrow;
    [SerializeField] private Image axe;
    [SerializeField] private Image shield;

    [SerializeField] private Button nextBtn;

    [SerializeField] private GameObject swordMovebar;
    [SerializeField] private GameObject arrowMovebar;
    [SerializeField] private GameObject axeMovebar;
    [SerializeField] private GameObject shieldMovebar;


    //[SerializeField] private ParticleSystem currentParticle;

    private Image currentWeapon;

    private int selectedWeapon;
    private bool isIncrease = true;

    private int maxClickCount = 5;
    private int clickCount = 0;

    private Coroutine shakeCoroutine;

    private float valueChange = 200.0f;
    private float maxSliderValue = 100.0f;
    private int hammerLevel;

    private void OnEnable()
    {
        SelectedWeapon();
        nextBtn.interactable = false;
        isIncrease = true;

        maxClickCount = 5;
        clickCount = 0;
        PlayerLevel();
    }

    void Start()
    {
        hammerBtn.onClick.AddListener(HammerBtnClick);
        nextBtn.onClick.AddListener(Next);

        SelectedWeapon();
        slider.maxValue = maxSliderValue;
        slider.interactable = false;
    }

    private void Update()
    {
        UpdateText();

        if (clickCount == maxClickCount)
        {
            nextBtn.interactable = true;
            return;
        }
        else
        {
            float delta = isIncrease ? valueChange : -valueChange;
            float newTempValue = slider.value + delta * Time.deltaTime;

            slider.value = newTempValue;

            if (newTempValue >= maxSliderValue || newTempValue <= 0)
            {
                isIncrease = !isIncrease;
                newTempValue = Mathf.Clamp(newTempValue, slider.minValue, maxSliderValue);
            }
        }
    }

    void SelectedWeapon() 
    {
        nextBtn.interactable = false;
        HideAll();
        selectedWeapon = ForgeManager.Instance.GetSelectedWeapon();

        switch (selectedWeapon)
        {
            case 1:
                currentWeapon = sword;
                SetWeaponImage(ForgeManager.Instance.GetselectedWeaponImage());
                sword.gameObject.SetActive(true);
                break;
            case 2:
                currentWeapon = arrow;
                SetWeaponImage(ForgeManager.Instance.GetselectedWeaponImage());
                arrow.gameObject.SetActive(true);
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

    void SetWeaponImage(Sprite weaponSprite) 
    {
        currentWeapon.sprite = weaponSprite;
    }

    void AddItem() 
    {
        var addItem = ForgeManager.Instance;

        Player.Instance.inventory.AddWeapon(new InventoryItem(addItem.GetSelectedWeaponInfo(), Enums.ItemType.Weapon, addItem.WeaponScore));
        //addItem.ResetWeaponScore();
    }

    void HideAll() 
    {
        sword.gameObject.SetActive(false);
        arrow.gameObject.SetActive(false);
        axe.gameObject.SetActive(false);
        shield.gameObject.SetActive(false);
    }

    void Next()
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        selectedWeapon = ForgeManager.Instance.GetSelectedWeapon();

        switch (selectedWeapon) 
        {
            case 1:
                swordMovebar.SetActive(true);
                break;
            case 2:
                arrowMovebar.gameObject.SetActive(true);
                break;
            case 3:
                axeMovebar.SetActive(true);
                break;
            case 4: 
                shieldMovebar.SetActive(true);
                break;
        }
        gameObject.SetActive(false);

        AddItem();
    }

    private void PlayerLevel()
    {
        hammerLevel = ForgeManager.Instance.HammerLevel;
        switch (hammerLevel)
        {
            case 1:
                valueChange = 200.0f;
                break;
            case 2:
                valueChange = 180.0f;
                break;
            case 3:
                valueChange = 160.0f;
                break;
            case 4:
                valueChange = 140.0f;
                break;
            case 5:
                valueChange = 120.0f;
                break;
            case 6:
                valueChange = 100.0f;
                break;
        }
    }

    private void UpdateText()
    {
        int countCheck = maxClickCount - clickCount;
        countText.text = countCheck.ToString();
    }

    private void HammerBtnClick()
    {
        if (clickCount < maxClickCount)
        {
            if (slider.value >= 30.0f && slider.value <= 70.0f)
            {
                SoundManager.Instance.SfxPlay(Enums.SFX.Hammering);
                ShakeImage();
                ForgeManager.Instance.AddWeaponScore(10);
            }
            else
            {
                SoundManager.Instance.SfxPlay(Enums.SFX.HammeringFail);
            }
            clickCount++;
        }
    }

    private void ShakeImage()
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }

        float shakeDuration = 0.5f;
        float shakeAmount = 10.0f;

        shakeCoroutine = StartCoroutine(ShakeCoroutine(currentWeapon.rectTransform, shakeAmount, shakeDuration));
    }

    private IEnumerator ShakeCoroutine(RectTransform rectTransform, float shakeAmount, float duration)
    {
        Vector3 originalPosition = rectTransform.anchoredPosition;
        Color originalColor = currentWeapon.color;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-shakeAmount, shakeAmount);
            float offsetY = Random.Range(-shakeAmount, shakeAmount);

            rectTransform.anchoredPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            Color newColor = new Color(originalColor.r, Mathf.Max(0, originalColor.g - 50), Mathf.Max(0, originalColor.b - 50), originalColor.a);
            currentWeapon.color = newColor;

            elapsed += Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = originalPosition;
        currentWeapon.color = originalColor;
        shakeCoroutine = null;
    }
}