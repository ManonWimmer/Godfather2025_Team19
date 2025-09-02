using UnityEngine;
using UnityEngine.SceneManagement;

public class vanManager : MonoBehaviour
{

    public BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Player collided with a wall.");
            SceneManager.LoadScene("CrashScreen");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Collectible"))
        {
            Debug.Log("Player collected something.");
        }
    }



}
