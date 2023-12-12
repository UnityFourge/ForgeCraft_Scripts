using System.Collections;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool isDragging = false;

    public ParticleSystem waterParticleSystem;
    private bool StartedParticleSystem = false;

    private Vector3 originalPosition;
    private Vector3 lastMousePosition;

    public GameObject target;

    [SerializeField] private float moveSpeed = 1f;

    [SerializeField] private float maxRotationX = -90f;

    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = -3f;

    private Rigidbody2D _rigidbody;

    private Vector3 targetOriginalPosition;
    private Quaternion targetOriginalRotation;

    public bool IsStoppedParticle { get; private set; }

    void Start()
    {

        originalPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0;
        _rigidbody.velocity = Vector2.zero;

        targetOriginalPosition = target.transform.position;
        targetOriginalRotation = target.transform.rotation;

        StopParticleSystem();
    }

    private void OnDisable()
    {
        isDragging = false;

        StopParticleSystem();
        StartedParticleSystem = false;

    }
    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.x = originalPosition.x;
            mousePosition.z = originalPosition.z;
            mousePosition.y = Mathf.Clamp(mousePosition.y, originalPosition.y + minY, originalPosition.y + maxY);
            Vector3 mouseDelta = mousePosition - lastMousePosition;

            if (transform.position.y + mouseDelta.y <= originalPosition.y + maxY)
            {
                _rigidbody.velocity = new Vector3(0f, mouseDelta.y / Time.deltaTime, 0f);
                transform.position = mousePosition;

                float rotationX = Mathf.Lerp(maxRotationX, 0f, (transform.position.y - (originalPosition.y + minY)) / (maxY - minY));
                target.transform.rotation = Quaternion.Euler(rotationX, 0f, 0f);

                if (transform.position.y <= -0.1f && !StartedParticleSystem)
                {
                    StartParticleSystem();
                    StartedParticleSystem = true;
                }
            }
            else
            {
                _rigidbody.velocity = Vector3.zero;
            }
            lastMousePosition = mousePosition;

        }
        else
        {
            if(!StartedParticleSystem)
            {  
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * moveSpeed);
            target.transform.position = Vector3.Lerp(target.transform.position, targetOriginalPosition, Time.deltaTime * moveSpeed);
            target.transform.rotation = Quaternion.Lerp(target.transform.rotation, targetOriginalRotation, Time.deltaTime * moveSpeed);

            }
        }

    }

    private void OnMouseDown()
    {
        isDragging = true;
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void StartParticleSystem()
    {
        var main = waterParticleSystem.main;
        waterParticleSystem.Play();

        IsStoppedParticle = false;

        StartCoroutine(CheckParticleSystem());
    }

    private void StopParticleSystem()
    {
        waterParticleSystem.Stop();
    }

    IEnumerator CheckParticleSystem()
    {
        yield return CoroutineHelper.WaitForSeconds(3.0f);

        IsStoppedParticle = true;
        StopParticleSystem();
    }
}