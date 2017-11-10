using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	private OVRInput.Controller RightHand=OVRInput.Controller.RTouch,LeftHand=OVRInput.Controller.LTouch;

	private Vector3 eulerR,eulerL;
	private bool XYZW,YZXW,XZYW;
	private Quaternion refR, refL,rotR,rotL,stopL,stopR;
	// Use this for initialization
	void Start () 
	{
		stopL=OVRInput.GetLocalControllerRotation (LeftHand);
		stopR=OVRInput.GetLocalControllerRotation (RightHand);
		XYZW = false;
		YZXW = true;
		XZYW = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (OVRInput.GetDown (OVRInput.RawButton.A) && XYZW == true) {
			XYZW = false;
			YZXW = true;
		} else if (OVRInput.GetDown (OVRInput.RawButton.A) && YZXW == true) {
			YZXW = false;
			XZYW = true;
		} else if (OVRInput.GetDown (OVRInput.RawButton.A) && XZYW == true) {
			XZYW = false;
			XYZW = true;
		}

		rotR = OVRInput.GetLocalControllerRotation (RightHand);
		rotL = OVRInput.GetLocalControllerRotation (LeftHand);

		if (OVRInput.GetDown (OVRInput.RawButton.RHandTrigger) || OVRInput.GetDown (OVRInput.RawButton.LHandTrigger)) {
			refR = OVRInput.GetLocalControllerRotation (RightHand);
			refL = OVRInput.GetLocalControllerRotation (LeftHand);
		}
			
		Quaternion R = (Quaternion.Inverse(rotR)*refR);
		Quaternion L = (Quaternion.Inverse(rotL)*refL);

		if (OVRInput.GetUp (OVRInput.RawButton.RHandTrigger) || OVRInput.GetDown (OVRInput.RawButton.LHandTrigger)) {
			stopR = (Quaternion.Inverse (rotR) * refR);
			stopL= (Quaternion.Inverse(rotL)*refL);
		}

		eulerL= L.eulerAngles * (Mathf.PI / 180);
		eulerR = R.eulerAngles * (Mathf.PI / 180);

		if ((OVRInput.Get (OVRInput.RawButton.RHandTrigger)||OVRInput.Get (OVRInput.RawButton.LHandTrigger))&&XYZW==true) {
			Matrix4x4 XY = new Matrix4x4 (
				new Vector4 (Mathf.Cos (eulerR.z), (-1) * Mathf.Sin (eulerR.z), 0f, 0f),
				new Vector4 (Mathf.Sin (eulerR.z), Mathf.Cos (eulerR.z), 0f, 0f),
				new Vector4 (0f, 0f, Mathf.Cos (eulerL.z), (-1) * Mathf.Sin (eulerL.z)),
				new Vector4 (0f, 0f, Mathf.Sin (eulerL.z), Mathf.Cos (eulerL.z)));
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", XY);
		}
		if ((OVRInput.Get (OVRInput.RawButton.RHandTrigger)||OVRInput.Get (OVRInput.RawButton.LHandTrigger))&&YZXW==true) {
			Matrix4x4 YZ = new Matrix4x4 (
				new Vector4 (Mathf.Cos (eulerL.z), 0, 0, Mathf.Sin (eulerL.z)),
				new Vector4 (0, Mathf.Cos (eulerR.z), Mathf.Sin (eulerR.z), 0),
				new Vector4 (0, (-1) * Mathf.Sin (eulerR.z), Mathf.Cos (eulerR.z), 0),
				new Vector4 ((-1) * Mathf.Sin (eulerL.z), 0, 0, Mathf.Cos (eulerL.z)));
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", YZ);
		}
		if ((OVRInput.Get (OVRInput.RawButton.RHandTrigger) || OVRInput.Get (OVRInput.RawButton.LHandTrigger)) && XZYW == true) {
			Matrix4x4 XZ = new Matrix4x4 (
				new Vector4 (Mathf.Cos (eulerR.z), 0, (-1) * Mathf.Sin (eulerR.z), 0),
				new Vector4 (0, Mathf.Cos (eulerL.z), 0, (-1) * Mathf.Sin (eulerL.z)),
				new Vector4 (Mathf.Sin (eulerR.z), 0, Mathf.Cos (eulerR.z), 0),
				new Vector4 (0, Mathf.Sin (eulerL.z), 0, Mathf.Cos (eulerL.z))
			);
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", XZ);
		}
		if (OVRInput.GetDown (OVRInput.RawButton.Start)) {
			gameObject.GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", Matrix4x4.identity);

		}
	}
	void DrawPlanes(){

	}
}
