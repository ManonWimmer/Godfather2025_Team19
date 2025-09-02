using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    // ----- FIELDS ----- //
    public static RandomGenerator Instance;

    [Header("Values")]
    [SerializeField] private int _nbrMaxGenerations = 10;
    [SerializeField] private List<Transform> _randomTransforms = new List<Transform>();

    [Header("Probabilities")]
    [SerializeField] private float _wallSpawnProbability = 0.2f;
    [SerializeField] private float _collectibleSpawnProbability = 0.4f;

    [Header("Prefabs")]
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _collectiblePrefab;

    [Header("Speed")]
    [SerializeField] private float _speedMultiplier = 1.05f;
    [SerializeField] private float _startSpeed = 5f;
    private float _currentSpeed = 5f;

    public float CurrentSpeed { get => _currentSpeed; set => _currentSpeed = value; }

    // ----- FIELDS ----- //

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentSpeed = _startSpeed;
    }

    public void SetNewRandoms()
    {
        // for each random transform
        // get random
        // check if random wall -> spawn wall
        // check if random collectible -> spawn collectible
    }


    private void Update()
    {
        _currentSpeed = _currentSpeed + _speedMultiplier * Time.deltaTime;

        // each x time * speed -> set new randoms
    }
}
