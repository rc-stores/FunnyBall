using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private GameObject levelGO;
    [SerializeField] private RectTransform pauseButtonRectTF;

    [SerializeField] private float vericalGravityCoefficient = 800;

    private int gravitySign = -1;
    private bool reverseGravity;

    private Vector3 lowerPosition;
    private Vector3 upperPosition;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, vericalGravityCoefficient * gravitySign, 0);
        lowerPosition = transform.position;
        upperPosition = lowerPosition + levelGO.GetComponent<LevelGenerator>().GetVerticalOffset();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < lowerPosition.y - 4f || transform.position.y > upperPosition.y + 4f)
        {
            Debug.Log(transform.position.y + " " + lowerPosition.y);
            FindObjectOfType<GameManager>().EndGame();
            return;
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            reverseGravity = true;
        }
        else if (IsGrounded() && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (Input.GetTouch(0).phase == TouchPhase.Began &&
                GameManager.gameIsActive &&
                !RectTransformUtility.RectangleContainsScreenPoint(pauseButtonRectTF, touch.position))
            { 
                reverseGravity = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (reverseGravity)
        {
            ReverseGravity();
            reverseGravity = false;
        }
    }

    private bool IsGrounded()
    {
        // there is a little bug here: after falling in a gap there is a chance to reverse gravity and land on a platform "upside down"
        return Physics.OverlapSphere(transform.position, transform.localScale.x / 2 + 0.1f, groundLayerMask).Length > 0;
    }

    private void ReverseGravity()
    {
        gravitySign *= -1;
        Physics.gravity = new Vector3(0, vericalGravityCoefficient * gravitySign, 0);
    }
}
