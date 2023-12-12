using UnityEngine;
using TMPro;

public class GoldUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;

    private void Start()
    {
        if (GoldManager.Instance != null)
        {
            GoldManager.Instance.onGoldChanged += UpdateGoldUI;
            UpdateGoldUI(GoldManager.Instance.CurrentGold); 
        }
    }

    private void UpdateGoldUI(int currentGold)
    {
        goldText.text = $"{currentGold}G";
    }

    private void OnDestroy()
    {
        if (GoldManager.Instance != null)
        {
            GoldManager.Instance.onGoldChanged -= UpdateGoldUI;
        }
    }
}
