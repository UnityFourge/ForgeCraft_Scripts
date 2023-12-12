using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public GameObject[] StoryLines;

    private void OnEnable()
    {
        PlayStory();
    }

    public void PlayStory()
    {
        if (!Player.Instance.D_PlayerData.storyPlayed[Player.Instance.D_PlayerData.storyScene])
        {
            StoryLines[Player.Instance.D_PlayerData.storyScene].SetActive(true);
            Player.Instance.D_PlayerData.storyPlayed[Player.Instance.D_PlayerData.storyScene] = true;
            Player.Instance.D_PlayerData.storyScene++;
        }
    }
}
