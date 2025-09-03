using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    public float speed = 5f; 
    public float limite_X = 1.5f; 
    float moveInput;

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        Vector3 newPosition = transform.position;
        newPosition.x -= moveInput * speed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -limite_X, limite_X);

        transform.position = newPosition;
    }
}
