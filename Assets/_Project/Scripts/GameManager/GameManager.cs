using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ----- FIELDS ----- //
    public static GameManager Instance;

    [Header("Victory")]
    [SerializeField] private float _timeBeforeVictory = 2f;
    // ----- FIELDS ----- //

    private void Awake()
    {
        Instance = this;
    }

    public void StartVictory()
    {
        StartCoroutine(WaitForVictory());
    }

    private IEnumerator WaitForVictory()
    {
        yield return new WaitForSeconds(_timeBeforeVictory);
        SceneManager.LoadScene("EndScreens");
    }
}
