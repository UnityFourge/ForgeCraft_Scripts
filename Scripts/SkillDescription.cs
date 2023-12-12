using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject SkillDscpUI;
    public Text Description;
    public int SkillNum;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SkillDscpUI.SetActive(true);
        SkillDscpUI.transform.position = new Vector2(Input.mousePosition.x + 100f, Input.mousePosition.y + 175f);
        Description.text = SkillManager.Instance.PlayerSkillSet[SkillNum].SkillDesc;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SkillDscpUI.SetActive(false);
    }
}
