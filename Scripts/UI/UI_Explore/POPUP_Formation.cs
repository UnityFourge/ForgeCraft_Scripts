using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class POPUP_Formation : MonoBehaviour
{
    [SerializeField] private Button[] btnsFormation;
    [SerializeField] private Image[] imagesExpeditions;
    [SerializeField] private POPUP_Expeditions popupExpeditions;
    [SerializeField] private GameObject popupStart;

    [SerializeField] private Sprite imageEmpty;
    [SerializeField] private Button button_Close;
    [SerializeField] private Button button_Start;

    private bool[] IsPlayed;
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < btnsFormation.Length; i++)
        {
            int index = i;

            btnsFormation[i].onClick.AddListener(() =>
            {
                SoundManager.Instance.SfxPlay(Enums.SFX.Button);
                Player.Instance.characterList.SelectIndexLineup = index;
                OpenExpeditions(index);
            });
        }

        button_Close.onClick.AddListener(() => SoundManager.Instance.SfxPlay(Enums.SFX.Button));
        button_Start.onClick.AddListener(() =>
        {
            SoundManager.Instance.SfxPlay(Enums.SFX.Button);
            StartBattle();
        });

        IsPlayed = new bool[4];
        for (int i = 0; i < IsPlayed.Length; i++)
        {
            IsPlayed[i] = Player.Instance.D_PlayerData.storyPlayed[i + 3];
        }
    }

    private void OnEnable()
    {
        ChangeImages();
    }

    private void OpenExpeditions(int index)
    {
        Player.Instance.characterList.SelectIndexCharacters = Player.Instance.characterList.roster.lineup[index].slotIndex;

        this.gameObject.SetActive(false);
        popupExpeditions.gameObject.SetActive(true);
    }

    public void ChangeImages()
    {
        for (int i = 0; i < 4; i++)
        {
            int selectID = Player.Instance.characterList.roster.lineup[i].characterID;

            if (selectID < 0)
            {
                if (imagesExpeditions[i].sprite != null) imagesExpeditions[i].sprite = imageEmpty;
                continue;
            }

            imagesExpeditions[i].sprite = DataManager.Instance.GetCharacter(selectID).CharacterSprite;
        }
    }

    public void StartBattle()
    {
        if (Player.Instance.characterList.CheckRoster())
        {
            if(Player.Instance.SelectStage % 5 == 1)
            {
                if (!IsPlayed[Player.Instance.D_PlayerData.clearStage / 5] && Player.Instance.SelectStage > Player.Instance.D_PlayerData.clearStage)
                {
                    MySceneManager.Instance.ChangeScene("Story");
                }
                else
                {
                    MySceneManager.Instance.ChangeScene("Battle");
                }
            }
            else
            {
                MySceneManager.Instance.ChangeScene("Battle");
            }
        }
        else
        {
            popupStart.SetActive(true);
        }
    }
}
