using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class vanManager : MonoBehaviour
{
    public static vanManager Instance;

    [HideInInspector] public static float points = 0f;
    [HideInInspector] public static bool crashed = false;

    private int crashCount = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _UIPointsTxt;
    [SerializeField] private GameObject _UIPointsGO;

    [Header("References")]
    [SerializeField] private GameObject _camera;
    [SerializeField] private SpriteRenderer _vanSprite;

    [Header("Sprite Van")]
    [SerializeField] private List<Sprite> etats = new List<Sprite>();

    [Header("Add Score Collectibles")]
    [SerializeField] private int _addScoreOnKirbyCollision = 20;
    [SerializeField] private int _addScoreOnWallCollision = 30;

    private float bonus1 = 5f;
    private float bonus2 = 10f;
    private float malus1 = 5f;
    private float malus2 = 10f;

    [Header("Combo")]
    [HideInInspector] public static float jauge = 1f;
    private float jaugeReset = 0f;
    [SerializeField] UnityEvent onJaugeChange;

    [Header("Score")]
    [SerializeField] private int _addScorePerSecond = -1;
    private float _lasTimeAddedScore = 0f;

    [Header("SoundsFX")]
    [SerializeField] private AudioClip[] Bonus;
    [SerializeField] private AudioClip[] Malus;
    [SerializeField] private AudioClip Wall;
    [SerializeField] private AudioClip Oil;
    [SerializeField] private AudioClip Fall;
    [SerializeField] private AudioClip Kirby;
    //[SerializeField] private AudioClip[] Bounces;

    [Header("Polish")]
    [SerializeField] private float _collectibleAnimationTime = 1f;
    [SerializeField] private float _turnCameraOilDuration = 5f;

    private bool _canGainPoints = true;

    // 4 crash donc 5eme GO 
    // jauge mutiply *1 *2* *3
    private void Awake()
    {
        Instance = this;    
    }

    private void Update()
    {
        if (!_canGainPoints) return;

        _UIPointsTxt.text = points.ToString();

        if (jaugeReset >= 2f)
        {
            jauge = 1f;
            jaugeReset = 0f;
            onJaugeChange?.Invoke();
        }

        if (jauge > 3)
        {
            jauge = 3;
            onJaugeChange?.Invoke();
        }

        // Check if add score per second
        _lasTimeAddedScore += Time.deltaTime;
        if (_lasTimeAddedScore > 1f)
        {
            points += _addScorePerSecond;
            PointsLittlePopUpAnimation();
            _lasTimeAddedScore = 0f;
        }

        jaugeReset += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "+5":
                points += bonus1 * jauge;
                PointsBigPopUpAnimation();

                jauge++;
                jaugeReset = 0f;
                onJaugeChange?.Invoke();

                SoundManager.instance.PlayRandomSoundFXClip(Bonus, transform);

                CollectibleAnimationAndDeactivate(collision.gameObject);
                break;

            case "Wall":
                Debug.Log($"Player collided with a wall - {collision.gameObject.name}");
                jauge++;
                onJaugeChange?.Invoke();
                points += _addScoreOnWallCollision * jauge;
                PointsBigPopUpAnimation();

                jaugeReset = 0f;
                crashCount++;

                SoundManager.instance.PlaySoundFXClip(Wall, transform);

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
                        WaitAndLoadScene();
                        break;
                }
                break;

            case "+10":
                points += bonus2 * jauge;
                PointsBigPopUpAnimation();

                jauge++;
                onJaugeChange?.Invoke();
                jaugeReset = 0f;
                
                SoundManager.instance.PlayRandomSoundFXClip(Bonus, transform);

                CollectibleAnimationAndDeactivate(collision.gameObject);
                break;

            case "-5":
                points -= malus1;
                PointsBigPopUpAnimation();

                SoundManager.instance.PlayRandomSoundFXClip(Malus, transform);

                CollectibleAnimationAndDeactivate(collision.gameObject);
                break;

            case "-10":
                points -= malus2;
                PointsBigPopUpAnimation();

                SoundManager.instance.PlayRandomSoundFXClip(Malus, transform);

                CollectibleAnimationAndDeactivate(collision.gameObject);
                break;

            case "Oil":
                Debug.Log("Player hit oil.");

                jaugeReset = 10f;

                SoundManager.instance.PlaySoundFXClip(Oil, transform);

                CollectibleAnimationAndDeactivate(collision.gameObject);

                OilTurnCamera(); 
                break;

            case "Hole":
                SoundManager.instance.PlaySoundFXClip(Fall, transform);
                crashed = true;
                WaitAndLoadScene();
                break;

            case "x2":
                points *= 2;
                PointsBigPopUpAnimation();

                SoundManager.instance.PlayRandomSoundFXClip(Bonus, transform);

                CollectibleAnimationAndDeactivate(collision.gameObject);
                break;

            case "Kirby":
                points += _addScoreOnKirbyCollision;
                PointsBigPopUpAnimation();

                SoundManager.instance.PlaySoundFXClip(Kirby, transform);

                CollectibleAnimationAndDeactivate(collision.gameObject);
                break;

            default:
                Debug.Log("Player collected something.");
                break;
        }

    }

    private void OilTurnCamera()
    {
        Quaternion originalRotation = _camera.transform.rotation;

        _camera.transform.DORotate(new Vector3(0, 0, 270f), _turnCameraOilDuration / 2, RotateMode.FastBeyond360)
            .OnComplete(() => { _camera.transform.DORotateQuaternion(originalRotation, _turnCameraOilDuration / 2); });
    
    }
    private void PointsBigPopUpAnimation()
    {
        _UIPointsGO.transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutBack)
         .OnComplete(() => _UIPointsGO.transform.DOScale(1f, 0.2f));
    }

    private void PointsLittlePopUpAnimation()
    {
        _UIPointsGO.transform.DOScale(.95f, 0.2f).SetEase(Ease.OutBack)
         .OnComplete(() => _UIPointsGO.transform.DOScale(1f, 0.1f));
    }

    private void CollectibleAnimationAndDeactivate(GameObject collectible)
    {
        float startScale = collectible.transform.localScale.x;
        Sequence seq = DOTween.Sequence();

        seq.Append(collectible.transform.DOScale(startScale * 1.3f, _collectibleAnimationTime).SetEase(Ease.OutBack));
        seq.Append(collectible.transform.DOScale(startScale, _collectibleAnimationTime / 2));
        seq.OnComplete(() => collectible.SetActive(false));
    }

    public void WaitAndLoadScene()
    {
        StopAllMovements();
        StartCoroutine(WaitAndLoadEndScene());
    }

    private IEnumerator WaitAndLoadEndScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("EndScreens");
    }

    private void StopAllMovements()
    {
        // Stop Van Movement
        VanMovement vanMove = GetComponent<VanMovement>();
        if (vanMove != null) vanMove.CanMove = false;

        // Stop Road Movement
        RandomGenerator.Instance.CurrentSpeed = 0f;

        // Stop points
        _canGainPoints = false;
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