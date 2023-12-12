using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven : UI_Popup
{
    enum InvenButtons
    {
        Button_Close,
    }

    UI_Inventory ui_Inventory;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        ui_Inventory = GetComponentInChildren<UI_Inventory>();
        ui_Inventory.FreshSlot();

        Bind<Button>(typeof(InvenButtons));
        GetButton((int)InvenButtons.Button_Close).gameObject.BindEvent(CloseInven);
    }

    private void CloseInven(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ClosePopupUI();
    }
}
