// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PortalTeleporter : MonoBehaviour {

// 	public Transform player;
// 	public Transform receiver;

// 	private bool playerIsOverlapping = false;

// 	// Update is called once per frame
// 	void Update () {
// 		if (playerIsOverlapping)
// 		{
// 			Vector3 portalToPlayer = player.position - transform.position;
// 			float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

// 			// If this is true: The player has moved across the portal
// 			if (dotProduct < 0f)
// 			{
// 				// Teleport him!
// 				float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
// 				rotationDiff += 180;
// 				player.Rotate(Vector3.up, rotationDiff);

// 				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
// 				player.position = receiver.position + positionOffset;

// 				playerIsOverlapping = false;
// 			}
// 		}
// 	}

// 	void OnTriggerEnter(Collider other)
// 	{
// 		if (other.CompareTag("Player"))
// 		{
// 			playerIsOverlapping = true;
// 			Debug.Log("Player is overlapping with " + transform.name);
// 		}
// 	}

// 	void OnTriggerExit(Collider other)
// 	{
// 		if (other.CompareTag("Player"))
// 		{
// 			playerIsOverlapping = false;
// 			Debug.Log("Player is no longer overlapping with " + transform.name);
// 		}
// 	}
// }

using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform receiver;

    private bool playerIsOverlapping = false;

    void Update()
    {
        if (playerIsOverlapping)
        {
			
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

			// Debug.Log("Dot product: " + dotProduct + " with " + transform.name);

            // If this is true: The player has moved across the portal
            if (dotProduct < 0f)
            {
                TeleportPlayer();
				Debug.Log("Teleporting player to " + receiver.position);
            }
        }
		// Debug.Log("Player Current Position: " + player.position);
				player.GetComponent<CharacterController>().enabled = true;

	}

    void TeleportPlayer()
    {
        	//Set character controller to be disabled while teleporting
			player.GetComponent<CharacterController>().enabled = false;

			float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
			rotationDiff += 180;
			player.Rotate(Vector3.up, rotationDiff);
			Vector3 portalToPlayer = player.position - transform.position;
			Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
			player.position = receiver.position + positionOffset;
			playerIsOverlapping = false;
			player.GetComponent<CharacterController>().enabled = true;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOverlapping = true;
            Debug.Log("Player is overlapping with " + transform.name);
        }
    }

    void OnTriggerExit(Collider other)
    {        if (other.CompareTag("Player"))
        {
            playerIsOverlapping = false;
            Debug.Log("Player is no longer overlapping with " + transform.name);
        }
    }
}
