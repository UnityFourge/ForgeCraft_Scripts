using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Explore : UI_Popup
{
    enum ExploreButtons
    {
        Button_Forge,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(ExploreButtons));

        GetButton((int)ExploreButtons.Button_Forge).gameObject.BindEvent(CloseExplore);
    }

    private void CloseExplore(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ClosePopupUI();
        GameManager.UI.ShowPopupUI<UI_ExploreSet>();
    }
}
