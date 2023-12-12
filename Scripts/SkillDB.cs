using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDB : MonoBehaviour
{
    private delegate void PlayerSkill(int index);

    private Dictionary<int, PlayerSkill> PlayerSkillDictionary = new Dictionary<int, PlayerSkill>();

    private delegate void EnemySkill();

    private Dictionary<int, EnemySkill> EnemySkillDictionary = new Dictionary<int, EnemySkill>();

    private EnemyBehavior _enemy;
    private Expedition[] _player;

    public void Init()
    {
        _enemy = SkillManager.Instance.Enemy;
        _player = SkillManager.Instance.PlayerCharacters;

        PlayerSkillDictionary.Add(90001, PlayerSkill_Buff_AttackDelayUP);
        PlayerSkillDictionary.Add(90002, PlayerSkill_Buff_Heal);
        PlayerSkillDictionary.Add(90003, PlayerSkill_Buff_DamageUP);
        PlayerSkillDictionary.Add(90004, PlayerSkill_Buff_DefenceUP);
        PlayerSkillDictionary.Add(90005, PlayerSkill_Attack_Magic);

        EnemySkillDictionary.Add(80001, EnemySkill_Buff_Heal);
        EnemySkillDictionary.Add(80002, EnemySkill_Buff_AttackUP);
        EnemySkillDictionary.Add(80003, EnemySkill_Buff_AttackDelayUP);
        EnemySkillDictionary.Add(80004, EnemySkill_Buff_DefenceUP);
        EnemySkillDictionary.Add(80005, EnemySkill_Attack_Shoot);
        EnemySkillDictionary.Add(80006, EnemySkill_Attack_Melee);
        EnemySkillDictionary.Add(80007, EnemySkill_Attack_Spawn);

        EnemySkillDictionary.Add(80008, BossSkill_Attack_Entire); 
        EnemySkillDictionary.Add(80009, BossSkill_Attack_Entire);
    }

    public void CheckSkillDictionary(int index1, int index2)
    {
        if (PlayerSkillDictionary.ContainsKey(index1))
        {
            PlayerSkill _skill = PlayerSkillDictionary[index1];
            _skill(index2);
        }
        else
        {
            return ;
        }
    }

    public void CheckSkillDictionary(int index)
    {
        if(EnemySkillDictionary.ContainsKey(index))
        {
            EnemySkill _skill = EnemySkillDictionary[index];
            _skill();
        }
    }


    /// <summary>
    //     플레이어 부분 
    /// </summary>
    public void PlayerSkill_Buff_AttackDelayUP(int index)
    {
        Expedition _expedition = _player[index];
        _expedition.attackDelay -= SkillManager.Instance.PlayerSkillSet[index].SkillAmount;
        StartCoroutine(SkillManager.Instance.ShowAndHideFX(SkillManager.Instance.PlayerSkillSet[index].SkillEffect, _expedition.transform));
    }

    public void PlayerSkill_Buff_Heal(int index)
    {
        Expedition _expedition = _player[index];
        int lowHP = 0;
        float lowPlayer = float.MaxValue;
        for (int i = 0; i < _player.Length; i++)
        {
            if ((_player[i].curHP / _player[i].maxHP) * 100 < lowPlayer)
            {
                lowPlayer = _player[i].curHP;
                lowHP = i;
            }
        }
        _player[lowHP].curHP += SkillManager.Instance.PlayerSkillSet[index].SkillAmount + _player[index].attack * 0.2f;
        StartCoroutine(SkillManager.Instance.ShowAndHideFX(SkillManager.Instance.PlayerSkillSet[index].SkillEffect, _player[lowHP].transform));
    }

    public void PlayerSkill_Buff_DamageUP(int index) 
    {
        Expedition _expedition = _player[index];
        _expedition.attack += SkillManager.Instance.PlayerSkillSet[index].SkillAmount;
        StartCoroutine(SkillManager.Instance.ShowAndHideFX(SkillManager.Instance.PlayerSkillSet[index].SkillEffect, _expedition.transform));
    }

    public void PlayerSkill_Buff_DefenceUP(int index)
    {
        Expedition _expedition = _player[index];
        _expedition.defence += SkillManager.Instance.PlayerSkillSet[index].SkillAmount;
        StartCoroutine(SkillManager.Instance.ShowAndHideFX(SkillManager.Instance.PlayerSkillSet[index].SkillEffect, _expedition.transform));
    }

    public void PlayerSkill_Attack_Magic(int index)
    {
        Expedition _expedition = _player[index];
        _expedition.enemy.TakeDamage(SkillManager.Instance.PlayerSkillSet[index].SkillAmount +(_expedition.attack * 0.5f));
        StartCoroutine(SkillManager.Instance.ShowAndHideFX(SkillManager.Instance.PlayerSkillSet[index].SkillEffect, _expedition.enemy.gameObject.transform));
    }

    /// <summary>
    //     몬스터 부분 
    /// </summary>

    public void EnemySkill_Buff_Heal()
    {
        _enemy.EnemyHealth += SkillManager.Instance.enemySkillSet.SkillAmount;
    }

    public void EnemySkill_Buff_AttackUP()
    {
        _enemy.EnemyAttack += SkillManager.Instance.enemySkillSet.SkillAmount;
    }
    public void EnemySkill_Buff_AttackDelayUP()
    {
        _enemy.EnemyAttackSpeed -= SkillManager.Instance.enemySkillSet.SkillAmount;
    }
    public void EnemySkill_Buff_DefenceUP()
    {
        _enemy.EnemyDefence += SkillManager.Instance.enemySkillSet.SkillAmount;
    }

    public void EnemySkill_Attack_Shoot()
    {
        Transform projectile = CharacterManager.Instance.pool.Get(1).transform;
        projectile.transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y + 0.5f);
    }

    public void EnemySkill_Attack_Melee()
    {
        _enemy.nearestPlayer.GetComponent<Expedition>().TakeDamage(_enemy.EnemyAttack * 2f) ;
    }

    public void EnemySkill_Attack_Spawn()
    {
        Transform spawnMonster = CharacterManager.Instance.Get(2).transform;
        spawnMonster.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
    }
    public void EnemySkill_Attack_Area()
    {
        for (int i = 0; i < _enemy.Targets.Length; i++)
        {
            _enemy.Targets[i].TakeDamage(SkillManager.Instance.enemySkillSet.SkillAmount * 1.5f);
        }
    }

    public void BossSkill_Attack_Entire()
    {
        for(int i = 0; i<_enemy.Targets.Length; i++)
        {
            _enemy.Targets[i].TakeDamage(SkillManager.Instance.enemySkillSet.SkillAmount * 1.5f);
        }
    }
}
