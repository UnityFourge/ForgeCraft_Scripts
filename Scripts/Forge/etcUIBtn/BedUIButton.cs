using UnityEngine;
using UnityEngine.UI;

public class BedUIButton : MonoBehaviour
{
    private NPCSpawner npcSpawner;
    public GameObject popup_Rest;

    [SerializeField] FatigueUI fatigueUI;

    [SerializeField] private Button confirmBtn;
    [SerializeField] private Button cancelBtn;

    private void Start()
    {
        npcSpawner = NPCManager.Instance.NPCSpawner;
    }

    public void OnClickRestButton()
    {
        Debug.Log("REST");

        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        if (npcSpawner != null)
        {
            NPCMovement npcMovement = NPCManager.Instance.NPCMovement;
            if (npcMovement != null)
            {
                SoundManager.Instance.SfxPlay(Enums.SFX.Etc_ImSleepSugo);
                npcMovement.WalkOutOfScreen();
            }
            npcSpawner.ResetDay(); 
        }
        ForgeManager.Instance.fatigueSystem.ResetFatigue();
        ForgeManager.Instance.fatigueSystem.UpdateUI();

        if (popup_Rest != null)
        {
            popup_Rest.SetActive(false);
        }
    }

    public void PopUPBedUI()
    {
        if (popup_Rest != null)
        {
            popup_Rest.SetActive(true);
        }
    }
    
    public void CloseBedUI()
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        if (popup_Rest != null)
        {
            popup_Rest.SetActive(false);
        }
    }
}
