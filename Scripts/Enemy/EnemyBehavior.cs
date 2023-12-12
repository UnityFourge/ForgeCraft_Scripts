using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : Enemy
{
    private Enums.EnemyState curState = Enums.EnemyState.Idle;

    public Transform nearestPlayer;
    public bool IsActive;
    protected override void Awake()
    {
        base.Awake();

        StartCoroutine(FSM());
    }

    private void Update()
    {
        nearestPlayer = FindClosest(Targets, transform.position);
        if (!IsActive)
        {
            CheckSkillCoolTime();
        }
    }

    public Transform FindClosest(Expedition[] Targets, Vector2 curPos)
    {
        Transform target = null;
        float closedis = Mathf.Infinity;

        foreach (var obj in Targets)
        {
            if (obj != null && obj.gameObject.activeSelf)
            {
                float dis = Vector2.Distance(curPos, obj.transform.position);
                if (dis < closedis)
                {
                    closedis = dis;
                    target = obj.transform;
                }
            }
        }
        return target;
    }

    public void CheckSkillCoolTime()
    {
        curCooltime -= Time.deltaTime;
        if(curCooltime <= 0)
        {
            IsActive = true;
            curCooltime = maxCooltime;
        }
    }
    IEnumerator FSM()
    {
        while (true)
        {
            yield return StartCoroutine(curState.ToString());
        }
    }

    IEnumerator Idle()
    {
        yield return null;
        MyAnimSetTrigger("doStop");

        yield return CoroutineHelper.WaitForSeconds(2.0f);

        if (Targets != null)
        {
            curState = Enums.EnemyState.Walk;
        }
    }

    IEnumerator Walk()
    {
        if (Targets == null)
        {
            curState = Enums.EnemyState.Idle;
            StopAllCoroutines();
        }

        yield return null;

        float dis = Mathf.Abs(transform.position.x - nearestPlayer.position.x);
        if (dis < EnemyAttackRange && nearestPlayer != null)
        {
            _rigidbody.velocity = Vector2.zero;
            if (!IsActive)
            {
                curState = Enums.EnemyState.Attack;
            }
            else if(IsActive && SkillID != 0)
            {
                if (SkillID <= 80004)
                {
                    curState = Enums.EnemyState.Buff;
                }
                else
                {
                    curState = Enums.EnemyState.Skill;
                }
            }
        }
        else if(dis >  EnemyAttackRange || nearestPlayer == null)
        {
            MyAnimSetTrigger("doMove");
            _rigidbody.velocity = new Vector2(transform.localScale.x * EnemyMovingSpeed * (-1), _rigidbody.velocity.y);
        }

        yield return null;
    }

    IEnumerator Attack()
    {
        yield return null;

        MyAnimSetTrigger("doAttack");
        SoundManager.Instance.SfxPlay(EnemySFX);
        nearestPlayer.gameObject.GetComponent<Expedition>().TakeDamage(EnemyAttack);
        yield return CoroutineHelper.WaitForSeconds(EnemyAttackSpeed);

        curState = Enums.EnemyState.Walk;
    }

    IEnumerator Skill()
    {
        yield return null;

        MyAnimSetTrigger("doSkill");
        nearestPlayer.gameObject.GetComponent<Expedition>().TakeDamage(EnemyAttack);

        SkillManager.Instance.enemySkillSet= SkillManager.Instance.CheckEnemySkillSet(this);
        SkillManager.Instance.EnemyCallSkill(SkillManager.Instance.enemySkillSet);
        //SoundManager.Instance.SfxPlay(SkillManager.Instance.enemySkillSet.SkillSFX);

        yield return CoroutineHelper.WaitForSeconds(EnemyAttackSpeed);

        SkillID = RandomTable().SkillId;
        IsActive = false;
        curState = Enums.EnemyState.Walk;
    }
    IEnumerator Buff()
    {
        yield return null;

        MyAnimSetTrigger("doStop");

        SkillManager.Instance.enemySkillSet = SkillManager.Instance.CheckEnemySkillSet(this);
        SkillManager.Instance.EnemyCallSkill(SkillManager.Instance.enemySkillSet);

        yield return CoroutineHelper.WaitForSeconds(EnemyAttackSpeed);

        SkillID = RandomTable().SkillId;
        IsActive = false;
        curState = Enums.EnemyState.Walk;
    }

}