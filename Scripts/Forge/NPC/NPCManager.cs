using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private static NPCManager instance;
    public static NPCManager Instance
    { 
        get 
        { 
            return instance; 
        } 
    }

    public GameObject NPCObject { get; private set; }
    public NPCMovement NPCMovement { get; private set; }
    public NPCSpawner NPCSpawner { get; private set; }

    public bool IsArrived { get; set; }

    public NPC Npc {  get; private set; }

    private void Awake()
    {
        instance = this;

        NPCSpawner = GetComponent<NPCSpawner>();
    }

    public void SetNPCObject(GameObject npcObject)
    {
        if (npcObject == null) return;

        NPCObject = npcObject;
    }

    public void SetNPC(NPC npc)
    {
        if (npc == null) return;

        Npc = npc;
    }

    public void SetNPCMovement(NPCMovement npcMovement)
    {
        if (npcMovement == null) return;

        NPCMovement = npcMovement;
    }

    public void SetNPCSpawner(NPCSpawner npcSpawner)
    {
        if (npcSpawner == null) return;

        NPCSpawner = npcSpawner;
    }
}
