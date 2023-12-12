using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Popup_Start : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text_Title;
    [SerializeField] TextMeshProUGUI text_Desc;
    [SerializeField] Button button_Confirm;
    [SerializeField] Button button_Cancel;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        button_Cancel.onClick.AddListener(() => SoundManager.Instance.SfxPlay(Enums.SFX.Button));
    }
     
    public void SetPopup(string title, string desc, UnityAction action = default, bool cancel = false)
    {
        text_Title.text = title;
        text_Desc.text = desc;
        button_Confirm.onClick.RemoveAllListeners();
        button_Confirm.onClick.AddListener(() => SoundManager.Instance.SfxPlay(Enums.SFX.Button));
        if (action != default ) 
        { 
            button_Confirm.onClick.AddListener(action); 
        }
        button_Cancel.gameObject.SetActive(cancel);
    }
}
