using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance
    { get { return instance == null ? null : instance; } }

    public CharacterSO[] characterSOs;
    public Dictionary<int, CharacterSO> characters = new Dictionary<int, CharacterSO>();

    public ItemSO[] itemSOs;
    public Dictionary<int, ItemSO> items = new Dictionary<int, ItemSO>();
    public ItemSO SelectedItem;

    public EnemySO[] Enemy;
    public int EnemyType;

    public SkillSO[] Skills;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        for (int i = 0; i < characterSOs.Length; i++)
        {
            characters.Add(characterSOs[i].CharacterID, characterSOs[i]);
        }

        for (int i = 0; i < itemSOs.Length; i++)
        {
            items.Add(itemSOs[i].itemID, itemSOs[i]);
        }
    }

    public EnemySO EnemyInit(int Select)
    {
        EnemySO SelectedEnemy = Enemy[Select];
        return SelectedEnemy; 
    }

    public CharacterSO GetCharacter(int id)
    {
        if (characters.TryGetValue(id, out CharacterSO character))
        {
            return character;
        }
        return null;
    }

    public List<ItemSO> GetWeaponItems()
    {
        return items.Values.Where(item => item.itemType == Enums.ItemType.Weapon).ToList();
    }

    public ItemSO GetItem(int id)
    {
        return items[id];
    }
}
