using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Forging : UI_Popup
{
    enum ForgingButtons
    {
        Button_Close,
    }

    private void Awake()
    {
        Camera mainCamera = Camera.main;
        Canvas canvas = GetComponent<Canvas>();

        if (canvas != null && mainCamera != null)
        {
            canvas.worldCamera = mainCamera;
        }
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        //base.Init();

        Bind<Button>(typeof(ForgingButtons));
        GetButton((int)ForgingButtons.Button_Close).gameObject.BindEvent(CloseForging);
    }

    private void CloseForging(PointerEventData data)
    {
        GameManager.UI.ClosePopupUI();
        GameManager.UI.ShowPopupUI<UI_Blacksmith>();
    }
}
