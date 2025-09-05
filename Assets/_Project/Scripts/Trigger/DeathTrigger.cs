using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Van"))
        {
            vanManager.crashed = true;

            // son trottoir

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
