using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Option : UI_Popup
{
    enum OptionButtons
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

        Bind<Button>(typeof(OptionButtons));
        GetButton((int)OptionButtons.Button_Close).gameObject.BindEvent(CloseBtn);
    }

    public void CloseBtn(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ClosePopupUI();
    }
}
