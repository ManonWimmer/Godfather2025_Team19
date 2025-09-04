using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Road : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> _randomRoads = new List<Sprite>();
    // ----- FIELDS ----- //

    private void Start()
    {
        SetRandomRoadSprite();
    }

    private void OnEnable()
    {
        SetRandomRoadSprite();
    }

    private void SetRandomRoadSprite()
    {
        if (_randomRoads.Count == 0) return;

        int randomIndex = Random.Range(0, _randomRoads.Count);
        _spriteRenderer.sprite = _randomRoads[randomIndex];
    }

}
