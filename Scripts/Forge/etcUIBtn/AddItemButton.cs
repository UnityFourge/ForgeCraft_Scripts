using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddItemButton : MonoBehaviour
{
    [SerializeField] private Button btnAddItem;
    [SerializeField] private ItemSO itemSO;
    private FatigueUI fatigueUI;
    private void Awake()
    {
        btnAddItem = GetComponent<Button>();
        fatigueUI = FindObjectOfType<FatigueUI>();
    }

    private void Start()
    {
        btnAddItem.onClick.AddListener(() =>
        {
            if (ForgeManager.Instance.fatigueSystem.CurrentFatigue == 0)
            {
                ForgeManager.Instance.ShowErrorPopup("피로도 부족", "피로도가 부족하여 \n 아이템을 추가할 수 없습니다.", null);
                return;
            }

            Player.Instance.inventory.AddItem(itemSO);
            ForgeManager.Instance.fatigueSystem.DecreaseFatigue();

            if (fatigueUI != null)
            {
                fatigueUI.UpdateFatigueIcons();
            }
        });
    }
}
