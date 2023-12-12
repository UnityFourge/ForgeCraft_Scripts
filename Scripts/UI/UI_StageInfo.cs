using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StageInfo : MonoBehaviour
{
    public GameObject[] EnemyInfo;
    public GameObject[] ItemInfo;
    public ThemeSO[] Themes;
    public GameObject StageName;

    private Image[] EnemySprite;
    private Image[] ItemSprite;

    private void Awake()
    {
        EnemySprite = new Image[EnemyInfo.Length];
        ItemSprite = new Image[ItemInfo.Length];
        for (int i = 0; i < EnemyInfo.Length; i++)
        {
            EnemySprite[i] = EnemyInfo[i].GetComponent<Image>();
            ItemSprite[i] = ItemInfo[i].GetComponent<Image>();
        }
    }
    public void Init(int num)
    {
        this.gameObject.SetActive(true);

        int temp = num;
        ThemeSO selectTheme = Themes[temp];

        StageName.GetComponent<Text>().text = selectTheme.StageName;
        for(int i = 0; i < EnemyInfo.Length; i++)
        {
            if (i < selectTheme.Appear_Enemy.Length)
            {
                EnemySprite[i].sprite = selectTheme.Appear_Enemy[i].EnemySprite;
            }
            else
            {
                EnemySprite[i].gameObject.SetActive(false);
            }
            if(i<selectTheme.Drop_Items.Length)
            {
                ItemSprite[i].sprite = selectTheme.Drop_Items[i].itemSprite;
            }
            else
            {
                ItemSprite[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < EnemyInfo.Length; i++)
        {
            EnemySprite[i].gameObject.SetActive(true);
            ItemSprite[i].gameObject.SetActive(true);
        }
    }
}
