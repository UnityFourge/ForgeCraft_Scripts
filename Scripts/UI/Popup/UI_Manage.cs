using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manage : UI_Popup
{
    enum ManageButtons
    {
        Button_Close,
    }


    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(ManageButtons));

        GetButton((int)ManageButtons.Button_Close).gameObject.BindEvent(CloseManage);
    }

    private void CloseManage(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        GameManager.UI.ClosePopupUI();
        GameManager.UI.ShowPopupUI<UI_ExploreSet>();
    }
}
