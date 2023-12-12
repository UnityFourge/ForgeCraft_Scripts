using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Choice : UI_Popup
{
    enum ForgeButtons
    {
        Button_Inventory,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        //base.Init();

        Bind<Button>(typeof(ForgeButtons));
        GetButton((int)ForgeButtons.Button_Inventory).gameObject.BindEvent(OpenInven);
    }

    private void OpenInven(PointerEventData data)
    {
        GameManager.UI.ShowPopupUI<UI_Inven>();
    }
}
