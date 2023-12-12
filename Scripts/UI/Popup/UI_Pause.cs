using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Pause : UI_Popup
{
    enum PauseButtons
    {
        Button_Setting,
        Button_Continue,
        Button_ReStart,
        Button_GiveUp,
    }

    private void Start()
    {
        Init();

    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(PauseButtons));
        GetButton((int)PauseButtons.Button_Setting).gameObject.BindEvent(SettingBtn);
        GetButton((int)PauseButtons.Button_Continue).gameObject.BindEvent(ContinueBtn);
        GetButton((int)PauseButtons.Button_ReStart).gameObject.BindEvent(ReStartBtn);
        GetButton((int)PauseButtons.Button_GiveUp).gameObject.BindEvent(GiveUpBtn);
    }

    public void SettingBtn(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ShowPopupUI<UI_Option>();
    }

    public void ContinueBtn(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        Time.timeScale = 1f;
        GameManager.UI.ClosePopupUI();
    }

    public void ReStartBtn(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GiveUpBtn(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Forge");
    }
}
