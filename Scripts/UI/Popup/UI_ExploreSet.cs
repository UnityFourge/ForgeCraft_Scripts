using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_ExploreSet : UI_Popup
{
    enum ExploreSetButtons
    {
        Button_Close,
        Button_Gotcha,
        Button_Manage,
        Button_Explore,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(ExploreSetButtons));
        GetButton((int)ExploreSetButtons.Button_Close).gameObject.BindEvent(CloseExploreSet);
        GetButton((int)ExploreSetButtons.Button_Gotcha).gameObject.BindEvent(OpenGotcha);
        GetButton((int)ExploreSetButtons.Button_Explore).gameObject.BindEvent(OpenExplore);
        GetButton((int)ExploreSetButtons.Button_Manage).gameObject.BindEvent(OpenManage);
    }

    private void CloseExploreSet(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ClosePopupUI();
    }

    private void OpenGotcha(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ClosePopupUI();
        GameManager.UI.ShowPopupUI<UI_Gotcha>();
    }

    private void OpenExplore(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ClosePopupUI();
        GameManager.UI.ShowPopupUI<UI_Explore>();
    }

    private void OpenManage(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ClosePopupUI();
        GameManager.UI.ShowPopupUI<UI_Manage>();
    }
}
