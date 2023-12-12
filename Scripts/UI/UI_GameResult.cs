using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_GameResult : MonoBehaviour
{
    public GameObject ResultWindow;
    public GameObject GameResult;
    private TextMeshProUGUI ResultText; 
    public GameObject[] ResultSlots; 
    public GameObject NextStageBtn;

    private Enemy _enemy;
    private Expedition[] _expeditions;
    private Image[] ResultSpritesImage;

    private bool[] IsPlayed;

    private void Awake()
    {
        _enemy = CharacterManager.Instance.Enemy;
        _expeditions = CharacterManager.Instance.Expeditions;
        ResultText = GameResult.GetComponent<TextMeshProUGUI>();
        ResultSpritesImage = new Image[ResultSlots.Length];
        for(int i = 0; i < ResultSpritesImage.Length; i++)
        {
            ResultSpritesImage[i] = ResultSlots[i].GetComponent<Image>();
        }
    }

    public void ResultInit()
    {
        ResultWindow.SetActive(true);
        ResultText.text = VictoryOrLose();
        IsPlayed = new bool[4];
        for (int i = 0; i < IsPlayed.Length; i++)
        {
            IsPlayed[i] = Player.Instance.D_PlayerData.storyPlayed[i + 3];
        }
    }

    public void Result_Win()
    {
        for (int i = 0; i < ResultSpritesImage.Length; i++)
        {
            if (i < _enemy.EnemyDrops.Length)
            {
                ResultSpritesImage[i].sprite = _enemy.EnemyDrops[i].itemSprite;

            }
            else
            {
                ResultSpritesImage[i].gameObject.SetActive(false);
            }
        }
    }

    public void Result_Lose()
    {
        for (int i = 0; i < ResultSpritesImage.Length; i++)
        {
            ResultSpritesImage[i].gameObject.SetActive(false) ;
        }
        NextStageBtn.SetActive(false );
    }
    public string VictoryOrLose()
    {
        string result = " ";
        if(_enemy.EnemyHealth <= 0)
        {
            result = "V I C T O R Y!!!";
            Result_Win();
        }
        else
        {
            result = "ㅜLOSEㅜ";
            Result_Lose();
        }
        return result;
    }

    public void ToForge()
    {
        this.gameObject.SetActive(false);
        AddDrops();
        MySceneManager.Instance.ChangeScene("Forge");
    }

    public void ToNextStage()
    {
        this.gameObject.SetActive(false);
        Player.Instance.SelectStage += 1;
        AddDrops();
        if (Player.Instance.D_PlayerData.clearStage % 5 == 0)
        {
            MySceneManager.Instance.ChangeScene("Story");
        }
        else
        {
            MySceneManager.Instance.ChangeScene("Battle");
        }
    }

    public void AddDrops()
    {
        for (int i = 0; i < _enemy.EnemyDrops.Length; i++)
        {
            Player.Instance.inventory.AddItem(_enemy.EnemyDrops[i]);
        }
    }

    public void AddDropItem()
    {
        for (int i = 0; i < _enemy.EnemyDrops.Length; i++)
        {
            ItemSO drops = _enemy.EnemyDrops[i];
            Player.Instance.inventory.AddItem(drops);
        }
    }
}
