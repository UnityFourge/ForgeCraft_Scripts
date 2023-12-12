using UnityEngine;
using TMPro;

public class CompleteOrderBtn : MonoBehaviour
{
    public GameObject modalOrderComplete;
    public TextMeshProUGUI orderStatusText;
    public TextMeshProUGUI orderDetailText;
    public OrderUI orderUI;

    public void OnClickCompleteOrderButton()
    {
        if (orderUI != null && NPCManager.Instance.NPCObject != null)
        {
            int weaponId = NPCManager.Instance.Npc.weaponId;
            Inventory playerInventory = Player.Instance.inventory;

            if (playerInventory != null)
            {
                Inventory.InventoryItem foundItem = playerInventory.bag.items.Find(item => item.itemID == weaponId);

                if (foundItem != null)
                {
                    int itemValue = DataManager.Instance.GetItem(weaponId).value;
                    GoldManager.Instance.AddGold(itemValue);
                    playerInventory.SubItem(foundItem);

                    if (NPCManager.Instance.NPCObject != null)
                    {
                        NPCSpawner npcSpawner = NPCManager.Instance.NPCSpawner;
                        if (npcSpawner != null)
                        {
                            npcSpawner.NPCCompleted(NPCManager.Instance.NPCObject);
                        }
                    }

                    if (modalOrderComplete != null)
                    {
                        orderStatusText.text = "의뢰 완료!";
                        orderDetailText.text = "보상: " + itemValue + "G";
                        modalOrderComplete.SetActive(true);
                    }
                    SoundManager.Instance.RandomSfxPlay(new Enums.SFX[] 
                    {
                        Enums.SFX.OrderComplete_KingaKinga,
                        Enums.SFX.OrderComplete_ThankYouKinga,
                        Enums.SFX.OrderComplete_YesGoodbye,
                        Enums.SFX.OrderComplete_YesILoveYou,
                        Enums.SFX.OrderComplete_YesSeeYou
                    });

                }
                else
                {
                    if (modalOrderComplete != null)
                    {
                        orderStatusText.text = "의뢰 실패!";
                        orderDetailText.text = "의뢰한 아이템이 인벤토리에 없습니다.";
                        modalOrderComplete.SetActive(true);
                    }
                }
            }
            orderUI.Hide();
        }
    }
}
