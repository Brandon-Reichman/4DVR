using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

    public OVRInput.Controller controller;
    public float grabRadius;
    public LayerMask grabMask;
    private bool grabbing;
    private GameObject grabbedObject;
    public GameObject sphere;
    private Quaternion lastRotation, currentRotation;
    private Vector3 startPosition;

    private Quaternion initialRotation=Quaternion.Euler(0,0,0);


    private void Start()
    {
		startPosition = sphere.transform.localPosition;
    }
    
	void GrabObject()
    {
        grabbing = true;

        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask);

        if (hits.Length > 0)
        {
            int closestHit = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance > hits[closestHit].distance) closestHit = i;
            }

            grabbedObject = hits[closestHit].transform.gameObject;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            grabbedObject.transform.position = transform.position;
            grabbedObject.transform.parent = transform;
        }
    }
    void DropObject()
    {
        grabbing = false;

        if (grabbedObject !=null)
        {
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = GetAngularVelocity();
            grabbedObject = null;
        }
    }

    Vector3 GetAngularVelocity()
    {
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
    }

    void ResetBall()
    {
        sphere.transform.position = startPosition;
        sphere.transform.rotation = initialRotation;
    }


    
    // Update is called once per frame
    void Update ()
    {
        if(grabbedObject != null)
        {
            lastRotation = currentRotation;
            currentRotation = grabbedObject.transform.rotation;
        }

		if (!grabbing && OVRInput.Get(OVRInput.RawButton.RHandTrigger))GrabObject();
		if (grabbing && OVRInput.Get(OVRInput.RawButton.RHandTrigger)==false)DropObject();

		if (OVRInput.Get(OVRInput.RawButton.A))
			ResetBall();
        
	}
}
