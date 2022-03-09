using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public float Speed { get; private set; } = 0.2f;

    public int Level { get; private set; } = 1;

    [SerializeField] private float _maxSpeed = 0.8f;
    [SerializeField] private float _speedStep = 0.05f;

    [SerializeField] private float _timeStep = 40; // the difficulty increases every 40 seconds

    private float _time = 0;
    private const int MAX_LEVEL = 5;

    void Update()
    {
        // since the chance that a frame appears exactly in the step time is extremely small, 
        // we can't compare a modulus with zero
        float oldModulus = _time % _timeStep;
        _time += Time.deltaTime;
        if (oldModulus > _time % _timeStep && Level < MAX_LEVEL)
        {
            IncreaseDifficulty();
        }
    }

    private void IncreaseDifficulty()
    {
        if (Speed < _maxSpeed)
        {
            Speed += _speedStep;
        }
        ++Level;
    }
}
