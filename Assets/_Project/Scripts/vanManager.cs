using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class vanManager : MonoBehaviour
{

    public BoxCollider2D boxCollider;
    [SerializeField] private float _points = 0;
    [SerializeField] private TextMeshProUGUI _UIPoints;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Debug.Log("Points: " + _points);
        Debug.Log("Points: " + _points);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Player collided with a wall.");
            SceneManager.LoadScene("CrashScreen");
        }

    }
    private void Update()
    {
        _UIPoints.text = _points.ToString();
        if (_points < 0)
        {
            _points = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "+5":
                _points += 5;
                Debug.Log("Points: " + _points);
                Destroy(collision.gameObject);
                break;
            case "+10":
                _points += 10;
                Debug.Log("Points: " + _points);
                Destroy(collision.gameObject);
                break;
            case "-5":
                _points -= 5;
                Debug.Log("Points: " + _points);
                Destroy(collision.gameObject);
                break;
            case "-10":
                _points -= 10;
                Debug.Log("Points: " + _points);
                Destroy(collision.gameObject);
                break;
            case "x2":
                _points *= 2;
                Debug.Log("Points: " + _points);
                Destroy(collision.gameObject);
                break;
            case "/2":
                _points *= 0.5f;
                Debug.Log("Points: " + _points);
                Destroy(collision.gameObject);
                break;
            default:
                Debug.Log("Player collected something.");
                break;
        }

    }

    



}
