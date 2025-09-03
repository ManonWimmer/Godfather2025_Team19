using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    // ----- FIELDS ----- //
    public static RoadGenerator Instance;

    [Header("Road")]
    [SerializeField] private Transform _roadSpawn;
    [SerializeField] private GameObject _roadPrefab;
    [SerializeField] private int _nbrRoadOnScreen = 3;
    [SerializeField] private float _roadHeight = 100f;

    [SerializeField] private Transform _lastSpawnedRoadTransform;
    // ----- FIELDS ----- //

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        if (_lastSpawnedRoadTransform.position.y <= _roadSpawn.position.y - _roadHeight)
        {
            GameObject newRoad = PoolManager.Instance.SpawnObject(SpawnObjectType.Road, _roadSpawn.position);
            _lastSpawnedRoadTransform = newRoad.transform;
        }
    }
}
