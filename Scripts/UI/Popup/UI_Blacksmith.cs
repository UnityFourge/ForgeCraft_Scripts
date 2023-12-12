using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Blacksmith : UI_Popup
{
    enum BlacksmithButtons
    {
        Button_Inventory,
        Button_Forging,
        Button_Forge,
    }

    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;

        Bind<Button>(typeof(BlacksmithButtons));
        GetButton((int)BlacksmithButtons.Button_Inventory).gameObject.BindEvent(OpenInven);
        GetButton((int)BlacksmithButtons.Button_Forging).gameObject.BindEvent(OpenForging);
        GetButton((int)BlacksmithButtons.Button_Forge).gameObject.BindEvent(OpenForge);
    }

    private void OpenInven(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ShowPopupUI<UI_Inven>();
    }

    private void OpenForging(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ClosePopupUI();
        GameManager.UI.ShowPopupUI<UI_Forging>();
    }

    private void OpenForge(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ClosePopupUI();
        //ForgeManager.Instance.ActiveLobby();
    }
}
