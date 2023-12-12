using UnityEngine;

public class RejectOrderBtn : MonoBehaviour
{
    public GameObject modalOrderReject;
    public OrderUI orderUI;

    public void OnClickRejectOrderButton()
    {
        if (NPCManager.Instance.NPCObject != null)
        {
            NPCSpawner npcSpawner = NPCManager.Instance.NPCSpawner;
            if (npcSpawner != null)
            {
                npcSpawner.NPCCompleted(NPCManager.Instance.NPCObject);
            }
        }

        if (modalOrderReject != null)
        {
            modalOrderReject.SetActive(true);
            SoundManager.Instance.RandomSfxPlay(new Enums.SFX[]
            {
                Enums.SFX.OrderReject_UpSeo,
                Enums.SFX.OrderReject_UpSeoQuiet,
                Enums.SFX.OrderReject_YourFace
            });
        }
        orderUI.Hide();
    }
}
