using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_OrderComplete : UI_Popup
{
    enum OrderCompleteButtons
    {
        Button_Complete,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(OrderCompleteButtons));
        GetButton((int)OrderCompleteButtons.Button_Complete).gameObject.BindEvent(ClosePopup);
    }

    private void ClosePopup(PointerEventData data)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        GameManager.UI.ClosePopupUI();
    }
}
