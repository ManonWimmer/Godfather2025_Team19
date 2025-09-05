using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Van"))
        {
            VanManager.crashed = true;

            // son trottoir

            WaitAndLoadScene();
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
