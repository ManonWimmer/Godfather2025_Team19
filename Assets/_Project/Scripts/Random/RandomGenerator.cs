using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    // ----- FIELDS ----- //
    public static RandomGenerator Instance;

    [Header("Values")]
    [SerializeField] private int _nbrMaxGenerations = 10;
    [SerializeField] private List<Transform> _randomTransforms = new List<Transform>();

    [Header("Probabilities")]
    [SerializeField][UnityEngine.Range(0, 1)] private float _wallSpawnProbability = 0.2f;
    [SerializeField][UnityEngine.Range(0, 1)] private float _collectibleSpawnProbability = 0.4f;

    private float _wallFinalMaxProba = 0f;
    private float _collectibleFinalMaxProba = 0f;

    [Header("Speed & Time")]
    [SerializeField] private float _speedMultiplier = 1.05f;
    [SerializeField] private float _startSpeed = 5f;
    [SerializeField] private float _timeBetweenSpawns = 1f;
    private float _currentSpeed = 5f;

    private float _lastSpawnedTime = 0f;

    public float CurrentSpeed { get => _currentSpeed; set => _currentSpeed = value; }

    // ----- FIELDS ----- //

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentSpeed = _startSpeed;

        // Final probabilities
        _wallFinalMaxProba = _wallSpawnProbability;
        _collectibleFinalMaxProba = _wallFinalMaxProba + _collectibleSpawnProbability;

        // Test randoms
        SetNewRandoms();
    }

    public void SetNewRandoms()
    {
        bool canSpawnWall = true;
        bool canSpawnCollectible = true;

        // Can only spawn 1 wall max & 1 collectible max
        foreach(Transform randomTransform in _randomTransforms)
        {
            float random = Random.Range(0f, 1f);
            Debug.Log(random);
            
            if (random <= _wallFinalMaxProba && canSpawnWall)
            {
                // Spawn wall
                PoolManager.Instance.SpawnObject(SpawnObjectType.Wall, randomTransform.position);
                canSpawnWall = false;
            }
            else if (random > _wallFinalMaxProba && random <= _collectibleFinalMaxProba && canSpawnCollectible)
            {
                // Spawn collectible
                PoolManager.Instance.SpawnObject(SpawnObjectType.Collectible, randomTransform.position);
                canSpawnCollectible = false;
            }
        }
    }


    private void Update()
    {
        _currentSpeed = _currentSpeed + _speedMultiplier * Time.deltaTime;

        _lastSpawnedTime += Time.deltaTime;
        if (_lastSpawnedTime > _timeBetweenSpawns)
        {
            SetNewRandoms();
            _lastSpawnedTime = 0f;
        }
    }
}
