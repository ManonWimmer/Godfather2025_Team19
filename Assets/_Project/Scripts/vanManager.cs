using UnityEngine;

public class vanManager : MonoBehaviour
{

    public BoxCollider2D _boxCollider;

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Player collided with a wall.");
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
