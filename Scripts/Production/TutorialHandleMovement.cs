using UnityEngine;
using System.Collections;

public class TutorialHandleMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public Vector3 rightOffset = new Vector3(1, 0, 0);
    public Vector3 downOffset = new Vector3(0, -1, 0);
    private Vector3 originalPosition;

    private Coroutine moveCoroutine;

    private void Awake()
    {
        originalPosition = transform.position; 
    }

    private void OnEnable()
    {
        transform.position = originalPosition; 

        if (moveCoroutine == null) 
        {
            moveCoroutine = StartCoroutine(MoveObject());
        }
    }

    private void OnDisable()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null; 
        }
    }

    IEnumerator MoveObject()
    {
        while (true)
        {
            yield return MoveToPosition(originalPosition + rightOffset);
            yield return MoveToPosition(originalPosition + downOffset);
            yield return new WaitForSeconds(1);
            transform.position = originalPosition;
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }
}
