using UnityEngine;
using UnityEngine.EventSystems;

public class HandicraftTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UpgradeToolTip tooltip;

    private void SetTooltip(string content,string maxLevel)
    {
        tooltip.gameObject.SetActive(true);
        tooltip.SetToolTip(content,maxLevel);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetTooltip("레벨업 시 최하급 등급의 무기가 등장하지않습니다", "MAX Level.2");

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }
}
