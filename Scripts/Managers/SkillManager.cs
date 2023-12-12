using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public EnemyBehavior Enemy;
    //public SkillSO EnemySkill;
    public SkillSO enemySkillSet;

    // 캐릭터 리스트를 받아오게 되면 수정!
    //public GameObject[] CharObj;
    public Expedition[] PlayerCharacters;

    private SkillSO[] Skills;
    public SkillSO[] PlayerSkillSet;
    private SkillDB _skillDB;
    public SkillIcon Icon;

    private static SkillManager instance;
    public static SkillManager Instance { get { return instance == null ? null : instance; } }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        _skillDB = GetComponent<SkillDB>();
    }


    public void Init(Expedition[] _playerCharacters, EnemyBehavior _enemy)
    {
        PlayerCharacters = _playerCharacters;

        Enemy = _enemy;

        Skills = DataManager.Instance.Skills;
        _skillDB.Init();
        //캐릭터의 순서에 맞게 배열에 저장
        PlayerSkillSet = new SkillSO[PlayerCharacters.Length];
        for (int i = 0; i < PlayerCharacters.Length; i++)
        {
            PlayerSkillSet[i] = CheckPlayerSkillSet(PlayerCharacters[i]);
        }
        enemySkillSet = CheckEnemySkillSet(Enemy);
        Icon.Init();
        
    }

    public SkillSO CheckPlayerSkillSet(Expedition _expedition)
    {
        foreach (SkillSO temp in Skills)
        {
            if (temp.SkillID == _expedition.skillId)
            {
                return temp;
            }
        }
        return null;
    }
    public void CallSkill(int index)
    {
        _skillDB.CheckSkillDictionary(PlayerSkillSet[index].SkillID, index);
        SoundManager.Instance.SfxPlay(PlayerSkillSet[index].SkillSFX);
        Icon.InteractiveSkill(index);
    }

    public SkillSO CheckEnemySkillSet(EnemyBehavior _enemy)
    {
        foreach (SkillSO temp in Skills)
        {
            if(temp.SkillID == _enemy.SkillID)
            {
                Enemy.SetSkillCoolTime_enemy(temp);
                return temp;
            }
        }
        return null;
    }
    public void EnemyCallSkill(SkillSO enemySkillSet)
    {
        _skillDB.CheckSkillDictionary(enemySkillSet.SkillID);
        SoundManager.Instance.SfxPlay(enemySkillSet.SkillSFX);
    }

    public void IsActive()
    {
        for(int i = 0;i < PlayerCharacters.Length;i++)
        {
            if (PlayerCharacters[i].curHP <= 0)
            {
                Icon.InactiveSkill(i);
            }
        }
    }

    public IEnumerator ShowAndHideFX(GameObject FX, Transform transform)
    {
        GameObject effect = Instantiate(FX, transform.position, Quaternion.identity, transform);

        SpriteRenderer renderer = effect.GetComponentInChildren<SpriteRenderer>();
        Color startColor = renderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        float duration = 2f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            renderer.material.color = Color.Lerp(startColor, endColor, elapsedTime / duration);

            elapsedTime += Time.deltaTime * 0.4f;
            yield return null;
        }
        yield return CoroutineHelper.WaitForSeconds(3f);
        Destroy(effect);
    }
}
