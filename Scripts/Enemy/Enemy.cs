using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySO Select;

    public string EnemyName;
    public float EnemyHealth;
    public float MaxHealth;
    public float EnemyAttack;
    public float EnemyDefence;
    public float EnemyAttackRange;
    public float EnemyMovingSpeed;
    public float EnemyAttackSpeed;
    public Sprite EnemySprite;
    public Enums.SFX EnemySFX;
    public ItemSO[] EnemyDrops;

    public int SkillID;
    public List<BossSkillSet> SkillSet = new List<BossSkillSet>();
    public int total;

    public float maxCooltime;
    public float curCooltime;

    protected Animator animator;
    protected Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;

    public Expedition[] Targets;
    private UI_HPGauge HP;

    private string curTrigger = "doStop";

    protected virtual void Awake()
    {
        EnemyInit();
        animator = GetComponentInChildren<Animator>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer.sprite = EnemySprite;
    }

    private void Start()
    {
        HP = StageManager.Instance._hp;
        Targets = CharacterManager.Instance.Expeditions;

        for (int i = 0; i < SkillSet.Count; i++)
        {
            total += SkillSet[i].weight;
        }
    }

    public void EnemyInit()
    {
        Select = DataManager.Instance.EnemyInit(Player.Instance.SelectStage - 1);

        EnemyName = Select.EnemyName;
        EnemyHealth = Select.EnemyHealth;
        EnemyAttack = Select.EnemyAttack;
        MaxHealth = EnemyHealth;
        EnemyDefence = Select.EnemyDefence;
        EnemyAttackRange = Select.EnemyAttackRange;
        EnemyAttackSpeed = Select.EnemyAttackSpeed;
        EnemyMovingSpeed = Select.EnemyMovingSpeed;
        for(int j = 0; j < Select.EnemySFX.Length; j++)
        {
            EnemySFX = Select.EnemySFX[j];
        }
        EnemyDrops = new ItemSO[Select.DropItems.Length];
        for (int i = 0; i < Select.DropItems.Length; i++)
        {
            EnemyDrops[i] = Select.DropItems[i];
        }

        EnemySprite = Select.EnemySprite;

        SkillID = RandomTable().SkillId;
    }

    public void SetSkillCoolTime_enemy(SkillSO baseSkill)
    {
        maxCooltime = baseSkill.SkillCoolTime;
        curCooltime = baseSkill.SkillCoolTime;
    }

    public void TakeDamage(float PlayerAttack)
    {
        PlayerAttack -= EnemyDefence;
        if (PlayerAttack <= 0)
        {
            PlayerAttack = 1f;
        }
        EnemyHealth -= PlayerAttack;
        HP.EnemyChangeHealth();
        if(EnemyHealth <= 0)
        {
            _rigidbody.simulated = false;
            StageManager.Instance.GameOver();
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        MyAnimSetTrigger("doDie");
        if(Player.Instance.D_PlayerData.clearStage < Player.Instance.SelectStage)
        {
            Player.Instance.D_PlayerData.clearStage++;
        }
        yield return CoroutineHelper.WaitForSeconds(2.0f);
        this.gameObject.SetActive(false);
    }

    public void MyAnimSetTrigger(string name)
    {
        if (!IsPlayingAnim(name))
        {
            animator.ResetTrigger(curTrigger);
            animator.SetTrigger(name);
            curTrigger = name;
        }
    }

    public bool IsPlayingAnim(string name)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(name))
        {
            return true;
        }
        return false;
    }
    public BossSkillSet RandomTable()
    {
        int weight = 0;
        int temp = 0;
        temp = Mathf.RoundToInt(total * Random.Range(0f, 1.0f));

        for (int i = 0; i < SkillSet.Count; i++)
        {
            weight += SkillSet[i].weight;
            if (temp <= weight)
            {
                BossSkillSet table = new BossSkillSet(SkillSet[i]);
                return table;
            }
        }
        return null;
    }
}

[System.Serializable]
public class BossSkillSet
{
    public int SkillId;
    public int weight;

    public BossSkillSet(BossSkillSet _skill)
    {
        this.SkillId = _skill.SkillId;
        this.weight = _skill.weight;
    }
}
