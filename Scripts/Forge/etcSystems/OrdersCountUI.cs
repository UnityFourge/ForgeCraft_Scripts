using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrdersCountUI : MonoBehaviour
{
    private NPCSpawner npcSpawner;
    [SerializeField] TextMeshProUGUI ordersCountText;

    private void Awake()
    {
        npcSpawner = FindObjectOfType<NPCSpawner>();
    }

    private void Update()
    {
        int maxOrders = npcSpawner.GetMaxNPCsPerDay();
        int spawnedCount = npcSpawner.GetSpawnedNPCsCount();
        int remainingOrders = maxOrders - spawnedCount;
        ordersCountText.text = remainingOrders.ToString();
    }
}
