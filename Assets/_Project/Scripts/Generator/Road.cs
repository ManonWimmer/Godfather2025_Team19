using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Road : MonoBehaviour
{
    // ----- FIELDS ----- //
    [Header("Random Road")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> _randomRoads = new List<Sprite>();

    [Header("Start / End Road")]
    [SerializeField] private Sprite _startRoad;
    [SerializeField] private Sprite _endRoad;

    private bool _isStartRoad = false;
    private bool _isEndRoad = false;
    // ----- FIELDS ----- //

    private void Start()
    {
        SetRoadSprite();
    }
    private void OnEnable()
    {
        SetRoadSprite();
    }

    private void SetRoadSprite()
    {
        if (_isStartRoad && _startRoad != null)
        {
            _spriteRenderer.sprite = _startRoad;
            return;
        }
        else if (_isEndRoad && _endRoad != null)
        {
            _spriteRenderer.sprite = _endRoad;
            return;
        }

        SetRandomRoadSprite();
    }

    private void SetRandomRoadSprite()
    {
        if (_randomRoads.Count == 0) return;

        int randomIndex = Random.Range(0, _randomRoads.Count);
        _spriteRenderer.sprite = _randomRoads[randomIndex];
    }

    public void SetupStartAndEnd(bool isStart, bool isEnd)
    {
        _isStartRoad = isStart;
        _isEndRoad = isEnd;

        SetRoadSprite();
    }
}
