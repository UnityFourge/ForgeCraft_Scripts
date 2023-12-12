using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Team : UI_Popup
{

    enum TeamBtns
    {
        CloseBtn,
        NextBtn,
        BackBtn,
    }


    private void Start()
    {
        Init();

    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(TeamBtns));
        GetButton((int)TeamBtns.CloseBtn).gameObject.BindEvent(OnCloseBtn);

    }

    public void OnCloseBtn(PointerEventData data)
    {
        GameManager.UI.ClosePopupUI();
    }
}
