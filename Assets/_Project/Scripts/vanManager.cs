using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VanManager : MonoBehaviour
{
    public static float points = 0;
    public static bool crashed = false;
    public BoxCollider2D boxCollider;
    [SerializeField] private TextMeshProUGUI _UIPoints;
    [SerializeField] private GameObject _camera;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Debug.Log("Points: " + points);
        Debug.Log("Points: " + points);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Player collided with a wall.");
            crashed = true;
            SceneManager.LoadScene("EndScreens");
        }

    }
    private void Update()
    {
        _UIPoints.text = points.ToString();
        if (points < 0)
        {
            points = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "+5":
                points += 5;
                Debug.Log("Points: " + points);
                Destroy(collision.gameObject);
                break;
            case "+10":
                points += 10;
                Debug.Log("Points: " + points);
                Destroy(collision.gameObject);
                break;
            case "-5":
                points -= 5;
                Debug.Log("Points: " + points);
                Destroy(collision.gameObject);
                break;
            case "-10":
                points -= 10;
                Debug.Log("Points: " + points);
                Destroy(collision.gameObject);
                break;
            case "x2":
                points *= 2;
                Debug.Log("Points: " + points);
                Destroy(collision.gameObject);
                break;
            case "/2":
                points *= 0.5f;
                Debug.Log("Points: " + points);
                Destroy(collision.gameObject);
                break;
            case "huile":
                Debug.Log("Player hit oil.");
                StartCoroutine(ScreenRotate());
                Destroy(collision.gameObject);
                break;
            default:
                Debug.Log("Player collected something.");
                break;
        }

    }


    IEnumerator ScreenRotate()
    {
        _camera.transform.Rotate(0, 0, 180);

        yield return new WaitForSeconds(3);

        _camera.transform.Rotate(0, 0, -180);
        yield return null;
    }
}