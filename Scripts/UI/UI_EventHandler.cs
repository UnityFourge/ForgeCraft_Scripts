using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour,IDragHandler, IPointerClickHandler,IPointerEnterHandler
    , IPointerExitHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnEnterHandler = null;
    public Action<PointerEventData> OnExitHandler = null;
   

    public void OnDrag(PointerEventData eventData)
    {
        if(OnDragHandler != null)
            OnDragHandler.Invoke(eventData);

        //Debug.Log("OnDrag");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("OnClick");
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Enter");
        if (OnEnterHandler != null)
            OnEnterHandler.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Exit");
        if (OnExitHandler != null)
            OnExitHandler.Invoke(eventData);
    }
}
