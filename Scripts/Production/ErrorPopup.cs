using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private Button confirm;

    private Action Oncomfirm;

    void Start()
    {
        confirm.onClick.AddListener(Confirm);
    }
    public void SetErrorPopup(String title, string content,Action oncomfirm)
    {
        titleText.text = title;
        contentText.text = content;
        Oncomfirm = oncomfirm;
        gameObject.SetActive(true);
    }
    void Confirm()
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        if(Oncomfirm != null) 
        {
            Oncomfirm();
        }

        gameObject.SetActive(false);
    }
}