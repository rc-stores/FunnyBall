using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    [SerializeField] private float horizontalTerminalValue;

    void Update()
    {
        // todov remove the magic number
        if (gameObject.transform.position.x <= horizontalTerminalValue)
        {
            Destroy(gameObject);
        }
    }
}
