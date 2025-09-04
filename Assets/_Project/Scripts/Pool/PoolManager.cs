using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct CollectibleProbability
{
    public SpawnObjectType SpawnType;
    public float SpawnProbability;
}

public enum SpawnObjectType
{
    Wall,
    Collectible, 
    Plus10,
    Plus5,
    Minus5,
    Minus10,
    x2,
    Oil,
    Hole,
    Kirby,
    Road,
    Road2
}

public class PoolManager : MonoBehaviour
{
    // ----- FIELDS ----- //
    public static PoolManager Instance;

    [Header("Wall")]
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private Transform _wallParent;

    [Header("Road")]
    [SerializeField] private GameObject _roadPrefab;
    [SerializeField] private Transform _roadParent;

    [Header("Road2")]
    [SerializeField] private GameObject _road2Prefab;
    [SerializeField] private Transform _road2Parent;

    [Header("----- COLLECTIBLES -----")]
    [Header("Probabilities")]
    [SerializeField] private List<CollectibleProbability> _collectibleProbas = new List<CollectibleProbability>();

    [Header("Plus 10")]
    [SerializeField] private GameObject _plus10Prefab;
    [SerializeField] private Transform _plus10Parent;

    [Header("Plus 5")]
    [SerializeField] private GameObject _plus5Prefab;
    [SerializeField] private Transform _plus5Parent;

    [Header("Minus 10")]
    [SerializeField] private GameObject _minus10Prefab;
    [SerializeField] private Transform _minus10Parent;

    [Header("Minus 5")]
    [SerializeField] private GameObject _minus5Prefab;
    [SerializeField] private Transform _minus5Parent;

    [Header("Multiply 2")]
    [SerializeField] private GameObject _multiply2Prefab;
    [SerializeField] private Transform _multiply2Parent;

    [Header("Oil")]
    [SerializeField] private GameObject _oilPrefab;
    [SerializeField] private Transform _oilParent;

    [Header("Hole")]
    [SerializeField] private GameObject _holePrefab;
    [SerializeField] private Transform _holeParent;

    [Header("Kirby")]
    [SerializeField] private GameObject _kirbyPrefab;
    [SerializeField] private Transform _kirbyParent;

    // Children
    private List<GameObject> _wallChildren = new List<GameObject>();
    private List<GameObject> _roadChildren = new List<GameObject>();
    private List<GameObject> _road2Children = new List<GameObject>();

    private List<GameObject> _collectiblePlus10Children = new List<GameObject>();
    private List<GameObject> _collectiblePlus5Children = new List<GameObject>();

    private List<GameObject> _collectibleMinus10Children = new List<GameObject>();
    private List<GameObject> _collectibleMinus5Children = new List<GameObject>();

    private List<GameObject> _collectibleMultiply2Children = new List<GameObject>();

    private List<GameObject> _collectibleOilChildren = new List<GameObject>();
    private List<GameObject> _collectibleHoleChildren = new List<GameObject>();

    private List<GameObject> _collectibleKirbyChildren = new List<GameObject>();
    // ----- FIELDS ----- //

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateChildrenLists();
    }

    private void UpdateChildrenLists()
    {
        // Get children 
        _wallChildren = GetChildren(_wallParent);
        _roadChildren = GetChildren(_roadParent);
        _road2Children = GetChildren(_road2Parent);

        // ----- Collectibles ----- //
        _collectiblePlus10Children = GetChildren(_plus10Parent);
        _collectiblePlus5Children = GetChildren(_plus5Parent);

        _collectibleMinus10Children = GetChildren(_minus10Parent);
        _collectibleMinus5Children = GetChildren(_minus5Parent);
        
        _collectibleMultiply2Children = GetChildren(_multiply2Parent);

        _collectibleOilChildren = GetChildren(_oilParent);
        _collectibleHoleChildren = GetChildren(_holeParent);

        _collectibleHoleChildren = GetChildren(_kirbyParent);
    }

    private List<GameObject> GetChildren(Transform parent)
    {
        List<GameObject> result = new List<GameObject>();
       
        for (int i = 0; i < parent.childCount; i++)
        {
            result.Add(parent.GetChild(i).gameObject);
        }

        return result;
    }

    public GameObject SpawnObject(SpawnObjectType spawnType, Vector3 spawnPosition)
    {
        //Debug.Log($"Spawn type : {spawnType}");
        List<GameObject> children = new List<GameObject>();
        Transform parent = null;
        GameObject prefab = null;

        switch (spawnType)
        {
            case SpawnObjectType.Wall:
                children = _wallChildren;
                parent = _wallParent;
                prefab = _wallPrefab;
                break;

            case SpawnObjectType.Collectible:
                GetRandomCollectibleChildrenParentAndPrefab(out children, out parent, out prefab);
                break;

            case SpawnObjectType.Road:
                children = _roadChildren;
                parent = _roadParent;
                prefab = _roadPrefab;
                break;

            case SpawnObjectType.Road2:
                children = _road2Children;
                parent = _road2Parent;
                prefab = _road2Prefab;
                break;
        }

        GameObject nonActiveChild = GetFirstNonActiveGameObject(children);

        // Create new child if full list
        if (nonActiveChild == null)
        {
            nonActiveChild = Instantiate(prefab, parent);
            UpdateChildrenLists();
        }

        nonActiveChild.SetActive(true);
        nonActiveChild.transform.position = spawnPosition;

        return nonActiveChild;
    }

    private GameObject GetFirstNonActiveGameObject(List<GameObject> children)
    {
        foreach (var child in children)
        {
            if (!child.activeSelf) return child;
        }

        return null;
    }

    private void GetRandomCollectibleChildrenParentAndPrefab(out List<GameObject> children, out Transform parent, out GameObject prefab)
    {
        CollectibleProbability randomCollectible = GetRandomCollectible(_collectibleProbas);
        Debug.Log($"Random collectible : {randomCollectible.SpawnType}");

        switch (randomCollectible.SpawnType)
        { 
            default:
                case SpawnObjectType.Plus10:
                    children = _collectiblePlus10Children;
                    parent = _plus10Parent;
                    prefab = _plus10Prefab;
                    break;
            case SpawnObjectType.Plus5:
                children = _collectiblePlus5Children;
                parent = _plus5Parent;
                prefab = _plus5Prefab;
                break;
            case SpawnObjectType.Minus5:
                children = _collectibleMinus5Children;
                parent = _minus5Parent;
                prefab = _minus5Prefab;
                break;
            case SpawnObjectType.Minus10:
                children = _collectibleMinus10Children;
                parent = _minus10Parent;
                prefab = _minus10Prefab;
                break;
            case SpawnObjectType.x2:
                children = _collectibleMultiply2Children;
                parent = _multiply2Parent;
                prefab = _multiply2Prefab;
                break;
            case SpawnObjectType.Oil:
                children = _collectibleOilChildren;
                parent = _oilParent;
                prefab = _oilPrefab;
                break;
            case SpawnObjectType.Hole:
                children = _collectibleHoleChildren;
                parent = _holeParent;
                prefab = _holePrefab;
                break;
            case SpawnObjectType.Kirby:
                children = _collectibleKirbyChildren;
                parent = _kirbyParent;
                prefab = _kirbyPrefab;
                break;
        }

        Debug.Log($"Collectible parent : {parent}");
    }

    public CollectibleProbability GetRandomCollectible(List<CollectibleProbability> liste)
    {
        float total = liste.Sum(s => s.SpawnProbability);
        if (total <= 0f)
        {
            Debug.LogWarning("Somme des probabilités nulle ou négative !");
            return default(CollectibleProbability);
        }

        float randomValue = Random.value * total; 
        float cumulative = 0f;

        foreach (var s in liste)
        {
            cumulative += s.SpawnProbability;
            if (randomValue <= cumulative)
            {
                return s;
            }
        }

        return liste[liste.Count - 1];
    }

}
