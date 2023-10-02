using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneCameraAnimation : MonoBehaviour
{
    public Transform target; // Reference to the object to rotate around
    public float rotationSpeed = 180f; // Rotation speed in degrees per second

    private float rotationTime = 0f; // Current rotation time

    private void Start()
    {
        // Start the camera rotation animation
        RotateCamera();
    }

    private void RotateCamera()
    {
        // Calculate the rotation angle based on the desired rotation time and rotation speed
        float angle = (360f / rotationSpeed) * 2f;

        // Rotate the empty game object around the target object
        Camera.main.transform.RotateAround(target.position, Vector3.up, angle);

        // Increase the rotation time
        rotationTime += Time.deltaTime;

        // Check if the desired rotation time has been reached
        if (rotationTime >= 2f)
        {
            // Stop the camera rotation animation
            enabled = false;
        }
    }
}
