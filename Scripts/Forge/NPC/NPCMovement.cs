using UnityEngine;
using System;

public class NPCMovement : MonoBehaviour
{
    public GameObject requestObject;
    public float speed = 1.0f;
    public Vector3 startOffScreenPosition = new Vector3(-100, 0, 0);
    public Vector3 centerScreenPosition = new Vector3(0, 0, 0);
    public Vector3 endOffScreenPosition = new Vector3(100, 0, 0);

    private Rigidbody2D rigid;
    private SPUM_Prefabs spumPrefabs;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private float arrivalThreshold = 0.1f;
    public event Action OnExitComplete;
    private bool isWalkingOut = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spumPrefabs = GetComponent<SPUM_Prefabs>();
        NPCManager.Instance.SetNPC(GetComponent<NPC>());
        NPCManager.Instance.SetNPCMovement(this);
    }

    private void Start()
    {
        SetStartPosition();
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }
    public bool IsWalkingOut()
    {
        return isWalkingOut;
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        CheckIfArrived();
    }

    private void CheckIfArrived()
    {
        if (Vector3.Distance(transform.position, targetPosition) < arrivalThreshold)
        {
            isMoving = false;
            isWalkingOut = false;
            rigid.velocity = Vector2.zero;
            spumPrefabs.PlayAnimation("Idle");

            if (targetPosition == centerScreenPosition)
            {
                EnableRequest();
                //var npcOrderClick = GetComponent<NPCOrderClick>();
                //if (npcOrderClick != null && npcOrderClick.npcInteractionButton != null)
                //{
                //    npcOrderClick.npcInteractionButton.gameObject.SetActive(true);
                //}
                NPCManager.Instance.IsArrived = true;
            }

            if (targetPosition == endOffScreenPosition)
            {
                OnExitComplete?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }



    public void SetStartPosition()
    {
        transform.position = startOffScreenPosition;
        targetPosition = centerScreenPosition;
        isMoving = true;
        spumPrefabs.PlayAnimation("Run");
        LookRight();
        DisableRequest(); 
    }

    public void MoveToCenter()
    {
        targetPosition = centerScreenPosition;
        isMoving = true;
        spumPrefabs.PlayAnimation("Run");
        LookRight(); 
    }

    public void WalkOutOfScreen()
    {
        targetPosition = endOffScreenPosition;
        isMoving = true;
        isWalkingOut = true;
        spumPrefabs.PlayAnimation("Run");
        LookLeft();
        DisableRequest();
        //var npcOrderClick = GetComponent<NPCOrderClick>();
        //if (npcOrderClick != null && npcOrderClick.npcInteractionButton != null)
        //{
        //    npcOrderClick.npcInteractionButton.gameObject.SetActive(false);
        //}
        NPCManager.Instance.IsArrived = false;
    }

    private void LookRight()
    {
        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x); 
        transform.localScale = localScale;
    }

    private void LookLeft()
    {
        Vector3 localScale = transform.localScale;
        localScale.x = -Mathf.Abs(localScale.x); 
        transform.localScale = localScale;
    }
    private void DisableRequest()
    {
        if (requestObject != null)
            requestObject.SetActive(false);
    }

    private void EnableRequest()
    {
        if (requestObject != null)
            requestObject.SetActive(true);
    }

}
