using TMPro;
using UnityEngine;

public class UpgradeToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Content;
    [SerializeField] private TextMeshProUGUI LevelText;
    public void SetToolTip( string content,string maxLevel) 
    {
        Content.text = content;
        LevelText.text = maxLevel;

    }
}
