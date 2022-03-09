using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    [SerializeField] private float _horizontalTerminalValue = -50;

    void Update()
    {
        if (gameObject.transform.position.x <= _horizontalTerminalValue)
        {
            Destroy(gameObject);
        }
    }
}
