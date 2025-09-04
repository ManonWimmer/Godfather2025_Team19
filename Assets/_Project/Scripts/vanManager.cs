using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VanManager : MonoBehaviour
{
    public static float points = 0f;
    public static bool crashed = false;
    [SerializeField] private int crashCount = 0;
    public BoxCollider2D boxCollider;
    [SerializeField] private TextMeshProUGUI _UIPoints;
    [SerializeField] private GameObject _camera;
    [SerializeField] private SpriteRenderer _vanSprite;

    [Header("Sprite Van")]
    [SerializeField] private List<Sprite> etats = new List<Sprite>();

    [Header("Collectibles")]
    private float bonus1 = 5f;
    private float bonus2 = 10f;
    private float malus1 = 5f;
    private float malus2 = 10f;
    private bool turning = false;
    private float turnCD = 0f;
    [SerializeField] private int _addScoreOnKirbyCollision = 20;
    [SerializeField] private int _addScoreOnWallCollision = 30;

    [Header("Combo")]
    public static float jauge = 1f;
    private float jaugeReset = 0f;

    [Header("Score")]
    [SerializeField] private int _addScorePerSecond = -1;
    private float _lasTimeAddedScore = 0f;

    [Header("SoundsFX")]
    [SerializeField] AudioClip[] Bonus;
    [SerializeField] AudioClip Malus;
    [SerializeField] AudioClip Oil;
    [SerializeField] AudioClip Fall;
    [SerializeField] AudioClip Kirby;
    [SerializeField] AudioClip Bounces;


    // 4 crash donc 5eme GO 
    // jauge mutiply *1 *2* *3

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Debug.Log("Points: " + points);
        Debug.Log("Points: " + points);
        crashCount = 0;
        jaugeReset = 0f;
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

        if (jaugeReset >= 2f)
        {
            jauge = 0f;
            jaugeReset = 0f;
        }

        if (jauge > 3)
        {
            jauge = 3;
        }

        // Check if add score per second
        _lasTimeAddedScore += Time.deltaTime;
        if (_lasTimeAddedScore > 1f)
        {
            points += _addScorePerSecond;
            Debug.Log(points);
            _lasTimeAddedScore = 0f;
        }

        jaugeReset += Time.deltaTime;
        turnCD += Time.deltaTime;

        // Turn camera
        if (turning)
        {
            _camera.transform.Rotate(Vector3.forward, 100.0f * Time.deltaTime);

            if (_camera.transform.rotation.eulerAngles.z >= 270.0f)
            {
                turning = false;
                turnCD = 0f;
            }
        }
        else
        {
            if (_camera.transform.rotation.eulerAngles.z != 90 && turnCD >= 3)
            {
                _camera.transform.Rotate(Vector3.forward, 100.0f * Time.deltaTime);

                if (_camera.transform.rotation.eulerAngles.z >= 360.0f || _camera.transform.rotation.eulerAngles.z < 91.0f)
                {
                    _camera.transform.rotation = Quaternion.Euler(0, 0, 90);
                }
            }
        }




    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "+5":
                points += bonus1 * jauge;
                jauge++;
                jaugeReset = 0f;
                Debug.Log("Points: " + points);
                SoundManager.instance.PlayRandomSoundFXClip(Bonus, transform);
                collision.gameObject.SetActive(false);
                break;
            case "Wall":
                Debug.Log("Player collided with a wall.");
                jauge++;
                points += _addScoreOnWallCollision * jauge;
                jaugeReset = 0f;
                crashCount++;
                StartCoroutine(TakeDamage());
                switch (crashCount)
                {
                    case 1:
                        _vanSprite.sprite = etats[0];
                        break;
                    case 2:
                        _vanSprite.sprite = etats[1];
                        break;
                    case 3:
                        _vanSprite.sprite = etats[2];
                        break;
                    case 4:
                        Debug.Log("Game Over.");
                        crashed = true;
                        SceneManager.LoadScene("EndScreens");
                        break;
                }
                break;
            case "+10":
                points += bonus2 * jauge;
                jauge++;
                jaugeReset = 0f;
                Debug.Log("Points: " + points);
                SoundManager.instance.PlayRandomSoundFXClip(Bonus, transform);
                collision.gameObject.SetActive(false);
                break;
            case "-5":
                points -= malus1;
                Debug.Log("Points: " + points);
                SoundManager.instance.PlaySoundFXClip(Malus, transform);
                collision.gameObject.SetActive(false);
                break;
            case "-10":
                points -= malus2;
                Debug.Log("Points: " + points);
                SoundManager.instance.PlaySoundFXClip(Malus, transform);
                collision.gameObject.SetActive(false);
                break;

            case "Oil":
                Debug.Log("Player hit oil.");

                jaugeReset = 10f;
                if (_camera.transform.rotation.eulerAngles.z != 0 && turnCD < 3)
                    turning = false;
                else turning = true;
                //StartCoroutine(ScreenRotate());
                SoundManager.instance.PlaySoundFXClip(Oil, transform);
                collision.gameObject.SetActive(false);
                break;

            case "Hole":
                SoundManager.instance.PlaySoundFXClip(Fall, transform);
                crashed = true;
                SceneManager.LoadScene("EndScreens");
                break;

            case "x2":
                points *= 2;
                SoundManager.instance.PlayRandomSoundFXClip(Bonus, transform);
                collision.gameObject.SetActive(false);
                break;

            case "Kirby":
                points += _addScoreOnKirbyCollision;
                SoundManager.instance.PlaySoundFXClip(Kirby, transform);
                collision.gameObject.SetActive(false);
                break;

            default:
                Debug.Log("Player collected something.");
                break;
        }

    }


    IEnumerator TakeDamage()
    {
        _vanSprite.color = new Color(0.6f, 0f, 0f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        _vanSprite.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.1f);
        _vanSprite.color = new Color(0.6f, 0f, 0f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        _vanSprite.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.1f);
        _vanSprite.color = new Color(0.6f, 0f, 0f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        _vanSprite.color = new Color(1f, 1f, 1f, 1f);
        yield return null;
    }
}