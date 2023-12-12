using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Start : UI_Scene
{
    enum StartBtns
    {
        GameStartBtn,
        LoadGameBtn,
        OptionBtn,
        TeamBtn,
        ExitBtn,
    }

    [SerializeField] Popup_Start popup_Start;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(StartBtns));

        GetButton((int)StartBtns.GameStartBtn).gameObject.BindEvent(OnStartGame);
        GetButton((int)StartBtns.LoadGameBtn).gameObject.BindEvent(OnLoadGame);
        GetButton((int)StartBtns.OptionBtn).gameObject.BindEvent(OnPopupOption);
        GetButton((int)StartBtns.TeamBtn).gameObject.BindEvent(OnPopupTeam);
        GetButton((int)StartBtns.ExitBtn).gameObject.BindEvent(OnExitGame);
    }

    public void OnStartGame(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        UnityAction action = () =>
        {
            Player.Instance.Init();
            Player.Instance.InitFlagCharacters = true;
            Player.Instance.InitFlagInventory = true;
            GameManager.Instance.ResetAllTutorials();
            if (Player.Instance.D_PlayerData.storyPlayed[0])
            {
                MySceneManager.Instance.ChangeScene("Forge");
            }
            else
            {
                MySceneManager.Instance.ChangeScene("Story");
            }
        };

        popup_Start.gameObject.SetActive(true);
        popup_Start.SetPopup("주의", "새로 게임을 시작하고 저장을 하게 되면 \n기존의 저장 정보는 사라지게 됩니다.\n\n새로 게임을 시작하시겠습니까?", action, true);
    }

    private void OnLoadGame(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        if (Player.Instance.LoadGameData())
        {
            SceneManager.LoadScene("Forge");
        }
        else
        {
            popup_Start.gameObject.SetActive(true);
            popup_Start.SetPopup("불러오기 실패", "불러올 저장 정보가 없습니다.");
        }
    }

    private void OnPopupTeam(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ShowPopupUI<UI_Team>();
    }

    private void OnPopupOption(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ShowPopupUI<UI_Option>();
    }

    private void OnExitGame(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        popup_Start.gameObject.SetActive(true);
        popup_Start.SetPopup("주의", "게임을 종료하시겠습니까?", () => Application.Quit(), true);
    }
}
