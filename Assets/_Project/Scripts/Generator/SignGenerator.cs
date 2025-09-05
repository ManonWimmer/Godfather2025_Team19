using UnityEngine;

public class SignGenerator : MonoBehaviour
{
    // ----- FIELDS ----- //
    [Header("Big Sign")]
    [SerializeField] private GameObject _bigSign;
    [SerializeField] private float _spawnBigSignEachTime = 15f;
    [SerializeField] private Transform _bigSignSpawn;

    private float _lastSpawnTimeBigSign = 0f;

    [Header("Little Sign")]
    [SerializeField] private GameObject _littleSign;
    [SerializeField] private float _spawnLittleSignEachTime = 10f;
    [SerializeField] private Transform _leftLittleSignSpawn;
    [SerializeField] private Transform _rightLittleSignSpawn;

    private float _lastSpawnTimeLittleSign = 0f;
    private bool _lastLittleSignSpawnLeft = false;

    // ----- FIELDS ----- //

    private void Start()
    {
        _littleSign.SetActive(false);
    }

    private void SpawnLittleSign()
    {
        if (_lastLittleSignSpawnLeft)
            _littleSign.transform.position = _leftLittleSignSpawn.position;
        else
            _littleSign.transform.position = _rightLittleSignSpawn.position;

        _littleSign.SetActive(true);
        _lastLittleSignSpawnLeft = !_lastLittleSignSpawnLeft;
    }

    private void Update()
    {
        // Big sign spawn
        _lastSpawnTimeBigSign += Time.deltaTime;
        if (_lastSpawnTimeBigSign > _spawnBigSignEachTime)
        {
            _bigSign.transform.position = _bigSignSpawn.position;
            _bigSign.SetActive(true);
            _lastSpawnTimeBigSign = 0f;
        }

        // Little sign spawn
        _lastSpawnTimeLittleSign += Time.deltaTime;
        if (_lastSpawnTimeLittleSign > _spawnLittleSignEachTime)
        {
            SpawnLittleSign();
            _lastSpawnTimeLittleSign = 0f;
        }
    }
}
