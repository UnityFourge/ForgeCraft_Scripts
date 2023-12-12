using UnityEngine;

public class FatigueSystem : MonoBehaviour
{
    [SerializeField] private int maxFatigue;
    private FatigueUI fatigueUI;

    public int CurrentFatigue { get; private set; }

    private void Awake()
    {
        fatigueUI = FindObjectOfType<FatigueUI>();

        if (fatigueUI != null)
        {
            ResetFatigue();
        }
    }

    public void ResetFatigue()
    {
        CurrentFatigue = maxFatigue;
        Player.Instance.D_PlayerData.fatigue = CurrentFatigue;
        UpdateUI();
    }

    public void DecreaseFatigue()
    {
        if (CurrentFatigue > 0)
        {
            CurrentFatigue--;
            Player.Instance.D_PlayerData.fatigue = CurrentFatigue;
            UpdateUI();
        }
    }

    public void IncreaseFatigue(int amount) // 아이템 등으로 피로도 회복용
    {
        CurrentFatigue = Mathf.Min(CurrentFatigue + amount, maxFatigue);
        Player.Instance.D_PlayerData.fatigue = CurrentFatigue;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (fatigueUI != null)
        {
            fatigueUI.UpdateFatigueIcons();
        }
    }

}
