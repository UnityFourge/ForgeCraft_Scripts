using UnityEngine;
using System;

public class GoldManager : MonoBehaviour
{
    private static GoldManager instance;
    public static GoldManager Instance
    {
        get { return instance; }
    }

    private int currentGold = 0;
    public int CurrentGold
    {
        get { return currentGold; }
        set { currentGold = value; }
    }
    public event Action<int> onGoldChanged;

    private void Awake()
    {
        instance = this;

        CurrentGold = Player.Instance.D_PlayerData.gold;
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        Player.Instance.D_PlayerData.gold = currentGold;
        onGoldChanged?.Invoke(currentGold);
    }

    public void SubtractGold(int amount)
    {
        currentGold = Mathf.Max(currentGold - amount, 0);
        Player.Instance.D_PlayerData.gold = currentGold;
        onGoldChanged?.Invoke(currentGold);
    }
}
