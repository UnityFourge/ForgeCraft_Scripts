using UnityEngine;
using UnityEngine.UI;

public class FatigueUI : MonoBehaviour
{
    [SerializeField] Image[] fatigueIcons;

    public void UpdateFatigueIcons()
    {
        if (ForgeManager.Instance.fatigueSystem != null)
        {
            int currentFatigue = ForgeManager.Instance.fatigueSystem.CurrentFatigue;

            for (int i = 0; i < fatigueIcons.Length; i++)
            {
                fatigueIcons[i].enabled = i < currentFatigue;
            }
        }
    }
}
