using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class POPUP_Expeditions : MonoBehaviour
{
    [SerializeField] private Slot_Expedition slotPrefab;
    [SerializeField] private Transform slotParent_Expedition;
    [SerializeField] private Slot_Expedition[] slots_Expedition;

    [SerializeField] private Image imageExpedition;
    [SerializeField] private TextMeshProUGUI textExpeditionName;
    [SerializeField] private TextMeshProUGUI textExpeditionClass;
    [SerializeField] public Button btnClose;
    [SerializeField] public Button btnClear;
    [SerializeField] private Button btnFormat;

    [SerializeField] private GameObject popupFormation;

    private int len_characters;
    private List<Slot_Expedition> pool;

    private void Awake()
    {
        slots_Expedition = slotParent_Expedition.GetComponentsInChildren<Slot_Expedition>();

        btnClose.onClick.AddListener(() =>
        {
            SoundManager.Instance.SfxPlay(Enums.SFX.Button);
            Player.Instance.characterList.InitIndex();
            InitSelectedExpedition();
        });

        pool = new List<Slot_Expedition>();
    }

    private void Start()
    {
        btnFormat.onClick.AddListener(() => OnClickFormat());
        btnClear.onClick.AddListener(() => OnClickClear());
    }

    private void OnEnable()
    {
        FreshSlot();
    }

    private void Init()
    {
        for (int i = 0; i < slots_Expedition.Length; i++)
        {
            slots_Expedition[i].gameObject.SetActive(false);
        }

        len_characters = Player.Instance.characterList.roster.characters.Count;
        slots_Expedition = new Slot_Expedition[len_characters];

        for (int i = 0; i <  len_characters; i++)
        {
            slots_Expedition[i] = Get();
            slots_Expedition[i].PopupExpeditions = this;
        }
    }

    private Slot_Expedition Get()
    {
        Slot_Expedition select = null;

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeSelf)
            {
                select = pool[i];
                select.gameObject.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(slotPrefab, slotParent_Expedition);
            pool.Add(select);
        }

        return select;
    }

    public void FreshSlot()
    {
        Init();

        for (int i = 0; i < len_characters; i++)
        {
            slots_Expedition[i].Character = Player.Instance.characterList.roster.characters[i];
        }
    }

    public void ChangeSelectExpedition(Sprite characterSprite, string characterName, string characterClass)
    {
        imageExpedition.color = Color.white;
        imageExpedition.sprite = characterSprite;
        textExpeditionName.text = characterName;
        textExpeditionClass.text = characterClass;
    }

    private void OnClickFormat()
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        CharacterList characterList = Player.Instance.characterList;

        int indexCharacters = characterList.SelectIndexCharacters;
        int indexLineup = characterList.SelectIndexLineup;
        int id = characterList.SelectID;

        if (indexCharacters < 0 || indexLineup < 0 || id < 0) return;

        bool isFormat = characterList.roster.characters[indexCharacters].isFormat;
        if (isFormat)
        {
            characterList.ChangeLineUp(characterList.roster.characters[indexCharacters].lineUPIndex, new CharacterList.Character());
        }

        characterList.roster.characters[indexCharacters].lineUPIndex = indexLineup;
        characterList.roster.characters[indexCharacters].isFormat = true;
        characterList.ChangeLineUp(indexLineup, characterList.roster.characters[indexCharacters]);
        characterList.InitIndex();
        InitSelectedExpedition();
        this.gameObject.SetActive(false);
        popupFormation.SetActive(true);
    }

    private void OnClickClear()
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        CharacterList characterList = Player.Instance.characterList;

        int indexCharacters = characterList.SelectIndexCharacters;
        int indexLineup = characterList.SelectIndexLineup;

        if (indexCharacters < 0) return;

        characterList.roster.characters[indexCharacters].lineUPIndex = -1;
        characterList.roster.characters[indexCharacters].isFormat = false;
        characterList.ChangeLineUp(indexLineup, new CharacterList.Character());
        characterList.InitIndex();
        InitSelectedExpedition();
        this.gameObject.SetActive(false);
        popupFormation.SetActive(true);
    }

    private void InitSelectedExpedition()
    {
        imageExpedition.color = new Color(1, 1, 1, 0);
        textExpeditionName.text = "";
        textExpeditionClass.text = "";
    }
}
