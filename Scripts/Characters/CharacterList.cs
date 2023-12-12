using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterList : MonoBehaviour
{
    [System.Serializable]
    public class Character
    {
        public int characterID;
        public Enums.CharacterType characterType;
        public int slotIndex;
        public int lineUPIndex;
        public bool isFormat;

        public int[] equipID = new int[6] { -1, -1, -1, -1, -1, -1 };
        public int[] equipItemRank = new int[6] { -1, -1, -1, -1, -1, -1 };

        public Character(int characterID = -1, Enums.CharacterType characterType = default, int slotIndex = -1, int lineUpIndex = -1,bool isFormat = false)
        {
            this.characterID = characterID;
            this.characterType = characterType;
            this.slotIndex = slotIndex;
            this.lineUPIndex = lineUpIndex;
            this.isFormat = isFormat;
        }

        public void SetEquipID(Enums.ItemType type, int id, int rank)
        {
            int index = (int)type;
            SetEquipID(index, id);
            SetEquipRank(index, rank);
        }

        private void SetEquipID(int index, int id)
        {
            if (id < -1) return;
            if (index < 0 || index > 6) return;

            equipID[index] = id;
        }

        private void SetEquipRank(int index, int rank)
        {
            if (rank < 0 || rank > 3) return;
            if (index < 0 || index > 6) return;

            equipItemRank[index] = rank;
        }
    }

    public class Roster
    {
        public int cntCharacters = 0;
        public List<Character> characters = new List<Character>();
        public Character[] lineup = new Character[4];

        public void Add(Character character)
        {
            characters.Add(character);
            cntCharacters++;
        }
    }

    public Roster roster;
    public int SelectIndexCharacters { get; set; }
    public int SelectIndexLineup { get; set; }
    public int SelectID { get; set; }

    public void Init()
    {
        roster = new Roster();

        for (int i = 0; i < 4; i++)
        {
            roster.lineup[i] = new Character();
        }

        SceneManager.sceneLoaded += TestInitCharacters;
    }

    public void InitIndex()
    {
        SelectIndexCharacters = -1;
        SelectIndexLineup = -1;
        SelectID = -1;
    }

    public void AddCharacter(CharacterSO characterSO)
    {
        var character = roster.characters.Where(x => x.characterID == characterSO.CharacterID).FirstOrDefault();
        if (character != null)
        {
            return;
        }
        else
        {
            roster.Add(new Character(characterSO.CharacterID, characterSO.CharacterType, roster.cntCharacters));
        }
    }

    public void ChangeLineUp(int index, Character character)
    {
        if (index < 0 || index > 3) return;
        if (character == null) return;

        int slotIndex = roster.lineup[index].slotIndex;

        if (slotIndex >= 0)
        {
            Player.Instance.characterList.roster.characters[slotIndex].isFormat = false;
        }

        roster.lineup[index] = character;
    }

    public bool CheckRoster()
    {
        for (int i = 0; i < roster.lineup.Length; i++) 
        {
            if (roster.lineup[i].characterID > 0) return true;
        }

        return false;
    }

    // Test
    public void TestInitCharacters(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Forge" && Player.Instance.InitFlagCharacters)
        {
            Player.Instance.InitFlagCharacters = false;

            AddCharacter(DataManager.Instance.GetCharacter(0));
            AddCharacter(DataManager.Instance.GetCharacter(100));
            AddCharacter(DataManager.Instance.GetCharacter(200));
            AddCharacter(DataManager.Instance.GetCharacter(300));
        }
    }

    // Debug
    public void DebugEquipID()
    {
        int[] equip = roster.characters[SelectIndexCharacters].equipID;

        for (int i = 0; i < equip.Length; i++)
        {
            Debug.Log(equip[i]);
        }
    }
}