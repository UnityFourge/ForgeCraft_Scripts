using UnityEngine;
using UnityEngine.EventSystems;

public class DurabilityTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UpgradeToolTip tooltip;

    private void Tooltip(string content,string maxLevel)
    {
        tooltip.gameObject.SetActive(true);
        tooltip.SetToolTip(content,null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip("거푸집 내구도를 수리할 수 있습니다 ",null);

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }
}
