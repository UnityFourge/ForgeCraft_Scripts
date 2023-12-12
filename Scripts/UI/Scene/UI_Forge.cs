using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Forge : UI_Scene
{
    enum ForgeButtons
    {
        Button_Blacksmith,
        Button_Inventory,
        Button_ExploreSet,
        Button_Upgrade,
        Button_Save,
        Button_Rest,
        Button_Tutorial,
        Button_Pause,
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
        canvas.worldCamera = Camera.main;

        Bind<Button>(typeof(ForgeButtons));
        GetButton((int)ForgeButtons.Button_Blacksmith).gameObject.BindEvent(OpenBlacksmith);
        GetButton((int)ForgeButtons.Button_Inventory).gameObject.BindEvent(OpenInven);
        GetButton((int)ForgeButtons.Button_ExploreSet).gameObject.BindEvent(OpenExploreSet);
        GetButton((int)ForgeButtons.Button_Upgrade).gameObject.BindEvent(OpenUpgrade);
        GetButton((int)ForgeButtons.Button_Save).gameObject.BindEvent(SaveData);
        GetButton((int)ForgeButtons.Button_Rest).gameObject.BindEvent(Rest);
        GetButton((int)ForgeButtons.Button_Pause).gameObject.BindEvent(OpenPause);
    }

    private void OpenBlacksmith(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        //ForgeManager.Instance.ActiveLobby();
        GameManager.UI.ShowPopupUI<UI_Blacksmith>();
    }

    private void OpenInven(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ShowPopupUI<UI_Inven>();
    }

    private void OpenExploreSet(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ShowPopupUI<UI_ExploreSet>();
    }

    private void OpenUpgrade(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
    }

    private void SaveData(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        Player.Instance.SaveGameData();
    }

    private void Rest(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
    }

    private void OpenPause(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ShowPopupUI<UI_ForgePause>();
    }
}
   
