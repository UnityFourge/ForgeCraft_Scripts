using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager instance;
    public static CharacterManager Instance
    { 
        get { return instance == null ? null : instance; } 
    }

    public PoolManager pool;

    public int[] prefabKeys;
    public GameObject[] prefabValues;
    private Dictionary<int, GameObject> prefabs;

    public List<GameObject>[] expeditions;

    public EnemyBehavior Enemy { get; set; }
    public Expedition[] Expeditions { get; set; }

    private void Awake()
    {
        instance = this;

        prefabs = new Dictionary<int, GameObject>();
        for (int i = 0; i < prefabKeys.Length; i++)
        {
            prefabs.Add(prefabKeys[i], prefabValues[i]);
        }
    }

    public void Init()
    {
        Enemy = GetComponentInChildren<EnemyBehavior>();
        Expeditions = GetComponentsInChildren<Expedition>();
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
        }

        return select;
    }
}
