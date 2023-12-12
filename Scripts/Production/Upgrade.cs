using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Upgrade : MonoBehaviour
{
    [Header("LevelText")]
    [SerializeField] private TextMeshProUGUI forgeLevelText;
    [SerializeField] private TextMeshProUGUI tempLevelText;
    [SerializeField] private TextMeshProUGUI hammerLevelText;
    [SerializeField] private TextMeshProUGUI handicraftLevelText;
 
    [Header("Button")]
    [SerializeField] private Button forgeBtn;
    [SerializeField] private Button tempBtn;
    [SerializeField] private Button hammerBtn;
    [SerializeField] private Button handicraftBtn;
    [SerializeField] private Button durability;
    [SerializeField] private Button cancelBtn;

    void Start()
    {
        var Level = ForgeManager.Instance;

        cancelBtn.onClick.AddListener(CloseUpgrade);

        forgeBtn.onClick.AddListener(() => Levelup("재료", "레벨을 " + Level.ForgeLevel + "->" + (Level.ForgeLevel + 1)+ " 로 올리시겠습니까?" , "비용은 " + Level.UpgradeCost(Level.ForgeLevel) + " 골드 입니다", Level.ForgeLevelUp));
        tempBtn.onClick.AddListener(() => Levelup("온도", "레벨을 " + Level.TempLevel + "->" + (Level.TempLevel + 1) + " 로 올리시겠습니까?", "비용은" + Level.UpgradeCost(Level.TempLevel) + " 골드 입니다", Level.TempLevelUp));
        hammerBtn.onClick.AddListener(() => Levelup("망치", "레벨을 " + Level.HammerLevel + "->" + (Level.HammerLevel + 1) + " 로 올리시겠습니까?", "비용은" + Level.UpgradeCost(Level.HammerLevel) + " 골드 입니다", Level.HammerLevelUp));
        handicraftBtn.onClick.AddListener(() => Levelup("손재주", "레벨을 " + Level.HandicraftLevel + "->" + (Level.HandicraftLevel + 1) + " 로 올리시겠습니까?", "비용은" + 500 + " 골드 입니다", Level.HandicraftLevelUp));
        durability.onClick.AddListener(() => Levelup("거푸집 수리", "거푸집을 수리하시겠습니까?", "비용은 내구도 1당 1 골드 입니다", Level.DurabilityReset));
    }
    void Update() 
    {
        UpdateLevelText();
    }
    private void UpdateLevelText() 
    {
        var LevelText = ForgeManager.Instance;

        forgeLevelText.text = "LV. "+LevelText.ForgeLevel.ToString();
        tempLevelText.text = "LV. " + LevelText.TempLevel.ToString();
        hammerLevelText.text = "LV. " + LevelText.HammerLevel.ToString();
        handicraftLevelText.text = "LV. " + LevelText.HandicraftLevel.ToString();
    }

    void Levelup(string title, string content, string cost,Action levelUp)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        var Level = ForgeManager.Instance;

        if ((title == "재료" && Level.ForgeLevel == 6) ||
           (title == "온도" && Level.TempLevel == 6) ||
           (title == "망치" && Level.HammerLevel == 6) ||
           (title == "손재주" && Level.HandicraftLevel == 2))
        {
            Level.ShowErrorPopup("업그레이드 실패", "더 이상 강화할 수 없습니다", null);
        }
        else
        {
            Level.ShowUIPopup(title, content, cost, () => { levelUp(); });
        }
    }

    void CloseUpgrade()
    {
        this.gameObject.SetActive(false);
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
    }
}