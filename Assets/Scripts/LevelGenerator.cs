using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform _platformPrefabTF;
    [SerializeField] private Transform _lastLowerPlatformTF;
    [SerializeField] private Transform _lastUpperPlatformTF;

    private DifficultyController _difficulty;
    private PlatformScaler _scaler;

    private Vector3 _spawnStart;
    private readonly Vector3 VERTICAL_OFFSET = new Vector3(0, 14f, 0);

    private const string PLATFORM_END = "PlatformEnd";

    public Vector3 GetVerticalOffset()
    {
        return VERTICAL_OFFSET;
    }

    void Start()
    {
        _spawnStart = _lastLowerPlatformTF.position; // must be immutable from now. how to do that w/o c-tor and readonly modifier?
        _difficulty = GetComponent<DifficultyController>();
        _scaler = GetComponent<PlatformScaler>();
    }

    void Update()
    {
        if (_lastLowerPlatformTF.position.x <= _spawnStart.x) {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        Vector3 gapScale = _scaler.ProduceGap();
        SpawnUpperPlatform(gapScale);
        SpawnLowerPlatform(gapScale);
    }
    private Vector3 GetEndPosition(in Transform platform)
    {
        return platform.Find(PLATFORM_END).position;
    }

    private Vector3 CalculateUpperScale(in Vector3 spawnPos, in Vector3 gapScale)
    {
        // max length is a distance to the last platform minus minimal gap length
        float maxLength = 2 * (Vector3.Distance(GetEndPosition(_lastUpperPlatformTF), spawnPos) - _scaler.GetRequiredGapLength());

        if (maxLength < gapScale.x)
        {
            maxLength = gapScale.x;
        }
        return _scaler.ProducePlatformScale(gapScale.x, maxLength);
    }

    private void SpawnUpperPlatform(in Vector3 gapScale) 
    {
        Vector3 spawnPosition = GetEndPosition(_lastLowerPlatformTF) + VERTICAL_OFFSET + gapScale / 2;

        float maxLength = 2 * (Vector3.Distance(GetEndPosition(_lastUpperPlatformTF), spawnPosition) - _scaler.GetRequiredGapLength());
        if (maxLength < gapScale.x)
        {
            maxLength = gapScale.x;
        }
         
        Spawn(out _lastUpperPlatformTF, spawnPosition, _scaler.ProducePlatformScale(gapScale.x, maxLength));
    }

    private void SpawnLowerPlatform(in Vector3 gapScale)
    {
        float minLength = (_lastUpperPlatformTF.localScale / 2 - gapScale / 2).x + 2.5f;
        Vector3 platformScale = _scaler.ProducePlatformScale(minLength);

        Vector3 spawnPosition = GetEndPosition(_lastLowerPlatformTF) + gapScale;
        spawnPosition.x += platformScale.x / 2;

        Spawn(out _lastLowerPlatformTF, spawnPosition, platformScale);
    }

    private void Spawn(out Transform lastPlatform, in Vector3 position, in Vector3 scale)
    {
        lastPlatform = Instantiate(_platformPrefabTF, position, Quaternion.identity, gameObject.transform);
        lastPlatform.localScale = scale;
    }
}
