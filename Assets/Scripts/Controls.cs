using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	private OVRInput.Controller RightHand=OVRInput.Controller.RTouch,LeftHand=OVRInput.Controller.LTouch;

	private Vector3 eulerR,eulerL;
	private bool XYZW,YZXW,XZYW;
	private Quaternion refR, refL,rotR,rotL,stopL,stopR;
	private GameObject X,Y,Z;
	// Use this for initialization
	void Start () 
	{
		X = GameObject.Find ("YZXW");
		Y = GameObject.Find ("XZYW");
		Z = GameObject.Find ("XYZW");
		X.SetActive (true);
		Y.SetActive (false);
		Z.SetActive (false);
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
			Z.SetActive (false);
			YZXW = true;
			X.SetActive (true);

		} else if (OVRInput.GetDown (OVRInput.RawButton.A) && YZXW == true) {
			YZXW = false;
			X.SetActive (false);
			XZYW = true;
			Y.SetActive (true);

		} else if (OVRInput.GetDown (OVRInput.RawButton.A) && XZYW == true) {
			XZYW = false;
			Y.SetActive (false);
			XYZW = true;
			Z.SetActive (true);

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
		if (OVRInput.Get (OVRInput.RawButton.RHandTrigger)&&XYZW==true) {
			Matrix4x4 XY = new Matrix4x4 (
				new Vector4 (Mathf.Cos (eulerR.z), (-1) * Mathf.Sin (eulerR.z), 0f, 0f),
				new Vector4 (Mathf.Sin (eulerR.z), Mathf.Cos (eulerR.z), 0f, 0f),
				new Vector4 (0f, 0f, 1, 0),
				new Vector4 (0f, 0f,0,1));
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", XY);
		}
		if (OVRInput.Get (OVRInput.RawButton.LHandTrigger) && XYZW == true) {
			Matrix4x4 ZW = new Matrix4x4 (
				new Vector4 (1,0, 0f, 0f),
				new Vector4 (0, 1, 0f, 0f),
				new Vector4 (0f, 0f, Mathf.Cos (eulerL.z), (-1) * Mathf.Sin (eulerL.z)),
				new Vector4 (0f, 0f, Mathf.Sin (eulerL.z), Mathf.Cos (eulerL.z)));
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", ZW);
		}
		if ((OVRInput.Get (OVRInput.RawButton.RHandTrigger)&&OVRInput.Get (OVRInput.RawButton.LHandTrigger))&&XYZW==true) {
			Matrix4x4 XYZW = new Matrix4x4 (
				new Vector4 (Mathf.Cos (eulerR.z), (-1) * Mathf.Sin (eulerR.z), 0f, 0f),
				new Vector4 (Mathf.Sin (eulerR.z), Mathf.Cos (eulerR.z), 0f, 0f),
				new Vector4 (0f, 0f, Mathf.Cos (eulerL.z), (-1) * Mathf.Sin (eulerL.z)),
				new Vector4 (0f, 0f, Mathf.Sin (eulerL.z), Mathf.Cos (eulerL.z)));
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", XYZW);
		}
		if (OVRInput.Get (OVRInput.RawButton.RHandTrigger) && YZXW == true) {
			Matrix4x4 YZ = new Matrix4x4 (
				new Vector4 (1, 0, 0, 0),
				new Vector4 (0, Mathf.Cos (eulerR.z), Mathf.Sin (eulerR.z), 0),
				new Vector4 (0, (-1) * Mathf.Sin (eulerR.z), Mathf.Cos (eulerR.z), 0),
				new Vector4 (0, 0, 0, 1));
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", YZ);
		}
		if (OVRInput.Get (OVRInput.RawButton.LHandTrigger) && YZXW == true) {
			Matrix4x4 XW = new Matrix4x4(
				new Vector4 (Mathf.Cos (eulerL.z), 0, 0, Mathf.Sin (eulerL.z)),
				new Vector4 (0, 1, 0, 0),
				new Vector4 (0, 0,1, 0),
				new Vector4 ((-1) * Mathf.Sin (eulerL.z), 0, 0, Mathf.Cos (eulerL.z)));
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", XW);
		}
		if ((OVRInput.Get (OVRInput.RawButton.RHandTrigger)&&OVRInput.Get (OVRInput.RawButton.LHandTrigger))&&YZXW==true) {
			Matrix4x4 YZXW = new Matrix4x4 (
				new Vector4 (Mathf.Cos (eulerL.z), 0, 0, Mathf.Sin (eulerL.z)),
				new Vector4 (0, Mathf.Cos (eulerR.z), Mathf.Sin (eulerR.z), 0),
				new Vector4 (0, (-1) * Mathf.Sin (eulerR.z), Mathf.Cos (eulerR.z), 0),
				new Vector4 ((-1) * Mathf.Sin (eulerL.z), 0, 0, Mathf.Cos (eulerL.z)));
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", YZXW);
		}
		if (OVRInput.Get (OVRInput.RawButton.RHandTrigger) && XZYW == true) {
			Matrix4x4 XZ=new Matrix4x4(
				new Vector4 (Mathf.Cos (eulerR.z), 0, (-1) * Mathf.Sin (eulerR.z), 0),
				new Vector4 (0,1, 0, 0),
				new Vector4 (Mathf.Sin (eulerR.z), 0, Mathf.Cos (eulerR.z), 0),
				new Vector4 (0, 0, 0,1));
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", XZ);
		}
		if(OVRInput.Get(OVRInput.RawButton.LHandTrigger)&& XZYW == true){
			Matrix4x4 YW = new Matrix4x4 (
				              new Vector4 (1, 0, 0, 0),
				              new Vector4 (0, Mathf.Cos (eulerL.z), 0, (-1) * Mathf.Sin (eulerL.z)),
				              new Vector4 (0, 0,1, 0),
				              new Vector4 (0, Mathf.Sin (eulerL.z), 0, Mathf.Cos (eulerL.z)));
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", YW);
		}
		if ((OVRInput.Get (OVRInput.RawButton.RHandTrigger) && OVRInput.Get (OVRInput.RawButton.LHandTrigger)) && XZYW == true) {
			Matrix4x4 XZ = new Matrix4x4 (
				new Vector4 (Mathf.Cos (eulerR.z), 0, (-1) * Mathf.Sin (eulerR.z), 0),
				new Vector4 (0, Mathf.Cos (eulerL.z), 0, (-1) * Mathf.Sin (eulerL.z)),
				new Vector4 (Mathf.Sin (eulerR.z), 0, Mathf.Cos (eulerR.z), 0),
				new Vector4 (0, Mathf.Sin (eulerL.z), 0, Mathf.Cos (eulerL.z))
			);
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", XZ);
		}
		if (OVRInput.GetDown (OVRInput.RawButton.B)) {
			gameObject.GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", Matrix4x4.identity);

		}
	}
}
