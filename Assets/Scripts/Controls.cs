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
		eulerR.x = eulerR.x * (Mathf.PI / 180);
		eulerR.y = eulerR.y * (Mathf.PI / 180);
		eulerR.z = eulerR.z * (Mathf.PI / 180);
		eulerL.x = eulerL.x * (Mathf.PI / 180);
		eulerL.y = eulerL.y * (Mathf.PI / 180);
		eulerL.z = eulerL.z * (Mathf.PI / 180);
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
				new Vector4 (Mathf.Cos (eulerL.x), 0, 0, Mathf.Sin (eulerL.x)),
				new Vector4 (0, Mathf.Cos (eulerR.x), Mathf.Sin (eulerR.x), 0),
				new Vector4 (0, (-1) * Mathf.Sin (eulerR.x), Mathf.Cos (eulerR.x), 0),
				new Vector4 ((-1) * Mathf.Sin (eulerL.x), 0, 0, Mathf.Cos (eulerL.x)));
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", YZ);
		}
		if ((OVRInput.Get (OVRInput.RawButton.RHandTrigger) || OVRInput.Get (OVRInput.RawButton.LHandTrigger)) && XZYW == true) {
			Matrix4x4 XZ = new Matrix4x4 (
				new Vector4 (Mathf.Cos (eulerR.y), 0, (-1) * Mathf.Sin (eulerR.y), 0),
				new Vector4 (0, Mathf.Cos (eulerL.y), 0, (-1) * Mathf.Sin (eulerL.y)),
				new Vector4 (Mathf.Sin (eulerR.y), 0, Mathf.Cos (eulerR.y), 0),
				new Vector4 (0, Mathf.Sin (eulerL.y), 0, Mathf.Cos (eulerL.y))
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
