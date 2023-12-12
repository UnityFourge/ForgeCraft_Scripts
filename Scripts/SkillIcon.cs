using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    public GameObject[] Icon;

    [SerializeField]private Image[] skillImage;
    [SerializeField]private Text[] skillTime;

    float[] curCoolTime;
    float[] maxCoolTime;
    bool[] IsActive;

    private void Update()
    {
        for(int i = 0; i < curCoolTime.Length; i++)
        {
            if (curCoolTime[i] > 0)
            {
                curCoolTime[i] -= Time.deltaTime;
                if (curCoolTime[i] <= 0)
                {
                    curCoolTime[i] = 0;
                    IsActive[i] = true;
                    skillTime[i].text = "Ready";
                    Icon[i].GetComponent<Button>().interactable = true;
                }
                else
                {
                    skillTime[i].text = curCoolTime[i].ToString("F0");
                }
            }
        }
    }
    public void Init()
    {
        skillImage = new Image[SkillManager.Instance.PlayerSkillSet.Length];
        skillTime = new Text[SkillManager.Instance.PlayerSkillSet.Length];
        curCoolTime = new float[SkillManager.Instance.PlayerSkillSet.Length];
        maxCoolTime = new float[SkillManager.Instance.PlayerSkillSet.Length];
        IsActive = new bool[SkillManager.Instance.PlayerSkillSet.Length];

        for(int h = 0; h < Icon.Length; h++)
        {
            if(h >= SkillManager.Instance.PlayerSkillSet.Length)
            {
                Icon[h].gameObject.SetActive(false);
                Icon[h].gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < SkillManager.Instance.PlayerSkillSet.Length; i++)
        {
            skillImage[i] = Icon[i].GetComponent<Image>();
            skillTime[i] = Icon[i].GetComponentInChildren<Text>();
            Icon[i].GetComponent<Button>().interactable = false;
        }
        for (int j =0; j < skillImage.Length; j++)
        {
            maxCoolTime[j] = 0f;
            skillImage[j].sprite = SkillManager.Instance.PlayerSkillSet[j].SkillIcon;
            maxCoolTime[j] = SkillManager.Instance.PlayerSkillSet[j].SkillCoolTime;
            curCoolTime[j] = maxCoolTime[j];
            skillTime[j].text = maxCoolTime[j].ToString("F0");
        }
    }

    public void InteractiveSkill(int index)
    {
        if (IsActive[index])
        {
            curCoolTime[index] = maxCoolTime[index];
            IsActive[index] = false;
            Icon[index].GetComponent<Button>().interactable = false;
            skillTime[index].text = maxCoolTime[index].ToString();
        }
    }

    public void InactiveSkill(int index)
    {
        Icon[index].gameObject.SetActive(false);
    }
}
