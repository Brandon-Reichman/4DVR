using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	private OVRInput.Controller RightHand=OVRInput.Controller.RTouch,LeftHand=OVRInput.Controller.LTouch;

	private Renderer rend;
	private Vector3 eulerR,eulerL;
	private bool XYZW,YZXW,XZYW;
	private Quaternion DownR, DownL,rotR,rotL,UpL,UpR;
	private GameObject X,Y,Z,Planes;
	private Matrix4x4 T;
	// Use this for initialization
	void Start() 
	{
		rend = GetComponent<Renderer> ();
		Planes = GameObject.Find ("Planes");
		X = Planes.transform.Find ("YZXW").gameObject;
		Y = Planes.transform.Find ("XZYW").gameObject;
		Z = Planes.transform.Find ("XYZW").gameObject;
		X.SetActive (true);
		Y.SetActive (false);
		Z.SetActive (false);
		UpL = Quaternion.identity;
		UpR = Quaternion.identity;
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
			DownR = OVRInput.GetLocalControllerRotation (RightHand);
			DownL = OVRInput.GetLocalControllerRotation (LeftHand);
			T = rend.material.GetMatrix ("_fdtransform");
		}

		if (OVRInput.GetUp (OVRInput.RawButton.RHandTrigger) || OVRInput.GetUp (OVRInput.RawButton.LHandTrigger)) {
			DownR = OVRInput.GetLocalControllerRotation (RightHand);
			DownL = OVRInput.GetLocalControllerRotation (LeftHand);
			T = rend.material.GetMatrix ("_fdtransform");
		}

		Quaternion R = Quaternion.Inverse(DownR)*rotR;
		Quaternion L = Quaternion.Inverse(DownL)*rotL;

		eulerL= L.eulerAngles * (Mathf.PI / 180);
		eulerR =R.eulerAngles * (Mathf.PI / 180);

		if (OVRInput.Get (OVRInput.RawButton.RHandTrigger)&&XYZW==true) {
			Matrix4x4 XY = new Matrix4x4 (
				new Vector4 (Mathf.Cos (eulerR.z), (-1) * Mathf.Sin (eulerR.z), 0f, 0f),
				new Vector4 (Mathf.Sin (eulerR.z), Mathf.Cos (eulerR.z), 0f, 0f),
				new Vector4 (0f, 0f, 1, 0),
				new Vector4 (0f, 0f,0,1));
			rend.material.SetMatrix ("_fdtransform", XY*T);
		}
		if (OVRInput.Get (OVRInput.RawButton.LHandTrigger) && XYZW == true) {
			Matrix4x4 ZW = new Matrix4x4 (
				new Vector4 (1,0, 0f, 0f),
				new Vector4 (0, 1, 0f, 0f),
				new Vector4 (0f, 0f, Mathf.Cos (eulerL.z), (-1) * Mathf.Sin (eulerL.z)),
				new Vector4 (0f, 0f, Mathf.Sin (eulerL.z), Mathf.Cos (eulerL.z)));
			rend.material.SetMatrix ("_fdtransform", ZW*T);
		}
		if ((OVRInput.Get (OVRInput.RawButton.RHandTrigger)&&OVRInput.Get (OVRInput.RawButton.LHandTrigger))&&XYZW==true) {
			Matrix4x4 XYZW = new Matrix4x4 (
				new Vector4 (Mathf.Cos (eulerR.z), (-1) * Mathf.Sin (eulerR.z), 0f, 0f),
				new Vector4 (Mathf.Sin (eulerR.z), Mathf.Cos (eulerR.z), 0f, 0f),
				new Vector4 (0f, 0f, Mathf.Cos (eulerL.z), (-1) * Mathf.Sin (eulerL.z)),
				new Vector4 (0f, 0f, Mathf.Sin (eulerL.z), Mathf.Cos (eulerL.z)));
			rend.material.SetMatrix ("_fdtransform", XYZW*T);
		}
		if (OVRInput.Get (OVRInput.RawButton.RHandTrigger) && YZXW == true) {
			Matrix4x4 YZ = new Matrix4x4 (
				new Vector4 (1, 0, 0, 0),
				new Vector4 (0, Mathf.Cos (eulerR.z), Mathf.Sin (eulerR.z), 0),
				new Vector4 (0, (-1) * Mathf.Sin (eulerR.z), Mathf.Cos (eulerR.z), 0),
				new Vector4 (0, 0, 0, 1));
			rend.material.SetMatrix ("_fdtransform", YZ*T);
		}
		if (OVRInput.Get (OVRInput.RawButton.LHandTrigger) && YZXW == true) {
			Matrix4x4 XW = new Matrix4x4(
				new Vector4 (Mathf.Cos (eulerL.z), 0, 0, Mathf.Sin (eulerL.z)),
				new Vector4 (0, 1, 0, 0),
				new Vector4 (0, 0,1, 0),
				new Vector4 ((-1) * Mathf.Sin (eulerL.z), 0, 0, Mathf.Cos (eulerL.z)));
			rend.material.SetMatrix ("_fdtransform", XW*T);
		}
		if ((OVRInput.Get (OVRInput.RawButton.RHandTrigger)&&OVRInput.Get (OVRInput.RawButton.LHandTrigger))&&YZXW==true) {
			Matrix4x4 YZXW = new Matrix4x4 (
				new Vector4 (Mathf.Cos (eulerL.z), 0, 0, Mathf.Sin (eulerL.z)),
				new Vector4 (0, Mathf.Cos (eulerR.z), Mathf.Sin (eulerR.z), 0),
				new Vector4 (0, (-1) * Mathf.Sin (eulerR.z), Mathf.Cos (eulerR.z), 0),
				new Vector4 ((-1) * Mathf.Sin (eulerL.z), 0, 0, Mathf.Cos (eulerL.z)));
			rend.material.SetMatrix ("_fdtransform", YZXW*T);
		}
		if (OVRInput.Get (OVRInput.RawButton.RHandTrigger) && XZYW == true) {
			Matrix4x4 XZ=new Matrix4x4(
				new Vector4 (Mathf.Cos (eulerR.z), 0, (-1) * Mathf.Sin (eulerR.z), 0),
				new Vector4 (0,1, 0, 0),
				new Vector4 (Mathf.Sin (eulerR.z), 0, Mathf.Cos (eulerR.z), 0),
				new Vector4 (0, 0, 0,1));
			rend.material.SetMatrix ("_fdtransform", XZ*T);
		}
		if(OVRInput.Get(OVRInput.RawButton.LHandTrigger)&& XZYW == true){
			Matrix4x4 YW = new Matrix4x4 (
				              new Vector4 (1, 0, 0, 0),
				              new Vector4 (0, Mathf.Cos (eulerL.z), 0, (-1) * Mathf.Sin (eulerL.z)),
				              new Vector4 (0, 0,1, 0),
				              new Vector4 (0, Mathf.Sin (eulerL.z), 0, Mathf.Cos (eulerL.z)));
			rend.material.SetMatrix ("_fdtransform", YW*T);
		}
		if ((OVRInput.Get (OVRInput.RawButton.RHandTrigger) && OVRInput.Get (OVRInput.RawButton.LHandTrigger)) && XZYW == true) {
			Matrix4x4 XZ = new Matrix4x4 (
				new Vector4 (Mathf.Cos (eulerR.z), 0, (-1) * Mathf.Sin (eulerR.z), 0),
				new Vector4 (0, Mathf.Cos (eulerL.z), 0, (-1) * Mathf.Sin (eulerL.z)),
				new Vector4 (Mathf.Sin (eulerR.z), 0, Mathf.Cos (eulerR.z), 0),
				new Vector4 (0, Mathf.Sin (eulerL.z), 0, Mathf.Cos (eulerL.z))
			);
			rend.material.SetMatrix ("_fdtransform", XZ*T);
		}
		if (OVRInput.GetDown (OVRInput.RawButton.B)) {
			rend.material.SetMatrix ("_fdtransform", Matrix4x4.identity);
			UpR = Quaternion.identity;
			UpL = Quaternion.identity;
		}
	}
}
