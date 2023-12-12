using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Modal_ForgePause : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Button button_Confirm;
    [SerializeField] private Button button_Cancel;

    private void Awake()
    {
        button_Cancel.onClick.AddListener(() => this.gameObject.SetActive(false));
    }

    public void SetModal(string info, UnityAction confirm)
    {
        infoText.text = info;
        button_Confirm.onClick.RemoveAllListeners();
        button_Confirm.onClick.AddListener(() => 
        {
            this.gameObject.SetActive(false);
        });
        button_Confirm.onClick.AddListener(confirm);
    }
}
