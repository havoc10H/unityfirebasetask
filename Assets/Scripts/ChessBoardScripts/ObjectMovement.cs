using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private Vector3 mouseOrigin;
    private bool isRotating;

    private void Update()
    {
        // Rotate the object based on mouse movement
        if (Input.GetMouseButtonDown(0))
        {
            mouseOrigin = Input.mousePosition;
            isRotating = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
            transform.RotateAround(transform.position, transform.up, -pos.x * 5);
            transform.RotateAround(transform.position, transform.right, pos.y * 5);
        }
    }
}