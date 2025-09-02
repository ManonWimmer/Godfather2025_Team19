using UnityEngine;

public class MovingObject : MonoBehaviour
{
    void Update()
    {
        // Move object down
        transform.Translate(-transform.up * Time.deltaTime * RandomGenerator.Instance.CurrentSpeed);
    }
}
