using UnityEngine;

public class DeactivateZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Collectible"))
        {
            //Debug.Log("deactivate");
            collision.gameObject.SetActive(false);
        }
    }
}
