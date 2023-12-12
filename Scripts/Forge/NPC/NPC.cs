using UnityEngine;

public class NPC : MonoBehaviour
{
    public int weaponId;

    public void NPCInit(ItemSO item)
    {
        weaponId = item.itemID;
    }

    }
