using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformScaler : MonoBehaviour
{
    [SerializeField] private float _minPlatformLength = 10;
    [SerializeField] private float _maxPlatformLength = 20;

    private float _platformHeight = 2;
    private float _platformWidth = 2;

    [SerializeField] private float _minGapLength = 5;
    [SerializeField] private float _maxGapLength = 15;

    [SerializeField] private bool _rescaleWithComplication = false;
    [SerializeField] private float _step = 2;

    private DifficultyController _difficulty;
    private int _currentDifficultyLevel;

    public float GetRequiredGapLength()
    {
        return _minGapLength;
    }

    public Vector3 ProduceGap()
    {
        return new Vector3(Random.Range(_minGapLength, _maxGapLength), 0, 0);
    }

    public Vector3 ProducePlatformScale(float minLength)
    {
        return ProducePlatformScaleImpl(
            Mathf.Max(minLength, _minPlatformLength),
            _maxPlatformLength);
    }

    public Vector3 ProducePlatformScale(float minLength, float maxLength)
    {
        // keeping platforms in defined boundaries
        return ProducePlatformScaleImpl(
            Mathf.Max(minLength, _minPlatformLength),
            Mathf.Min(maxLength, _maxPlatformLength));
    }

    private Vector3 ProducePlatformScaleImpl(float minLength, float maxLength)
    {
        return new Vector3(Random.Range(minLength, maxLength), _platformHeight, _platformWidth);
    }

    void Start()
    {
        _difficulty = GetComponentInParent<DifficultyController>();
        _currentDifficultyLevel = _difficulty.Level;
    }

    // Update is called once per frame
    void Update()
    {
        if (_rescaleWithComplication && _currentDifficultyLevel < _difficulty.Level)
        {
            _currentDifficultyLevel = _difficulty.Level;
            Rescale();
        }
    }

    private void Rescale()
    {
        // now length reducing is restricted by level count, but eventually one might need absolute extremums
        _minPlatformLength -= _step;
        _maxPlatformLength -= _step / 2;
    }
}
