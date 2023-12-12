using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    // Start is called before the first frame update
    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        //Spawn();
        //StageManager.Instance.Spawn();
        //SkillManager.Instance.Init();
    }

    public void Spawn()
    {
        for (int i = 0; i < 4; i++)
        {
            CharacterList.Character character = Player.Instance.characterList.roster.lineup[i];
            if (character.characterID < 0) continue;

            GameObject expedition = null;

            Enums.CharacterType type = character.characterType;
            
            switch (type)
            {
                case Enums.CharacterType.Melee:
                case Enums.CharacterType.Range:
                    expedition = CharacterManager.Instance.Get(character.characterID);
                    break;
                default:
                    return;
            }
            
            expedition.transform.position = spawnPoint[i+1].transform.position;
            expedition.GetComponent<Expedition>().Init(DataManager.Instance.GetCharacter(character.characterID), character.equipID);
        }
    }
}
