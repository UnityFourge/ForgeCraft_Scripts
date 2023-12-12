using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_ForgePause : UI_Popup
{
    enum ForgePauseButtons
    {
        Button_Setting,
        Button_Continue,
        Button_Title,
        Button_Quit,
    }

    [SerializeField] private Modal_ForgePause modal_ForgePause;

    private void Start()
    {
        Init();

    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(ForgePauseButtons));
        GetButton((int)ForgePauseButtons.Button_Setting).gameObject.BindEvent(SettingBtn);
        GetButton((int)ForgePauseButtons.Button_Continue).gameObject.BindEvent(ContinueBtn);
        GetButton((int)ForgePauseButtons.Button_Title).gameObject.BindEvent(TitleBtn);
        GetButton((int)ForgePauseButtons.Button_Quit).gameObject.BindEvent(QuitBtn);
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

    public void TitleBtn(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        string info = "타이틀 화면으로 돌아가시겠습니까?\n(저장되지 않은 정보는 모두 초기화됩니다.)";
        modal_ForgePause.gameObject.SetActive(true);
        modal_ForgePause.SetModal(info, () => SceneManager.LoadScene("Start"));
    }

    public void QuitBtn(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        string info = "게임을 종료하시겠습니까?\n(저장되지 않은 정보는 모두 초기화됩니다.)";
        modal_ForgePause.gameObject.SetActive(true);
        modal_ForgePause.SetModal(info, () => Application.Quit());
    }
}
