using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{

    public Transform playerCamera; // Player camera
    public Transform portal; // Portal
    public Transform otherPortal; // Other portal


    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position; // Get the player offset from the other portal        
        transform.position = portal.position + playerOffsetFromPortal; // Set the position of the camera to the position of the portal plus the player offset from the other portal

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation); // Get the angular difference between the portal rotations
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up); // Get the portal rotational difference
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward; // Get the new camera direction
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up); // Set the rotation of the camera to the new camera direction 
    }
}
