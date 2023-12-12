using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Expedition : MonoBehaviour
{
    public POPUP_Expeditions PopupExpeditions { get; set; }

    [SerializeField] private Image image;
    [SerializeField] private Button btnExpedition;
    [SerializeField] private Image imageFormatted;

    private CharacterList.Character character;
    public CharacterList.Character Character
    {
        get 
        {
            //btnExpedition.interactable = !character.isFormat;
            imageFormatted.gameObject.SetActive(character.isFormat);
            return character; 
        }
        set
        {
            character = value;
            if (character != null)
            {
                image.color = new Color(1, 1, 1, 1);
                image.sprite = DataManager.Instance.GetCharacter(character.characterID).CharacterIcon;
                //btnExpedition.interactable = !character.isFormat;
                imageFormatted.gameObject.SetActive(character.isFormat);
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
                btnExpedition.interactable = false;
            }
        }
    }

    private void Start()
    {
        btnExpedition.onClick.AddListener(() =>
        {
            SoundManager.Instance.SfxPlay(Enums.SFX.Button);
            ChangeImageExpedition();
        });
    }

    private void ChangeImageExpedition()
    {
        Player.Instance.characterList.SelectIndexCharacters = character.slotIndex;
        Player.Instance.characterList.SelectID = character.characterID;

        CharacterSO select = DataManager.Instance.GetCharacter(character.characterID);

        PopupExpeditions.ChangeSelectExpedition(select.CharacterSprite, select.CharacterName, select.CharacterClass);
    }
}
