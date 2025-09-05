using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _finalPoints;
    [SerializeField] private GameObject _crashMessage;
    [SerializeField] private GameObject _successMessage;

    [SerializeField] private AudioClip Win;
    [SerializeField] private AudioClip Loose;

    private void Start()
    {
        _finalPoints.text =  vanManager.points.ToString();
        if (vanManager.crashed)
        {
            _crashMessage.SetActive(true);
            SoundManager.instance.PlaySoundFXClip(Loose, transform);
            _successMessage.SetActive(false);
        }
        else
        {
            _crashMessage.SetActive(false);
            SoundManager.instance.PlaySoundFXClip(Win, transform);
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
