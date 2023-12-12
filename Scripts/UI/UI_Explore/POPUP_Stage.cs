using UnityEngine;
using UnityEngine.UI;

public class POPUP_Stage : MonoBehaviour
{
    [SerializeField] private Transform[] btnStageParents;
    [SerializeField]private Button[] btnsStage;

    public GameObject[] Theme;

    [SerializeField] private GameObject[] popupThema;
    [SerializeField] private Button button_Close;

    public void InitPopupStage()
    {

        DeactiveStage();
        popupThema[Player.Instance.SelectThema - 1].SetActive(true);

        for (int i = 0; i < btnsStage.Length; i++)
        {
            int index = i + 1;
            btnsStage[i].onClick.AddListener(() => SelectStage(index));
            if (i <= Player.Instance.D_PlayerData.clearStage)
            {
                btnsStage[i].interactable = true;
            }
            else
            {
                btnsStage[i].interactable = false;
            }
        }
        button_Close.onClick.RemoveAllListeners();
        button_Close.onClick.AddListener(() => SoundManager.Instance.SfxPlay(Enums.SFX.Button));
    }

    private void SelectStage(int num)
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        Player.Instance.SelectStage = num;
    }

    public void DeactiveStage()
    {
        for (int i = 0; i < btnsStage.Length; i++)
        {
            btnsStage[i].onClick.RemoveAllListeners();
        }

        for (int i = 0; i < popupThema.Length; i++)
        {
            popupThema[i].SetActive(false);
        }
    }
}
