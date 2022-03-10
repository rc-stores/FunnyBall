using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _levelGO;
    [SerializeField] private RectTransform _pauseButtonRectTF;
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private float _verticalShift = 2f;
    private Vector3 _direction = Vector3.down;
    private bool _isGrounded = false;

    private Vector3 _lowerPosition;
    private Vector3 _upperPosition;

    private const float GAME_OVER_VERT_OFFSET = 4f;

    // Start is called before the first frame update
    void Start()
    {
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

        if (_isGrounded)
        {
            // a quick fix for debugging w/o a smartphone
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isGrounded = false;
                _direction.y *= -1;
            }
            else if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began &&
                    GameManager.GameIsActive &&
                    !RectTransformUtility.RectangleContainsScreenPoint(_pauseButtonRectTF, touch.position))
                {
                    _isGrounded = false;
                    _direction.y *= -1;

                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_isGrounded)
        {
            transform.Translate(_direction.normalized * _verticalShift);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        _isGrounded = true;
        Transform platformTF = collider.transform;
        float offsetSign = _direction.y * -1;
        float offset = platformTF.localScale.y / 2 + transform.localScale.y / 2;
        transform.position = new Vector3(
            transform.position.x,
            platformTF.position.y + offset * offsetSign,
            transform.position.z);
    }

    private void OnTriggerExit(Collider collider)
    {
        _isGrounded = false;
    }
}
