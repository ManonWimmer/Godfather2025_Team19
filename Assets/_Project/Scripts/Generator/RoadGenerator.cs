using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    // ----- FIELDS ----- //
    public static RoadGenerator Instance;

    [Header("Transforms")]
    [SerializeField] private Transform _roadSpawn;
    [SerializeField] private Transform _lastSpawnedRoadTransform;

    [Header("Values")]
    [SerializeField] private float _roadHeight = 100f;

    private SpawnObjectType _currentRoadType = SpawnObjectType.Road;
    // ----- FIELDS ----- //

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        if (_lastSpawnedRoadTransform.position.y <= _roadSpawn.position.y - _roadHeight)
        {
            GameObject newRoad = PoolManager.Instance.SpawnObject(_currentRoadType, _roadSpawn.position);
            _lastSpawnedRoadTransform = newRoad.transform;
        }
    }

    public void SetNewRoadType(SpawnObjectType newRoadType)
    {
        //Debug.Log("Set new road type");
        _currentRoadType = newRoadType;
    }
}
