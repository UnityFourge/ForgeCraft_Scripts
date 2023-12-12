using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageGraph : MonoBehaviour
{
    public Slider[] DamageGraphs;
    public Image[] GraphIcon;

    private Expedition[] expeditions;
    private EnemyBehavior enemy;

    public Sprite[] CharacterIcons;
    public void GraphInit()
    {
        enemy = StageManager.Instance.enemy;
        expeditions = StageManager.Instance.expeditions;

        for(int i = 0; i < DamageGraphs.Length; i++)
        {
            if(i < expeditions.Length)
            {
                DamageGraphs[i].maxValue = enemy.MaxHealth;
                DamageGraphs[i].value = expeditions[i].StackedDamage;
                DamageGraphs[i].GetComponentInChildren<Text>().text = expeditions[i].StackedDamage.ToString();
                switch (expeditions[i].id / 100)
                {
                    case 0:
                        GraphIcon[i].sprite = CharacterIcons[0];
                        break;
                    case 1:
                        GraphIcon[i].sprite = CharacterIcons[1];
                        break;
                    case 2:
                        GraphIcon[i].sprite = CharacterIcons[2];
                        break;
                    case 3:
                        GraphIcon[i].sprite = CharacterIcons[3];
                        break;
                }
            }
            else
            {
                DamageGraphs[i].gameObject.SetActive(false);
            }
        }
    }
}
