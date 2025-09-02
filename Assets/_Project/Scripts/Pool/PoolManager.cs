using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SpawnObjectType
{
    Wall,
    Collectible
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

    private List<GameObject> _wallChildren = new List<GameObject>();
    private List<GameObject> _collectibleChildren = new List<GameObject>();
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

    public void SpawnObject(SpawnObjectType spawnType, Vector3 spawnPosition)
    {
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
        }

        GameObject nonActiveChild = GetFirstNonActiveGameObject(children);
        Debug.Log(nonActiveChild);

        // Create new child if full list
        if (nonActiveChild == null)
        {
            nonActiveChild = Instantiate(prefab, parent);
            UpdateChildrenLists();
        }

        nonActiveChild.SetActive(true);
        nonActiveChild.transform.position = spawnPosition;

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
