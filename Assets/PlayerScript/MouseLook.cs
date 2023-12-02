using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f; // Mouse sensitivity
    public Transform playerBody; // Player body

    float xRotation = 0f; // Rotation on the X axis

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Get the mouse X axis
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // Get the mouse Y axis

        xRotation -= mouseY; // Subtract the mouse Y axis from the rotation on the X axis
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp the rotation on the X axis

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Rotate the camera on the X axis
        playerBody.Rotate(Vector3.up * mouseX); // Rotate the player body on the Y axis
    }
}
