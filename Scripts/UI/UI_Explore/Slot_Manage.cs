using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot_Manage : MonoBehaviour
{
    [SerializeField] public POPUP_Manage PopupManage { get; set; }
    [SerializeField] public UI_EquipInventory EquipInventory { get; set; }

    [SerializeField] private Button btnManage;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text_Name;
    [SerializeField] private TextMeshProUGUI text_Class;

    CharacterSO characterSO;
    ItemSO[] itemSO;

    private CharacterList.Character character;
    public CharacterList.Character Character
    {
        get
        {
            return character;
        }
        set
        {
            character = value;
            SetCharacter(character);
        }
    }

    private void Awake()
    {
        btnManage.onClick.AddListener(() => {
            SoundManager.Instance.SfxPlay(Enums.SFX.Button);
            EquipInventory.gameObject.SetActive(true);
            InitSelectExpedition();
        });
    }

    public void InitSelectExpedition()
    {
        InitItemSOs();
        ChangeSelectExpedition();
        EquipInventory.FreshWeaponSlot();
    }

    private void ChangeSelectExpedition()
    {
        characterSO = DataManager.Instance.GetCharacter(character.characterID);

        Player.Instance.characterList.SelectIndexCharacters = character.slotIndex;
        Player.Instance.characterList.SelectID = character.characterID;

        PopupManage.ChangeImageExpedition(characterSO);
        PopupManage.ChangeImageEquip(itemSO);
        PopupManage.ChanageTextStatus(MakeStatusArray());
    }

    private void SetCharacter(CharacterList.Character character)
    {
        if (character != null)
        {
            characterSO = DataManager.Instance.GetCharacter(character.characterID);

            image.color = new Color(1, 1, 1, 1);
            image.sprite = characterSO.CharacterIcon;
            text_Name.text = characterSO.CharacterName;
            text_Class.text = characterSO.CharacterClass;
        }
        else
        {
            image.color = new Color(1, 1, 1, 0);
            text_Name.text = null;
            text_Class.text = null;
            btnManage.interactable = false;
        }
    }

    private float[] MakeStatusArray()
    {
        float[] status = new float[5];

        status[0] = characterSO.BaseHealth;
        status[1] = characterSO.BaseAttack;
        status[2] = characterSO.BaseAttackDelay;
        status[3] = characterSO.BaseDefence;
        status[4] = characterSO.BaseAttackRange;

        return status;
    }

    private void InitItemSOs()
    {
        itemSO = new ItemSO[character.equipID.Length];

        EquipInventory.InitStatusEquip();

        for (int i = 0; i < character.equipID.Length; i++)
        {
            if (character.equipID[i] >= 0)
            {
                itemSO[i] = DataManager.Instance.GetItem(character.equipID[i]);
                EquipInventory.InitStatusEquip(itemSO[i].itemStatus);
            }
            else
            {
                itemSO[i] = null;
            }
        }
    }
}
