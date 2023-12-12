using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager s_Instance;
    public static GameManager Instance { get { Init(); return s_Instance; } }

    ResourceManager _resource = new ResourceManager();
    SceneManagerEX _scene = new SceneManagerEX();
    UIManager _ui = new UIManager();

    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEX Scene { get { return Instance._scene; } }
    public static UIManager UI { get { return Instance._ui; } }

    public CharacterManager characterManager;


    private void Start()
    {
        Init();
    }

    static void Init()
    {
        if(s_Instance == null)
        {
            GameObject go = GameObject.Find("@GameManager");
            if(go == null)
            {
                go = new GameObject { name = "@GameManager" };
                go.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(go);
            s_Instance = go.GetComponent<GameManager>();
        }
    }
    public bool HasTutorialBeenShown(int tutorialIndex)
    {
        if (tutorialIndex >= 0 && tutorialIndex < Player.Instance.D_PlayerData.tutorialPlayed.Length)
        {
            return Player.Instance.D_PlayerData.tutorialPlayed[tutorialIndex];
        }
        return false;
    }

    public void SetTutorialShown(int tutorialIndex)
    {
        if (tutorialIndex >= 0 && tutorialIndex < Player.Instance.D_PlayerData.tutorialPlayed.Length)
        {
            Player.Instance.D_PlayerData.tutorialPlayed[tutorialIndex] = true;
        }
    }

    public void ResetAllTutorials()
    {
        for (int i = 0; i < Player.Instance.D_PlayerData.tutorialPlayed.Length; i++)
        {
            Player.Instance.D_PlayerData.tutorialPlayed[i] = false;
        }
    }

}
