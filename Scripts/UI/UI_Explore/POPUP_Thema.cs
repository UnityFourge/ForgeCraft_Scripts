using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POPUP_Thema : MonoBehaviour
{
    [SerializeField] private Transform btnThemaParent;
    private Button[] btnsThema;
    public UI_StageInfo StageInfo;
    [SerializeField] private POPUP_Stage popup_Stage;

    private void Awake()
    {
        btnsThema = btnThemaParent.GetComponentsInChildren<Button>();

        for (int i = 0; i < btnsThema.Length; i++)
        {
            int index = i + 1;
            btnsThema[i].onClick.AddListener(() => 
            {
                SoundManager.Instance.SfxPlay(Enums.SFX.Button);
                SelectThema(index);
                popup_Stage.InitPopupStage();
            });
        }
    }

    private void SelectThema(int num)
    {
        Player.Instance.SelectThema = num;
        StageInfo.Init(num-1);
    }
}
