using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Gotcha : UI_Popup
{
    enum GotchaButtons
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

        Bind<Button>(typeof(GotchaButtons));
        GetButton((int)GotchaButtons.Button_Close).gameObject.BindEvent(CloseGotcha);
    }

    private void CloseGotcha(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ClosePopupUI();
        GameManager.UI.ShowPopupUI<UI_ExploreSet>();
    }
}
