using UnityEngine;

public class LevelGenerator : MonoBehaviour
{ 
    [SerializeField] private Transform platform;
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform lastLowerPlatform;
    [SerializeField] private Transform lastUpperPlatform;

    private DifficultyController difficulty;
    private PlatformScaler scaler;

    private Vector3 spawnStart;
    private readonly Vector3 verticalOffset = new Vector3(0, 14f, 0);

    public Vector3 GetVerticalOffset()
    {
        return verticalOffset;
    }

    void Start()
    {
        spawnStart = lastLowerPlatform.position; // must be immutable from now. how to do that w/o c-tor and readonly modifier?
        difficulty = GetComponent<DifficultyController>();
        scaler = GetComponent<PlatformScaler>();
    }

    void Update()
    {
        if (lastLowerPlatform.position.x <= spawnStart.x) {
            SpawnLevelPart();
            Debug.Log(GetEndPosition(lastLowerPlatform));
        }
    }

    private void SpawnLevelPart()
    {
        Vector3 gapScale = scaler.ProduceGap();
        SpawnUpperPlatform(gapScale);
        SpawnLowerPlatform(gapScale);
    }
    private Vector3 GetEndPosition(in Transform platform)
    {
        return platform.Find("PlatformEnd").position;
    }

    private Vector3 CalculateUpperScale(in Vector3 spawnPos, in Vector3 gapScale)
    {
        // max length is a distance to the last platform minus minimal gap length
        float maxLength = 2 * (Vector3.Distance(GetEndPosition(lastUpperPlatform), spawnPos) - scaler.GetRequiredGapLength());

        if (maxLength < gapScale.x)
        {
            maxLength = gapScale.x;
        }
        return scaler.ProducePlatformScale(gapScale.x, maxLength);
    }

    private void SpawnUpperPlatform(in Vector3 gapScale) 
    {
        Vector3 spawnPosition = GetEndPosition(lastLowerPlatform) + verticalOffset + gapScale / 2;

        float maxLength = 2 * (Vector3.Distance(GetEndPosition(lastUpperPlatform), spawnPosition) - scaler.GetRequiredGapLength());
        if (maxLength < gapScale.x)
        {
            maxLength = gapScale.x;
        }
         
        Spawn(out lastUpperPlatform, spawnPosition, scaler.ProducePlatformScale(gapScale.x, maxLength));
    }

    private void SpawnLowerPlatform(in Vector3 gapScale)
    {
        float minLength = (lastUpperPlatform.localScale / 2 - gapScale / 2).x + 2.5f;
        Vector3 platformScale = scaler.ProducePlatformScale(minLength);

        Vector3 spawnPosition = GetEndPosition(lastLowerPlatform) + gapScale;
        spawnPosition.x += platformScale.x / 2;

        Spawn(out lastLowerPlatform, spawnPosition, platformScale);
    }

    private void Spawn(out Transform lastPlatform, in Vector3 position, in Vector3 scale)
    {
        lastPlatform = Instantiate(platform, position, Quaternion.identity, gameObject.transform);
        lastPlatform.localScale = scale;
    }
}
