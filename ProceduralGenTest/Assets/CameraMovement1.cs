using UnityEngine;

public class CameraMovement1 : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 100f; 

    private bool rotateCamera = false;
    private float rotationX = 0f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        if (Input.GetMouseButtonDown(1))
        {
            rotateCamera = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            rotateCamera = false;
        }

        if (rotateCamera)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            rotationX -= mouseY * rotationSpeed * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            transform.rotation = Quaternion.Euler(rotationX, transform.eulerAngles.y + mouseX * rotationSpeed * Time.deltaTime, 0f);
        }
    }
}
