using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI costText;

    [SerializeField] private Button cancleBtn;
    [SerializeField] private Button confirmBtn;

    private Action OnConfirm;

    void Start()
    {
        cancleBtn.onClick.AddListener(Close);
        confirmBtn.onClick.AddListener(Confirm);
    }
    public void SetPopup(string title, string content,string cost, Action onConfirm = null)
    {
        titleText.text = title;
        contentText.text = content;
        costText.text = cost;
        OnConfirm = onConfirm;
    }
    void Confirm() 
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        if (OnConfirm != null)
        {
            OnConfirm();
        }
        Close();
    }
    void Close() 
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        gameObject.SetActive(false);
    }
}
