using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class VanManager : MonoBehaviour
{
    public static float points = 0f;
    public static bool crashed = false;
    [SerializeField] private int crashCount = 0;
    public BoxCollider2D boxCollider;
    [SerializeField] private TextMeshProUGUI _UIPoints;
    [SerializeField] private GameObject _camera;

    [SerializeField] private float bonus1 = 5f;
    [SerializeField] private float bonus2 = 10f;
    [SerializeField] private float malus1 = 5f;
    [SerializeField] private float malus2 = 10f;

    [SerializeField] private float jauge = 1f;


    
// 4 crash donc 5eme GO 
// jauge mutiply *1 *2* *3

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Debug.Log("Points: " + points);
        Debug.Log("Points: " + points);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Wall"))
    //    {
    //        Debug.Log("Player collided with a wall.");
    //        jauge++;
    //        crashCount++;
    //        if (crashCount == 5)
    //        {
    //            Debug.Log("Game Over.");
    //            crashed = true;
    //            SceneManager.LoadScene("EndScreens");
    //        }
           
    //    }

    //}
    private void Update()
    {
        _UIPoints.text = points.ToString();
        if (points < 0)
        {
            points = 0;
        }
        if (jauge > 3)
        {
            jauge = 3;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "+5":
                points += bonus1 * jauge;
                jauge++;
                Debug.Log("Points: " + points);
                Destroy(collision.gameObject);
                break;
            case "wall":
                Debug.Log("Player collided with a wall.");
                jauge++;
                crashCount++;
                if (crashCount == 5)
                {
                    Debug.Log("Game Over.");
                    crashed = true;
                    SceneManager.LoadScene("EndScreens");
                }
                break;
            case "+10":
                points += bonus2 * jauge;
                jauge++;
                Debug.Log("Points: " + points);
                Destroy(collision.gameObject);
                break;
            case "-5":
                points -= malus1;
                Debug.Log("Points: " + points);
                Destroy(collision.gameObject);
                break;
            case "-10":
                points -= malus2;
                Debug.Log("Points: " + points);
                Destroy(collision.gameObject);
                break;

            case "Oil":
                Debug.Log("Player hit oil.");
                jauge++;
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