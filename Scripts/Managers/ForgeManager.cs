using System;
using UnityEngine;

public class ForgeManager : MonoBehaviour
{
    private static ForgeManager instance;
    public static ForgeManager Instance
    {
        get { return instance; }
    }

    private int forgeLevel = 1;
    private int tempLevel = 1;
    private int hammerLevel = 1;
    private int handicraftLevel = 1;
    private int durability = 100;

    public int ForgeLevel { get { return forgeLevel; } set { forgeLevel = value; Player.Instance.D_PlayerData.forgeLevel = value; } }
    public int TempLevel { get { return tempLevel; } set { tempLevel = value; Player.Instance.D_PlayerData.tempLevel = value; } }
    public int HammerLevel { get { return hammerLevel; } set { hammerLevel = value; Player.Instance.D_PlayerData.hammerLevel = value; } }
    public int HandicraftLevel { get { return handicraftLevel; } set { handicraftLevel = value; Player.Instance.D_PlayerData.handicraftLevel = value; } }
    public int Durability { get { return durability; } set { durability = value; Player.Instance.D_PlayerData.durability = value; } }

    public int WeaponScore { get; private set; }

    private int selectedWeaponID;
    private string selectedWeaponName;
    private Sprite selectedWeaponImage;

    private int selectedWeapon;

    [SerializeField] private GameObject ui_Lobby;
    public FatigueSystem fatigueSystem;

    public ErrorPopup errorPopup;
    public UIPopup uiPopup;

    private void Awake()
    {
        instance = this;
    }

    public void SetSelectedWeapon(int weaponName)
    {
        selectedWeapon = weaponName;
    }

    public int GetSelectedWeapon()
    {
        return selectedWeapon;
    }

    public void ClearSelectedWeapon()
    {
        selectedWeapon = -1;
    }

    public void ForgeLevelUp()
    {
        int cost = UpgradeCost(ForgeLevel);
        int PlayerGold = GoldManager.Instance.CurrentGold;

        if (ForgeLevel < 6 && PlayerGold >= cost)
        {
            GoldManager.Instance.SubtractGold(cost);
            ForgeLevel++;
            ShowErrorPopup("업그레이드 성공", "대장간 레벨이" + ForgeLevel + "으로 올랐다", null);
        }
        else if (ForgeLevel == 6)
        {
            ShowErrorPopup("업그레이드 실패", "이미 최대 레벨입니다.", null);
        }
        else
        {
            ShowErrorPopup("업그레이드 실패", "골드가 부족하거나 업그레이드 조건에 맞지 않습니다", null);
        }
    }

    public void TempLevelUp()
    {
        int cost = UpgradeCost(TempLevel);
        int PlayerGold = GoldManager.Instance.CurrentGold;

        if (TempLevel < 6 && PlayerGold >= cost)
        {
            GoldManager.Instance.SubtractGold(cost);
            TempLevel++;
            ShowErrorPopup("업그레이드 성공", "온도 레벨이 " + TempLevel + "으로 올랐다", null);
        }
        else if (TempLevel == 6)
        {
            ShowErrorPopup("업그레이드 실패", "이미 최대 레벨입니다.", null);
        }
        else
        {
            ShowErrorPopup("업그레이드 실패", "골드가 부족하거나 업그레이드 조건에 맞지 않습니다", null);
        }
    }

    public void HammerLevelUp()
    {
        int cost = UpgradeCost(HammerLevel);
        int PlayerGold = GoldManager.Instance.CurrentGold;

        if (HammerLevel < 6 && PlayerGold >= cost)
        {
            GoldManager.Instance.SubtractGold(cost);
            HammerLevel++;
            ShowErrorPopup("업그레이드 성공", "망치 레벨이 " + HammerLevel + "으로 올랐다", null);
        }
        else if (HammerLevel == 6)
        {
            ShowErrorPopup("업그레이드 실패", "이미 최대 레벨입니다.", null);
        }
        else
        {
            ShowErrorPopup("업그레이드 실패", "골드가 부족하거나 업그레이드 조건에 맞지 않습니다", null);
        }
    }

    public void HandicraftLevelUp()
    {
        int cost = UpgradeCost(HandicraftLevel);
        int PlayerGold = GoldManager.Instance.CurrentGold;

        if (HandicraftLevel < 2 && PlayerGold >= 500)
        {
            GoldManager.Instance.SubtractGold(500);
            HandicraftLevel++;
            ShowErrorPopup("업그레이드 성공", "내구도 레벨이 " + HandicraftLevel + "으로 올랐다", null);
            AddWeaponScore(20);
        }
        else if (HandicraftLevel == 2)
        {
            ShowErrorPopup("업그레이드 실패", "이미 최대 레벨입니다.", null);
        }
        else
        {
            ShowErrorPopup("업그레이드 실패", "골드가 부족하거나 업그레이드 조건에 맞지 않습니다", null);
        }
    }

    public void DurabilityReset()
    {
        int cost = (100 - Durability);
        int PlayerGold = GoldManager.Instance.CurrentGold;

        if (Durability == 100)
        {
            ShowErrorPopup("수리 실패 ", "내구도가 높아 수리를 할 수 없습니다", null);
        }
        else if (PlayerGold >= cost)
        {
            Durability = 100;
            GoldManager.Instance.SubtractGold(cost);
            ShowErrorPopup("수리 성공", "수리비용: " + cost + "골드 입니다", null);
        }
        else
        {
            ShowErrorPopup("수리 실패 ", "골드가 부족하여 수리를 할 수 없습니다", null);
        }
    }

    public void ShowErrorPopup(string title, string content, Action onconfirm)
    {
        errorPopup.SetErrorPopup(title, content, onconfirm);
        errorPopup.gameObject.SetActive(true);

    }

    public void ShowUIPopup(string title, string content, string cost, Action onConfirm)
    {
        uiPopup.SetPopup(title, content, cost, onConfirm);
        uiPopup.gameObject.SetActive(true);
    }
    public void ActiveLobby()
    {
        ui_Lobby.SetActive(!ui_Lobby.activeSelf);
    }

    public int UpgradeCost(int Level)
    {
        int cost = 200 * Level;

        return cost;
    }

    public void AddWeaponScore(int score)
    {
        WeaponScore += score;
    }

    public void ResetWeaponScore()
    {
        if (HandicraftLevel < 2)
        {
            WeaponScore = 0;
        }
        else
        {
            WeaponScore = 20;
        }
    }

    public void SetSelectedWeaponInfo(int weaponID, string weaponName, Sprite weaponImage)
    {
        selectedWeaponID = weaponID;
        selectedWeaponName = weaponName;
        selectedWeaponImage = weaponImage;
    }
    public void ResetSelectedWeaponInfo()
    {
        selectedWeaponID = 0;
        selectedWeaponName = null;
        selectedWeaponImage = null;
    }

    public int GetSelectedWeaponInfo()
    {
        return selectedWeaponID;
    }
    public string GetSelectedWeaponName()
    {
        return selectedWeaponName;
    }
    public Sprite GetselectedWeaponImage()
    {
        return selectedWeaponImage;
    }
}