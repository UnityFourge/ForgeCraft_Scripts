using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SetNPCDatas : MonoBehaviour
{
    public OrderUI orderUI;

    public void InitializeNPC(GameObject npcObject)
    {
        NPC npcComponent = npcObject.GetComponent<NPC>();

        if (npcComponent != null)
        {
            SetImagesInOrderUI(npcComponent.weaponId);
        }
    }

    private void SetImagesInOrderUI(int weaponId)
    {
        ItemSO weaponData = DataManager.Instance.GetItem(weaponId);

        if (weaponData != null && weaponData.itemSprite != null)
        {
            orderUI.SetOrderImage(weaponData.itemSprite);
        }
    }
}

