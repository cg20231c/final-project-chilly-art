// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
 
// public class GravityGun : MonoBehaviour
// {
 
//     [SerializeField] Camera cam;
//     [SerializeField] float maxGrabDistance = 10f, throwForce = 20f, lerpSpeed = 100f, scrollSpeed = 250f;
//     [SerializeField] Transform objectHolder; 
 
//     Rigidbody grabbedRB;
 
//     void Update()
//     {
//         if(grabbedRB)
//         {
//             objectHolder.transform.position = objectHolder.transform.position + cam.transform.forward * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

//             grabbedRB.MovePosition(Vector3.Lerp(grabbedRB.position, objectHolder.transform.position, Time.deltaTime * lerpSpeed));
//             grabbedRB.MoveRotation(Quaternion.Lerp(grabbedRB.rotation, objectHolder.transform.rotation, Time.deltaTime * lerpSpeed)); // Add rotation

 
//             if(Input.GetMouseButtonDown(0))
//             {
//                 grabbedRB.isKinematic = false;
//                 grabbedRB.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
//                 grabbedRB = null;
//             }
//         }
 
//         if(Input.GetKeyDown(KeyCode.E))
//         {
//             if(grabbedRB)
//             {
//                 grabbedRB.isKinematic = false;
//                 grabbedRB = null;
//             }
//             else
//             {
//                 RaycastHit hit;
//                 Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
//                 if(Physics.Raycast(ray, out hit, maxGrabDistance))
//                 {
//                     grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
//                     if(grabbedRB)
//                     {
//                         grabbedRB.isKinematic = true;
//                     }
//                 }
//             }
//         }
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GravityGun : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, throwForce = 20f, lerpSpeed = 100f, scrollSpeed = 250f;
    [SerializeField] Transform objectHolder; 
 
    Rigidbody grabbedRB;
    bool isRotating = false;

    bool isFreeze = false;
 
    void Update()
    {
        if(grabbedRB)
        {
            objectHolder.transform.position = objectHolder.transform.position + cam.transform.forward * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

            if (isRotating && Input.GetMouseButton(1))
            {
                float rotationX = Input.GetAxis("Mouse X") * lerpSpeed * Time.deltaTime;
                float rotationY = Input.GetAxis("Mouse Y") * lerpSpeed * Time.deltaTime;
                objectHolder.transform.Rotate(Vector3.up, -rotationX);
                objectHolder.transform.Rotate(Vector3.right, rotationY);
            }

            grabbedRB.MovePosition(Vector3.Lerp(grabbedRB.position, objectHolder.transform.position, Time.deltaTime * lerpSpeed));
            grabbedRB.MoveRotation(Quaternion.Lerp(grabbedRB.rotation, objectHolder.transform.rotation, Time.deltaTime * lerpSpeed));
 
            if(Input.GetMouseButtonDown(0))
            {
                grabbedRB.isKinematic = false;
                grabbedRB.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
                grabbedRB = null;
            }
        }
 
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(grabbedRB)
            {
                grabbedRB.isKinematic = false;
                grabbedRB = null;
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if(Physics.Raycast(ray, out hit, maxGrabDistance))
                {
                    grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    if(grabbedRB)
                    {
                        grabbedRB.isKinematic = true;
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            isRotating = !isRotating;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Make a logic if Q is pressed for the first time, freeze the object but when it picked up again, unfreeze it
            isFreeze = !isFreeze;
            if (isFreeze)
            {
                grabbedRB.constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                grabbedRB.constraints = RigidbodyConstraints.None;
            }

        }
    }
}