using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StateEvent : UnityEvent<Enums.ExpeditionState>
{

}

public class Expedition : MonoBehaviour
{
    public int id;
    public float maxHP;
    public float curHP;
    public float attack;
    public float attackDelay;
    public float defence;
    protected float attackRange;
    public float[] status;
    private float criticalProb;
    private float criticalAttack;
    private float hitProb;
    private float dodgeProb;
    private UI_HPGauge HP;
    public int skillId;
    public Enums.SFX sFx;
    protected Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    public Enemy enemy;

    public SPUM_Prefabs _prefabs;

    public float StackedDamage;

    private string curTrigger = "doIdle";

    protected void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //anim = GetComponentInChildren<Animator>();

        if (_prefabs == null)
        {
            _prefabs = transform.GetChild(0).GetComponent<SPUM_Prefabs>();
        }
    }

    private void Start()
    {
        HP = StageManager.Instance._hp;
        enemy = CharacterManager.Instance.Enemy;
    }

    public void Init(CharacterSO data, int[] equipID)
    {

        id = data.CharacterID;
        //spriteRenderer.sprite = data.CharacterSprite;
        //anim.runtimeAnimatorController = data.CharacterAnim;
        status = new float[5];
        for (int i = 0; i < status.Length; i++)
        {
            status[i] = data.BaseStatus[i];
        }
        maxHP = data.BaseHealth;
        attack = data.BaseAttack;
        attackDelay = data.BaseAttackDelay;
        defence = data.BaseDefence;
        attackRange = data.BaseAttackRange;
        skillId = data.SkillId;
        sFx = data.CharacterSFX[0];
        for (int i = 0; i < equipID.Length; i++)
        {
            if (equipID[i] > 0)
            {
                float[] _status = DataManager.Instance.GetItem(equipID[i]).itemStatus;

                for (int j = 0; j < status.Length; j++)
                {
                    status[j] += _status[j];
                }
            }
        }
        StackedDamage = 0;
        curHP = status[0];
    }

    public void TakeDamage(float damage)
    {
        float calcDamage = damage - defence;
        curHP -= calcDamage > 0 ? calcDamage : 1;

        HP.PlayerChangeHealth();
        if (curHP <= 0) 
        {
            rigid.simulated = false;
            StageManager.Instance.GameOver();
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        SkillManager.Instance.IsActive();
        yield return CoroutineHelper.WaitForSeconds(2.0f);

        this.gameObject.SetActive(false);
    }

    public bool IsPlayingAnim(string name)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(name))
        {
            return true;
        }
        return false;
    }

    public void MyAnimSetTrigger(string name)
    {
        if (!IsPlayingAnim(name))
        {
            anim.ResetTrigger(curTrigger);
            anim.SetTrigger(name);
            curTrigger = name;
        }
    }

    //public void ChangeAttack()
    //{
    //    attack += 10f;
    //    StartCoroutine(SkillManager.Instance.ShowAndHideFX(SkillManager.Instance.SkillFX[0], transform));

    //}

    //public void ChangeHealth()
    //{
    //    curHP += 10f;
    //    StartCoroutine(SkillManager.Instance.ShowAndHideFX(SkillManager.Instance.SkillFX[1], transform));

    //}

    //public void ChangeDefence()
    //{
    //    defence += 10f;
    //    StartCoroutine(SkillManager.Instance.ShowAndHideFX(SkillManager.Instance.SkillFX[2], transform));

    //}

    //public void ChangeDelay()
    //{
    //    attackDelay -= 0.5f;
    //    StartCoroutine(SkillManager.Instance.ShowAndHideFX(SkillManager.Instance.SkillFX[3], transform));
    //}
}
