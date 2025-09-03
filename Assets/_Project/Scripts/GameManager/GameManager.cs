using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ----- FIELDS ----- //
    public static GameManager Instance;

    [Header("Victory")]
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private float _timeBeforeVictory = 2f;
    // ----- FIELDS ----- //

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _victoryScreen.SetActive(false);
    }

    public void StartVictory()
    {
        StartCoroutine(WaitForVictory());
    }

    private IEnumerator WaitForVictory()
    {
        yield return new WaitForSeconds(_timeBeforeVictory);
        _victoryScreen.SetActive(true);
    }
}
