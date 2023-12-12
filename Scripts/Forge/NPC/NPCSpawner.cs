using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System;

public class NPCSpawner : MonoBehaviour
{
    public List<GameObject> npcPool;
    public Vector3 spawnPoint;
    public List<ItemSO> allItems;
    private int spawnCount = 0;
    private const int maxNPCsPerDay = 5;
    private List<GameObject> activeNPCs = new List<GameObject>();

    private event Action<int> spawnCountChanged;

    private void Start()
    {
        Initialize();
        TrySpawnNPC();
    }

    private void Update()
    {
        
    }

    private void Initialize()
    {
        allItems = DataManager.Instance.GetWeaponItems();
        DeactivateAllNPCs();
        spawnCount = Player.Instance.D_PlayerData.countNPC;
    }


    private void ActivateNPC()
    {
        if (spawnCount < maxNPCsPerDay && npcPool.Count > 0)
        {
            GameObject npcPrefab = npcPool[UnityEngine.Random.Range(0, npcPool.Count)];
            GameObject npcInstance = Instantiate(npcPrefab, spawnPoint, Quaternion.identity, transform);
            npcInstance.SetActive(true); 
            AssignRandomItemToNPC(npcInstance);
            activeNPCs.Add(npcInstance); 
            spawnCount++;
            Player.Instance.D_PlayerData.countNPC = spawnCount - 1;
            SoundManager.Instance.RandomSfxPlay(new Enums.SFX[]
            {
            Enums.SFX.Npc_Hello,
            Enums.SFX.Npc_WhoAreYou,
            Enums.SFX.Npc_YesYes
            });
        }
    }

    public void TrySpawnNPC()
    {
        if (activeNPCs.Count == 0 && spawnCount < maxNPCsPerDay)
        {
            ActivateNPC();
        }
    }


    public int GetSpawnedNPCsCount()
    {
        return spawnCount;
    }

    public int GetMaxNPCsPerDay()
    {
        return maxNPCsPerDay;
    }

    private GameObject GetInactiveNPCFromPool()
    {
        return npcPool.FirstOrDefault(npc => !npc.activeSelf);
    }

    private void AssignRandomItemToNPC(GameObject npcObject)
    {
        ItemSO randomItem = ChooseRandomItem();
        NPC npcComponent = npcObject.GetComponent<NPC>();
        NPCImageController imageController = npcObject.GetComponent<NPCImageController>();

        if (randomItem != null && npcComponent != null && imageController != null)
        {
            npcComponent.NPCInit(randomItem);
            imageController.Initialize(randomItem.itemID);
        }
    }

    public void NPCCompleted(GameObject completedNPC)
    {
        NPCMovement npcMovement = NPCManager.Instance.NPCMovement;
        if (npcMovement != null)
        {
            npcMovement.OnExitComplete += () =>
            {
                DestroyNPC(completedNPC);
                TrySpawnNPC(); 
            };
            npcMovement.WalkOutOfScreen();
        }
        else
        {
            DestroyNPC(completedNPC);
            TrySpawnNPC();
        }
    }

    private void DestroyNPC(GameObject npc)
    {
        activeNPCs.Remove(npc);
        Destroy(npc);
    }



    private ItemSO ChooseRandomItem()
    {
        int forgeLevel = ForgeManager.Instance.ForgeLevel;
        List<ItemSO> filteredItems = allItems.Where(item => item.UseLevel <= forgeLevel).ToList();

        if (filteredItems.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, filteredItems.Count);
            return filteredItems[index];
        }
        return null;
    }

    public void DeactivateAllNPCs()
    {
        foreach (var npc in npcPool)
        {
            npc.SetActive(false);
        }
        spawnCount = 0;
    }
    public List<GameObject> GetActiveNPCs()
    {
        return activeNPCs;
    }

    public void ResetDay()
    {
        StartCoroutine(ResetDayCoroutine());
    }

    private IEnumerator ResetDayCoroutine()
    {
        List<GameObject> npcsToProcess = new List<GameObject>(activeNPCs);

        foreach (var npc in npcsToProcess)
        {
            NPCMovement npcMovement = npc.GetComponent<NPCMovement>();
            if (npcMovement != null)
            {
                npcMovement.WalkOutOfScreen();
                npcMovement.OnExitComplete += () => DestroyNPC(npc);
            }
            else
            {
                DestroyNPC(npc);
            }
            yield return CoroutineHelper.WaitForSeconds(1.0f);
        }
        yield return new WaitUntil(() => activeNPCs.Count == 0);

        spawnCount = 0;
        Player.Instance.D_PlayerData.countNPC = spawnCount;
        yield return CoroutineHelper.WaitForSeconds(3.0f);

        TrySpawnNPC();
    }
}


