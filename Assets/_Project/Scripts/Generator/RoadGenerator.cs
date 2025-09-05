using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    // ----- FIELDS ----- //
    public static RoadGenerator Instance;

    [Header("Transforms")]
    [SerializeField] private Transform _roadSpawn;
    [SerializeField] private Transform _lastSpawnedRoadTransform;
    private Road _lastSpawnedRoad;

    [Header("Values")]
    [SerializeField] private float _roadHeight = 100f;

    private SpawnObjectType _lastObjectType = SpawnObjectType.Road;
    private SpawnObjectType _currentRoadType = SpawnObjectType.Road;

    private bool _isStarted = false;
    private bool _isStart = false;
    private bool _isEnd = false;
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
            _lastSpawnedRoad = newRoad.GetComponent<Road>();

            /*
            if (_lastSpawnedRoad != null )
            {
                _lastSpawnedRoad.SetupStartAndEnd(_isStart, _isEnd);
            }

            _isStart = false;
            _isEnd = false;
            */

            _lastSpawnedRoadTransform = newRoad.transform;
        }
    }

    public void SetNewRoadType(SpawnObjectType newRoadType)
    {
        _lastObjectType = _currentRoadType;
        _currentRoadType = newRoadType;

        /*
        if (_lastObjectType != _currentRoadType)
        {
            if (_isStarted)
            {
                _isStarted = false;
                _isStart = false;
                _isEnd = true;

                _lastSpawnedRoad.SetupStartAndEnd(false, true);
            }
            else
            {
                _isStarted = true;
                _isStart = true;
            }
        }
        else
        {
            if (WillChangeTypeSoon()) _isEnd = true;
        }
        */
    }

    private bool WillChangeTypeSoon()
    {
        int nextGen = RandomGenerator.Instance.GetCurrentGeneration() + 1;

        foreach (var change in RandomGenerator.Instance.GetRoadChanges())
        {
            if (change.NbrGenerationsChange == nextGen)
                return true;
        }

        return false;
    }

}
