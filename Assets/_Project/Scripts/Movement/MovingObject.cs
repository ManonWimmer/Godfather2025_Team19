using UnityEngine;

public class MovingObject : MonoBehaviour
{
    // ----- FIELDS ----- //
    // Kirby
    private bool _isKirby = false;
    private Vector3 _kirbyDirection;
    // ----- FIELDS ----- //

    private void Start()
    {
        _isKirby = gameObject.CompareTag("Kirby");
        GetKirbyRandomDirection();
    }

    private void OnEnable()
    {
        GetKirbyRandomDirection();
    }

    private void GetKirbyRandomDirection()
    {
        if (_isKirby)
        {
            int random = Random.Range(0, 1);

            if (random == 0)
                _kirbyDirection = new Vector3(.75f, 0, 0);
            else
                _kirbyDirection = new Vector3(-.75f, 0, 0);
        }
    }

    void Update()
    {
        // Move object down
        if (_isKirby)
            transform.Translate((-transform.up + _kirbyDirection) * Time.deltaTime * RandomGenerator.Instance.CurrentSpeed);
        else
            transform.Translate(-transform.up * Time.deltaTime * RandomGenerator.Instance.CurrentSpeed);
    }
}
