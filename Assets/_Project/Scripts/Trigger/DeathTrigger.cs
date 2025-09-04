using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Van"))
        {
            VanManager.crashed = true;
            SceneManager.LoadScene("EndScreens");
        }
    }
}
