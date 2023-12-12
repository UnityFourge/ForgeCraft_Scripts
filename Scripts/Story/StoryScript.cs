using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryScript : MonoBehaviour
{
    public GameObject[] StoryScripts;
    public GameObject ChatBox;
    public Button NextScript;
    public GameObject[] Characters;
    public DialogueScript[] Scripts;

    [SerializeField] private TextMeshProUGUI Chat;
    [SerializeField] private int chatLog;
    private int characterCount;
    private int StoryNum;
    private void Awake()
    {
        StoryNum = Player.Instance.D_PlayerData.storyScene;
        Chat = ChatBox.GetComponentInChildren<TextMeshProUGUI>();
        chatLog = 0;
        characterCount = Characters.Length;
        NextScript.onClick.AddListener(() =>
        {
            SoundManager.Instance.SfxPlay(Enums.SFX.Button);
            Chatting();
        });
    }

    void Start()
    {
        Chatting();
    }

    public void Chatting()
    {
        int curCharacter = Scripts[chatLog].ID % characterCount;
        for(int i = 0; i< characterCount; i++)
        {
            Characters[i].SetActive(i == curCharacter);
        }
        Chat.text = Scripts[chatLog].Dialogue;
        chatLog++;
        if(chatLog >= Scripts.Length)
        {
            CloseStory();
        }
    }

    public void CloseStory()
    {
        if (Player.Instance.D_PlayerData.storyScene > 3 && Player.Instance.D_PlayerData.storyScene <= 6)
        {
            MySceneManager.Instance.ChangeScene("Battle");
        }
        else if (Player.Instance.D_PlayerData.storyScene < 3)
        {
            MySceneManager.Instance.ChangeScene("Story");
        }
        else if (Player.Instance.D_PlayerData.storyScene == 3)
        {
            MySceneManager.Instance.ChangeScene("Forge");
        }else if (Player.Instance.D_PlayerData.storyScene > 6)
        {
            MySceneManager.Instance.ChangeScene("Start");
        }

    }

    [System.Serializable]
    public class DialogueScript
    {
        public int ID;
        [TextArea]
        public string Dialogue;
    }
}
