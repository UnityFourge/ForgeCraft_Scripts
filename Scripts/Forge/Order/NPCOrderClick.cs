using UnityEngine;
using UnityEngine.UI;

public class NPCOrderClick : MonoBehaviour
{
    [SerializeField] OrderUI orderUI;
    [SerializeField] Button npcInteractionButton; 

    void Start()
    {
        if (orderUI != null) orderUI.gameObject.SetActive(false);

        if (npcInteractionButton != null)
        {
            npcInteractionButton.onClick.AddListener(OnNPCInteractionButtonClicked);
            npcInteractionButton.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("NPCInteractionButton is not found.");
        }
    }

    private void Update()
    {
        npcInteractionButton.gameObject.SetActive(NPCManager.Instance.IsArrived);
    }

    private void OnNPCInteractionButtonClicked()
    {
        NPC npcComponent = NPCManager.Instance.Npc;
        NPCManager.Instance.SetNPCObject(npcComponent.gameObject);
        if (npcComponent != null && orderUI != null)
        {
            int weaponId = npcComponent.weaponId;
            ItemSO weaponData = DataManager.Instance.GetItem(weaponId);
            if (weaponData != null)
            {
                orderUI.SetOrderDetails(weaponData.itemName, weaponData.value);
                orderUI.Show(weaponId);
            }
        }
    }
}
