using UnityEngine;

public class DeactivateZone : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private bool _checkForRoad = false;
    // ----- FIELDS ----- //

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject);

        if (collision.gameObject.CompareTag("Road") || collision.gameObject.CompareTag("Sign"))
        {
            if (_checkForRoad)
            {
                collision.gameObject.SetActive(false);
            }
        }
        else if (!collision.gameObject.CompareTag("Van") && !collision.gameObject.CompareTag("Untagged"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
