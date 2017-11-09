using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	private OVRInput.Controller RightHand=OVRInput.Controller.RTouch,LeftHand=OVRInput.Controller.LTouch;

	private Vector3 eulerR,eulerL;
	private bool XYZW,YZXW,XZYW;
	// Use this for initialization
	void Start () 
	{
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
		eulerR = OVRInput.GetLocalControllerRotation (RightHand).eulerAngles;
		eulerL = OVRInput.GetLocalControllerRotation (LeftHand).eulerAngles;
		if ((OVRInput.Get (OVRInput.RawButton.RHandTrigger)||OVRInput.Get (OVRInput.RawButton.LHandTrigger))&&XYZW==true) {
			Matrix4x4 XY = new Matrix4x4 (
				              new Vector4 (Mathf.Cos (-eulerR.z / 100), (-1) * Mathf.Sin (-eulerR.z / 100), 0f, 0f),
				              new Vector4 (Mathf.Sin (-eulerR.z / 100), Mathf.Cos (-eulerR.z / 100), 0f, 0f),
				              new Vector4 (0f, 0f, Mathf.Cos (-eulerL.z / 100), (-1) * Mathf.Sin (-eulerL.z / 100)),
				              new Vector4 (0f, 0f, Mathf.Sin (-eulerL.z / 100), Mathf.Cos (-eulerL.z / 100)));
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", XY);
		}
		if ((OVRInput.Get (OVRInput.RawButton.RHandTrigger)||OVRInput.Get (OVRInput.RawButton.LHandTrigger))&&YZXW==true) {
			Matrix4x4 YZ = new Matrix4x4 (
				new Vector4 (Mathf.Cos (-eulerL.x / 100), 0, 0, Mathf.Sin (-eulerL.x / 100)),
				new Vector4 (0, Mathf.Cos (-eulerR.x / 100), Mathf.Sin (-eulerR.x / 100), 0),
				new Vector4 (0, (-1) * Mathf.Sin (-eulerR.x / 100), Mathf.Cos (-eulerR.x / 100), 0),
				new Vector4 ((-1) * Mathf.Sin (-eulerL.x / 100), 0, 0, Mathf.Cos (-eulerL.x / 100)));
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", YZ);
		}
		if ((OVRInput.Get (OVRInput.RawButton.RHandTrigger) || OVRInput.Get (OVRInput.RawButton.LHandTrigger)) && XZYW == true) {
			Matrix4x4 XZ = new Matrix4x4 (
				new Vector4 (Mathf.Cos (-eulerR.y/100), 0, (-1) * Mathf.Sin (-eulerR.y/100), 0),
				new Vector4 (0, Mathf.Cos (-eulerL.y/100), 0, (-1) * Mathf.Sin (-eulerL.y/100)),
				new Vector4 (Mathf.Sin (-eulerR.y/100), 0, Mathf.Cos (-eulerR.y/100), 0),
				new Vector4 (0, Mathf.Sin (-eulerL.y/100), 0, Mathf.Cos (-eulerL.y/100))
			);
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", XZ);
		}
		if (OVRInput.GetDown (OVRInput.RawButton.Start)) {
			Matrix4x4 M = Matrix4x4.identity;
			gameObject.GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		}
		Debug.Log ("Right Hand: " + eulerR + "\n Left Hand: " + eulerL);
	}
}
