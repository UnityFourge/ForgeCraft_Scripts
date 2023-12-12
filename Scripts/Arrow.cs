using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] private float arrowAttack;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private float arrowHeight;
    [SerializeField] private float initialAngle = 30f;

    Enemy enemy;
    Transform target;
    
    Vector3 start;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.right = rigid.velocity;
    }

    public void Init(float attack)
    {
        if (enemy == null) enemy = CharacterManager.Instance.Enemy;
        if (target == null) target = enemy.transform;

        arrowAttack = attack;
        start = transform.position;

        Vector3 velocity = GetVelocity(start, new Vector3(target.position.x, -3.0f), initialAngle);
        rigid.velocity = velocity;
    }

    public Vector3 GetVelocity(Vector3 player, Vector3 target, float initialAngle)
    {
        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(target.x, 0, target.z);
        Vector3 planarPosition = new Vector3(player.x, 0, player.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = player.y - target.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (target.x > player.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        return finalVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            float damage = arrowAttack - enemy.EnemyDefence <= 1 ? 1 : arrowAttack - enemy.EnemyDefence;
            enemy.TakeDamage(damage);
            this.gameObject.SetActive(false);
        }
    }
}
