using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    private DifficultyController difficulty; 
    private Vector3 direction;

    private void Start()
    {
        difficulty = GetComponentInParent<DifficultyController>();
        direction = new Vector3(-1, 0, 0);
    }

    private void FixedUpdate()
    {
        transform.Translate(direction.normalized * difficulty.speed);
    }

}
