using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformScaler : MonoBehaviour
{
    [SerializeField] private float minPlatformLength = 10;
    [SerializeField] private float maxPlatformLength = 20;

    private float platformHeight = 2;
    private float platformWidth = 2;

    [SerializeField] private float minGapLength = 5;
    [SerializeField] private float maxGapLength = 15;

    [SerializeField] private bool rescaleWithComplication = false;
    [SerializeField] private float step = 2;

    private DifficultyController difficulty;
    private int currentDifficultyLevel;

    public float GetRequiredGapLength()
    {
        return minGapLength;
    }

    public Vector3 ProduceGap()
    {
        return new Vector3(Random.Range(minGapLength, maxGapLength), 0, 0);
    }

    public Vector3 ProducePlatformScale(float minLength)
    {
        return ProducePlatformScaleImpl(
            Mathf.Max(minLength, minPlatformLength),
            maxPlatformLength);
    }

    public Vector3 ProducePlatformScale(float minLength, float maxLength)
    {
        // keeping platforms in defined boundaries
        return ProducePlatformScaleImpl(
            Mathf.Max(minLength, minPlatformLength),
            Mathf.Min(maxLength, maxPlatformLength));
    }

    private Vector3 ProducePlatformScaleImpl(float minLength, float maxLength)
    {
        return new Vector3(Random.Range(minLength, maxLength), platformHeight, platformWidth);
    }

    void Start()
    {
        difficulty = GetComponentInParent<DifficultyController>();
        currentDifficultyLevel = difficulty.level;
    }

    // Update is called once per frame
    void Update()
    {
        if (rescaleWithComplication && currentDifficultyLevel < difficulty.level)
        {
            currentDifficultyLevel = difficulty.level;
            Rescale();
        }
    }

    private void Rescale()
    {
        // now length reducing is restricted by level count, but eventually one might need absolute extremums
        minPlatformLength -= step;
        maxPlatformLength -= step / 2;
    }
}
