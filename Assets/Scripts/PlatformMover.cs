using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    private DifficultyController _difficulty; 
    private Vector3 _direction;

    private void Start()
    {
        _difficulty = GetComponentInParent<DifficultyController>();
        _direction = new Vector3(-1, 0, 0);
    }

    private void FixedUpdate()
    {
        transform.Translate(_direction.normalized * _difficulty.Speed);
    }

}
