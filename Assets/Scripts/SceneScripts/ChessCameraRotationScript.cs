using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessCameraRotationScript : MonoBehaviour
{
    public float rotationSpeed = 0.3f;

    private Vector3 initialMousePosition;

    private Vector3 initialCameraPosition;
    public float zoomSpeed = 50.0f;
    public float minZoomLevel = 1f;
    public float maxZoomLevel = 10f;
    public GameObject RoateAround;

    [Header("Limit Rot")]
    public float maxXRot = 90f;
    public float minXRot = 45f;

    void Start()
    {
        initialCameraPosition = Camera.main.transform.position;
    }
    void Update()
    {        
        if (Input.GetMouseButtonDown(0))
        {
            initialMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 initialPosition = Camera.main.transform.position;

            Vector3 mouseDelta = Input.mousePosition - initialMousePosition;
            float rotationX = -mouseDelta.y * rotationSpeed;
            float rotationY = mouseDelta.x * rotationSpeed;

            // Limit the rotation in the y-axis
            Vector3 rotation = Camera.main.transform.rotation.eulerAngles;
            
            Camera.main.transform.RotateAround(transform.position, Vector3.up, rotationY); //left
            RoateAround.transform.RotateAround(transform.position, Vector3.up, -rotationY); //left
            Camera.main.transform.RotateAround(RoateAround.transform.position, Camera.main.transform.right, rotationX); //up
            // Debug.Log(Camera.main.transform.rotation.eulerAngles.x);

            // Vector3 playerEulerAngles = Camera.main.transform.rotation.eulerAngles;
            // playerEulerAngles.x = (playerEulerAngles.x > 180) ? playerEulerAngles.x - 360 : playerEulerAngles.x;

            // // Limit the rotation in the x-axis
            // playerEulerAngles.x = Mathf.Clamp(playerEulerAngles.x, minXRot, maxXRot);
            
            // Camera.main.transform.rotation = Quaternion.Euler(playerEulerAngles);

            // // Reset the position if rotation didn't change
            // if (Camera.main.transform.rotation.eulerAngles.x == rotation.x)
            // {
            //     Camera.main.transform.position = initialPosition;
            // }

            initialMousePosition = Input.mousePosition;

            // Vector3 playerEulerAngles = Camera.main.transform.rotation.eulerAngles;
            // playerEulerAngles.x = (playerEulerAngles.x > 180) ? playerEulerAngles.x - 360 : playerEulerAngles.x;
            // playerEulerAngles.x = Mathf.Clamp(playerEulerAngles.x, minXRot, maxXRot);
            // Camera.main.transform.rotation = Quaternion.Euler(playerEulerAngles); 

            // // Limit the rotation in the x-axis
            // float currentRotationX = Camera.main.transform.rotation.eulerAngles.x;
            // float clampedRotationX = Mathf.Clamp(currentRotationX, minXRot, maxXRot);
            // float rotationDifferenceX = clampedRotationX - currentRotationX;

            // Camera.main.transform.RotateAround(RoateAround.transform.position, Camera.main.transform.right, rotationDifferenceX);
            // // Debug.Log("2: " + playerEulerAngles.x);
            // // Camera.main.transform.rotation.eulerAngles.x = Mathf.Clamp(Camera.main.transform.rotation.eulerAngles.x);
        }

        // float zoomInput = Input.GetAxis("Mouse ScrollWheel"); 
        // Camera.main.fieldOfView -= zoomInput * zoomSpeed;
        // Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 10.0f, 100.0f); // Adjust min/max zoom values
    }
}