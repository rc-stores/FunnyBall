using UnityEngine;

public class LevelGenerator : MonoBehaviour
{ 
    [SerializeField] private Transform platformPrefabTF;
    [SerializeField] private Transform lastLowerPlatformTF;
    [SerializeField] private Transform lastUpperPlatformTF;

    private DifficultyController difficulty;
    private PlatformScaler scaler;

    private Vector3 spawnStart;
    private readonly Vector3 VERTICAL_OFFSET = new Vector3(0, 14f, 0);

    public Vector3 GetVerticalOffset()
    {
        return VERTICAL_OFFSET;
    }

    void Start()
    {
        spawnStart = lastLowerPlatformTF.position; // must be immutable from now. how to do that w/o c-tor and readonly modifier?
        difficulty = GetComponent<DifficultyController>();
        scaler = GetComponent<PlatformScaler>();
    }

    void Update()
    {
        if (lastLowerPlatformTF.position.x <= spawnStart.x) {
            SpawnLevelPart();
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
        float maxLength = 2 * (Vector3.Distance(GetEndPosition(lastUpperPlatformTF), spawnPos) - scaler.GetRequiredGapLength());

        if (maxLength < gapScale.x)
        {
            maxLength = gapScale.x;
        }
        return scaler.ProducePlatformScale(gapScale.x, maxLength);
    }

    private void SpawnUpperPlatform(in Vector3 gapScale) 
    {
        Vector3 spawnPosition = GetEndPosition(lastLowerPlatformTF) + VERTICAL_OFFSET + gapScale / 2;

        float maxLength = 2 * (Vector3.Distance(GetEndPosition(lastUpperPlatformTF), spawnPosition) - scaler.GetRequiredGapLength());
        if (maxLength < gapScale.x)
        {
            maxLength = gapScale.x;
        }
         
        Spawn(out lastUpperPlatformTF, spawnPosition, scaler.ProducePlatformScale(gapScale.x, maxLength));
    }

    private void SpawnLowerPlatform(in Vector3 gapScale)
    {
        float minLength = (lastUpperPlatformTF.localScale / 2 - gapScale / 2).x + 2.5f;
        Vector3 platformScale = scaler.ProducePlatformScale(minLength);

        Vector3 spawnPosition = GetEndPosition(lastLowerPlatformTF) + gapScale;
        spawnPosition.x += platformScale.x / 2;

        Spawn(out lastLowerPlatformTF, spawnPosition, platformScale);
    }

    private void Spawn(out Transform lastPlatform, in Vector3 position, in Vector3 scale)
    {
        lastPlatform = Instantiate(platformPrefabTF, position, Quaternion.identity, gameObject.transform);
        lastPlatform.localScale = scale;
    }
}
