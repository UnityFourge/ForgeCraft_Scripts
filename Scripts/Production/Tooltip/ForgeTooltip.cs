using UnityEngine;
using UnityEngine.EventSystems;


public class ForgeTooltip : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private UpgradeToolTip tooltip;

    private void Tooltip(string content,string levelText) 
    {
        tooltip.gameObject.SetActive(true);
        tooltip.SetToolTip(content,levelText);
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        Tooltip("재료 레벨을 올리면 더 높은 등급의 재료를 사용해서 무기를 만들 수 있습니다","MAX Level.6");

    }
    public void OnPointerExit(PointerEventData eventData) 
    {
        tooltip.gameObject.SetActive(false);
    }

}
