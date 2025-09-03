using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SpawnObjectType
{
    Wall,
    Collectible, 
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

    [Header("Collectible")]
    [SerializeField] private GameObject _collectiblePrefab;
    [SerializeField] private Transform _collectibleParent;

    [Header("Road")]
    [SerializeField] private GameObject _roadPrefab;
    [SerializeField] private Transform _roadParent;

    [Header("Road2")]
    [SerializeField] private GameObject _road2Prefab;
    [SerializeField] private Transform _road2Parent;

    private List<GameObject> _wallChildren = new List<GameObject>();
    private List<GameObject> _collectibleChildren = new List<GameObject>();
    private List<GameObject> _roadChildren = new List<GameObject>();
    private List<GameObject> _road2Children = new List<GameObject>();
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
        _collectibleChildren = GetChildren(_collectibleParent);
        _roadChildren = GetChildren(_roadParent);
        _road2Children = GetChildren(_road2Parent);
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
        //Debug.Log(spawnType);
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
                children = _collectibleChildren;
                parent = _collectibleParent;
                prefab = _collectiblePrefab;
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
}
