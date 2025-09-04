using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    public float speed = 10f; 
    public float limite_X = 1.5f; 
    //public float speed_Y = 3f;
    //public float limite_Y = 2.75f;
    float moveInput_X;
    //float moveInput_Y;

    void FixedUpdate()
    {
        moveInput_X = Input.GetAxis("Horizontal");
        //moveInput_Y = Input.GetAxis("Vertical");

        Vector3 newPosition = transform.position;
        newPosition.x += moveInput_X * speed * Time.deltaTime;
        //newPosition.y += moveInput_Y * speed_Y * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -limite_X, limite_X);
        //newPosition.y = Mathf.Clamp(newPosition.y, -limite_Y, limite_Y);

        transform.position = newPosition;
    }
}
