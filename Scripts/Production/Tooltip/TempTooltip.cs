using UnityEngine;
using UnityEngine.EventSystems;

public class TempTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private UpgradeToolTip tooltip;
    private void Tooltip(string content,string maxLevel)
    {
        tooltip.gameObject.SetActive(true);
        tooltip.SetToolTip(content,maxLevel);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip("레벨을 올릴수록 주조과정의 불? 속도가 줄어들어요", "MAX Level.6");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }
}
