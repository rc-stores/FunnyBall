using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private GameObject _levelGO;
    [SerializeField] private RectTransform _pauseButtonRectTF;
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private float _vericalGravityCoefficient = 800;

    private int _gravitySign = -1;
    private bool _reverseGravity;

    private Vector3 _lowerPosition;
    private Vector3 _upperPosition;

    private const float GAME_OVER_VERT_OFFSET = 4f;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, _vericalGravityCoefficient * _gravitySign, 0);
        _lowerPosition = transform.position;
        _upperPosition = _lowerPosition + _levelGO.GetComponent<LevelGenerator>().GetVerticalOffset();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < _lowerPosition.y - GAME_OVER_VERT_OFFSET || transform.position.y > _upperPosition.y + GAME_OVER_VERT_OFFSET)
        {
            _gameManager.EndGame();
            return;
        }

        if (IsGrounded())
        {
            // a quick fix for debugging w/o a smartphone
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _reverseGravity = true;
            }
            else if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began &&
                    GameManager.GameIsActive &&
                    !RectTransformUtility.RectangleContainsScreenPoint(_pauseButtonRectTF, touch.position))
                {
                    _reverseGravity = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (_reverseGravity)
        {
            ReverseGravity();
            _reverseGravity = false;
        }
    }

    private bool IsGrounded()
    {
        // there is a little bug here: after falling in a gap there is a chance to reverse gravity and land on a platform "upside down"
        return Physics.OverlapSphere(transform.position, transform.localScale.x / 2 + 0.1f, _groundLayerMask).Length > 0;
    }

    private void ReverseGravity()
    {
        _gravitySign *= -1;
        Physics.gravity = new Vector3(0, _vericalGravityCoefficient * _gravitySign, 0);
    }
}
