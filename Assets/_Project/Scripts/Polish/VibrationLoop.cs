using UnityEngine;
using DG.Tweening;

public class VibrationLoop : MonoBehaviour
{
    // ----- FIELDS ----- //
    [Header("Values")]
    [SerializeField] private float _duration = 0.1f;
    [SerializeField] private float _strength = 0.05f;
    [SerializeField] private int _vibrato = 10;
    [SerializeField] private bool _random = false;
    // ----- FIELDS ----- //

    void Start()
    {
        transform.DOShakePosition(_duration, _strength, _vibrato, 90, false, _random)
                 .SetLoops(-1, LoopType.Restart);
    }
}
