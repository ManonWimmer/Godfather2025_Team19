using UnityEngine;

public class DeactivateZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject);

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Collectible") || collision.gameObject.CompareTag("Road"))
        {
            //Debug.Log("deactivate");
            collision.gameObject.SetActive(false);
        }
    }
}
