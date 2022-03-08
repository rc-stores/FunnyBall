using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public float speed { get; private set; } = 0.2f;

    public int level { get; private set; } = 1;

    [SerializeField] private float kMaxSpeed = 0.8f;
    [SerializeField] private float speedStep = 0.05f;

    [SerializeField] private float timeStep = 40; // the difficulty increases every 40 seconds

    private float time = 0;
    private const int kMaxLevel = 5;

    void Update()
    {
        // since the chance that a frame appears exactly in the step time is extremely small, 
        // we can't compare a modulus with zero
        float oldModulus = time % timeStep;
        time += Time.deltaTime;
        if (oldModulus > time % timeStep && level < kMaxLevel)
        {
            IncreaseDifficulty();
        }
    }

    private void IncreaseDifficulty()
    {
        if (speed < kMaxSpeed)
        {
            speed += speedStep;
        }
        ++level;
    }
}
