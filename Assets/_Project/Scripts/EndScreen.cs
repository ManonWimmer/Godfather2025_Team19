using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _finalPoints;
    [SerializeField] private GameObject _crashMessage;
    [SerializeField] private GameObject _successMessage;

    private void Start()
    {
        _finalPoints.text =  VanManager.points.ToString();
        if (VanManager.crashed)
        {
            _crashMessage.SetActive(true);
            _successMessage.SetActive(false);
        }
        else
        {
            _crashMessage.SetActive(false);
            _successMessage.SetActive(true);
        }


    }
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
