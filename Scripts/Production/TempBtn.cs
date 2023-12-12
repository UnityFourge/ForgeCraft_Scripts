using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TempBtn : MonoBehaviour
{
    [SerializeField] private Button tempBtn;
    [SerializeField] private Slider tempSlider;
    [SerializeField] private Slider mainSlider;

    [SerializeField] private TextMeshProUGUI mainValueText;

    [SerializeField] private TextMeshProUGUI countText;

    [SerializeField] private GameObject nextBtn;
    [SerializeField] private Lever lever;

    private float maxTempValue = 100.0f;
    private float maxMainValue = 200.0f;

    private bool isIncrease = true;

    private float tempValueChange;

    private int clickCount = 0;
    private int maxClickCount = 5;

    private int tempLevel;

    void Start()
    {
        tempLevel = ForgeManager.Instance.TempLevel;

        tempBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.SfxPlay(Enums.SFX.Button);
            TempButtonClick();
        });
        nextBtn.SetActive(false);

        lever.enabled = false;

        tempSlider.maxValue = maxTempValue;
        mainSlider.maxValue = maxMainValue;

        tempSlider.interactable = false;
        mainSlider.interactable = false;

    }

    void OnDisable()
    {
        lever.enabled = false;
        isIncrease = true;
        nextBtn.SetActive(false);
        clickCount = 0;
        mainSlider.value = 0;

        tempBtn.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        PlayerLevel();
    }

    void Update()
    {
        UpdateTempText();

        if (clickCount == maxClickCount)
        {
            isIncrease = false;
            lever.enabled = true;

            if (lever.IsStoppedParticle) { nextBtn.SetActive(true); }
        }
        else
        {
            float delta = isIncrease ? tempValueChange : -tempValueChange;
            float newTempValue = tempSlider.value + delta * Time.deltaTime;

            tempSlider.value = newTempValue;

            if (newTempValue >= maxTempValue || newTempValue <= 0)
            {
                isIncrease = !isIncrease;
                newTempValue = Mathf.Clamp(newTempValue, tempSlider.minValue, maxTempValue);
            }
        }
    }

    void UpdateTempText() 
    {
        mainValueText.text = "온도: " + mainSlider.value.ToString("F0");

        int countCheck = maxClickCount - clickCount;

        countText.text = countCheck.ToString();

    }

    private void TempButtonClick()
    {
        if (clickCount < maxClickCount)
        {
            if (tempSlider.value >= 30.0f && tempSlider.value <= 70.0f)
            {
                mainSlider.value += 40.0f;
                ForgeManager.Instance.AddWeaponScore(10);
            }
            clickCount++;
        }
    }

    private void PlayerLevel() 
    {
        tempLevel = ForgeManager.Instance.TempLevel;
        switch (tempLevel) 
        {
            case 1:
                tempValueChange = 200.0f;
                break;
            case 2:
                tempValueChange = 180.0f;
                break;
            case 3:
                tempValueChange = 160.0f;
                break;
            case 4:
                tempValueChange = 140.0f;
                break;
            case 5:
                tempValueChange = 120.0f;
                break;
            case 6:
                tempValueChange = 100.0f;
                break;
        }

    }

}
