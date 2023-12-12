using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Gotcha : UI_Popup
{
    public List<PickUpTable> Table = new List<PickUpTable>();
    public List<PickUpTable> pick = new List<PickUpTable>();
    public List<PickUpTable> duplication = new List<PickUpTable>();
    public int total = 0;
    public GotchaResult[] Resulticons;
    public GameObject[] Gold;
    public int totalGold;

    public GameObject FirstScreen;
    public GameObject SecondScreen;
    public GameObject ResultScreen;

    [SerializeField] private Button button_DrawOne;
    [SerializeField] private Button button_DrawTen;
    [SerializeField] private Button button_Accept;

    private void Awake()
    {
        button_DrawOne.onClick.AddListener(() =>
        {
            SoundManager.Instance.SfxPlay(Enums.SFX.Button);
            RepeatRandom(1);
        });

        button_DrawTen.onClick.AddListener(() =>
        {
            SoundManager.Instance.SfxPlay(Enums.SFX.Button);
            RepeatRandom(10);
        });

        button_Accept.onClick.AddListener(() =>
        {
            SoundManager.Instance.SfxPlay(Enums.SFX.Button);
            AcceptCharacter();
            GameManager.UI.ClosePopupUI();
        });

        totalGold = 0;
    }

    private void Start()
    {
        for(int i = 0; i < Table.Count; i++)
        {
            total += Table[i].weight;
        }
    }

    public void CheckDuplication(PickUpTable table, int i)
    {
        var check = Player.Instance.characterList.roster.characters.Where(x => x.characterID == table.Character.CharacterID).FirstOrDefault();

        if (check != null)
        {
            Resulticons[i].GoldInit(table, Gold[i]);
            totalGold += Resulticons[i].ResultGold;
        }
        else
        {
            Resulticons[i].CharacterInit(table, Gold[i]);
            Player.Instance.characterList.AddCharacter(table.Character);
        }
    }
    public PickUpTable RandomTable()
    {
        int weight = 0;
        int temp = 0;
        temp = Mathf.RoundToInt(total * Random.Range(0f, 1.0f));

        for (int i = 0; i < Table.Count; i++)
        {
            weight += Table[i].weight;
            if (temp <= weight)
            {
                PickUpTable table = new PickUpTable(Table[i]);
                return table;
            }
        }
        return null;
    }
    
    public bool CheckGotcha(int temp)
    {
        bool IsOkay = false;
        int curGold = GoldManager.Instance.CurrentGold;
        if (temp == 1 && curGold < 100)
        {
            ForgeManager.Instance.ShowErrorPopup("뽑기 실패", "골드가 부족합니다.", null);
        }
        else if (temp == 10 && curGold < 1000)
        {
            ForgeManager.Instance.ShowErrorPopup("뽑기 실패", "골드가 부족합니다.", null);
        }
        else
        {
            IsOkay = true;
        }
        return IsOkay;
    }

    public void RepeatRandom(int temp)
    {
        bool IsOkay = CheckGotcha(temp);
        if (IsOkay)
        {
            FirstScreen.SetActive(false);
            SecondScreen.SetActive(true);
            int curGold = GoldManager.Instance.CurrentGold;
            PickUpTable select = null;
            for (int i = 0; i < temp; i++)
            {
                GoldManager.Instance.SubtractGold(100);
                select = RandomTable();
                pick.Add(select);
            }
        }
    }

    public void ShowResult()
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        SecondScreen.SetActive(false);
        ResultScreen.SetActive(true);

        for(int i = 0; i<Resulticons.Length; i++)
        {
            if (i < pick.Count)
            {
                CheckDuplication(pick[i], i);
            }
            else
            {
                Resulticons[i].gameObject.SetActive(false);
                Gold[i].SetActive(false);
            }
        }
    }

    public void AcceptCharacter()
    {
        ResultScreen.SetActive(false );

        GoldManager.Instance.AddGold(totalGold);
    }
}
