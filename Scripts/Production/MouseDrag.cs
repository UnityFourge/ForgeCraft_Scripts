using UnityEngine;
using UnityEngine.UI;


public class MouseDrag : MonoBehaviour
{
    private bool isDragging = false;
    private bool canClick = true;
    private bool objectClick = true;

    private Vector3 offset;
    private Rigidbody2D _rigidbody;
    [SerializeField] private float minY = 2.5f;

    [SerializeField] private Button nextBtn;
    [SerializeField] private GameObject parent;

    private Vector3 originalPosition;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0f;
        nextBtn.interactable = false;

        originalPosition = transform.position;

    }
    private void OnEnable()
    {
        canClick = true;     
        objectClick = true;
        nextBtn.interactable = false;
       
    }
    private void OnDisable()
    {
        transform.position = originalPosition;
        _rigidbody.gravityScale = 0f;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 cursorWorldPoint = Camera.main.ScreenToWorldPoint(cursorScreenPoint);

            cursorWorldPoint.y = Mathf.Max(cursorWorldPoint.y, minY);

            transform.position = cursorWorldPoint + offset;
        }
    }
    void OnMouseDown()
    {
        if (canClick)
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
            _rigidbody.simulated = false;
            canClick = false;

            if (objectClick)
            {  
                objectClick = false;
                nextBtn.interactable = true;
            }
            Cursor.visible = false;
        }
    }
    void OnMouseUp()
    {
        Cursor.visible = true;
        isDragging = false;
        _rigidbody.simulated = true;
        _rigidbody.gravityScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("DestroyLine"))
        {
            Destroy(gameObject);
            GameObject newObject = Instantiate(gameObject, originalPosition, Quaternion.identity, parent.transform);

            Collider2D newCollider = newObject.GetComponent<Collider2D>();
            newCollider.enabled = true;

            MouseDrag newMouseDragScript = newObject.GetComponent<MouseDrag>();
            newMouseDragScript.enabled = true;
        }
    }
}
