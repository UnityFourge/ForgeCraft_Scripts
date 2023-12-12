using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectileMove : MonoBehaviour
{
    private Transform startPoint;
    private Transform endPoint;
    public Vector2 middlePoint;

    private float Speed = 0.3f;
    private float Length;
    private float elapsedTime = 0f;

    public Vector2 startPos;
    public Vector2 endPos;

    public Enemy EnemyPosition;
    public Expedition[] PlayerPosition;
    Expedition player;


    public void Init()
    {
        EnemyPosition = CharacterManager.Instance.Enemy;
        PlayerPosition = CharacterManager.Instance.Expeditions;

        float rand = Random.Range(0f, 2f);

        startPos = transform.position;
        endPos = EnemyPosition.transform.position;
        middlePoint = new Vector2((startPos.x + endPos.x)/2, rand);
        Vector2 controlPos = middlePoint;

        Length = Vector2.Distance(startPos, controlPos);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime * Speed;

        if(elapsedTime < 1f )
        {
            float time = elapsedTime / 1f;
            Vector2 m1 = Vector2.Lerp(startPos, middlePoint, time);
            Vector2 m2 = Vector2.Lerp(middlePoint, endPos, time);
            transform.position = Vector2.Lerp(m1, m2, time);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        elapsedTime = 0f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            this.gameObject.SetActive(false);
        }
    }
}
