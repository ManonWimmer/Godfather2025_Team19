using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{

    [SerializeField] private AudioClip TrottoirHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Van"))
        {
            vanManager.crashed = true;

            SoundManager.instance.PlaySoundFXClip(TrottoirHit, transform);

            vanManager.Instance.WaitAndLoadScene();
        }
    }

    private void WaitAndLoadScene()
    {
        StartCoroutine(WaitAndLoadEndScene());
    }

    private IEnumerator WaitAndLoadEndScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("EndScreens");
    }
}
