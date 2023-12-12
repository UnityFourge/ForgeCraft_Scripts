using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPGauge : MonoBehaviour
{
    public Slider EnemySlider;
    public Slider[] PlayerSlider;

    public EnemyBehavior _enemy;
    public Expedition[] _expedition;
    float enemyMaxHP;
    float[] playerMaxHP;
    float enemycurHealth;
    float playercurHealth;
    public Text enemyHPtext;
    public Text[] playerHPtext;
    public GameObject[] PlayerHPIcon;
    public Sprite[] HPIcon;

    public void Init()
    {
        _enemy = CharacterManager.Instance.Enemy;
        _expedition = CharacterManager.Instance.Expeditions;

        enemyMaxHP = _enemy.EnemyHealth;
        playerMaxHP = new float[_expedition.Length];
        for(int h = 0; h < _expedition.Length; h++)
        {
            playerMaxHP[h] = _expedition[h].curHP;
        }

        EnemySlider.maxValue = enemyMaxHP;

        for (int i = 0; i < PlayerSlider.Length; i++)
        {
            if (i < _expedition.Length)
            {
                PlayerSlider[i].maxValue = playerMaxHP[i];
                PlayerSlider[i].gameObject.SetActive(true);
                playerHPtext[i].gameObject.SetActive(true);
            }else if( i >= _expedition.Length && PlayerSlider[i].maxValue == 1)
            {
                PlayerSlider[i].gameObject.SetActive(false);
                playerHPtext[i].gameObject.SetActive(false);
            }
        }
        EnemyChangeHealth();
        for (int j = 0; j < _expedition.Length; j++)
        {
            PlayerChangeHealth();
        }
        SetHPIcon();
    }

    public void EnemyChangeHealth()
    {
        enemycurHealth = _enemy.EnemyHealth;
        EnemySlider.value = enemycurHealth;

        float healthPercent = enemycurHealth;
        if(healthPercent <= 0) 
        {
            healthPercent = 0f;        
        }
        enemyHPtext.text = healthPercent.ToString("F0");
    }

    public void PlayerChangeHealth()
    {
        for (int index = 0; index < _expedition.Length; index++)
        {
            playercurHealth = _expedition[index].curHP;
            PlayerSlider[index].value = playercurHealth;

            float healthPercent = playercurHealth;
            if (healthPercent <= 0)
            {
                healthPercent = 0f;
            }
            playerHPtext[index].text = healthPercent.ToString("F0");
        }
    }

    public void SetHPIcon()
    {
        for(int i = 0; i< _expedition.Length; i++)
        {
            switch (_expedition[i].id / 100)
            {
                case 0:
                    PlayerHPIcon[i].GetComponent<Image>().sprite = HPIcon[0];
                    break;
                case 1:
                    PlayerHPIcon[i].GetComponent<Image>().sprite = HPIcon[1];
                    break;
                case 2:
                    PlayerHPIcon[i].GetComponent<Image>().sprite = HPIcon[2];
                    break;
                case 3:
                    PlayerHPIcon[i].GetComponent<Image>().sprite = HPIcon[3];
                    break;
            }
        }
    }
}
