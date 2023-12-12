using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Battle : UI_Scene
{
    enum BattleButtons
    {
        Button_Pause,
    }

    private void Start()
    {
        Init();

    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(BattleButtons));
        GetButton((int)BattleButtons.Button_Pause).gameObject.BindEvent(PauseBtn);

    }

    public void PauseBtn(PointerEventData data)
    {
        GameManager.UI.ShowPopupUI<UI_Pause>();
        Time.timeScale = 0f;
    }
}
