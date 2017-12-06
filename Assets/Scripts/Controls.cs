using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	private OVRInput.Controller RightHand=OVRInput.Controller.RTouch,LeftHand=OVRInput.Controller.LTouch;
	private Renderer rend;
	private Vector3 eulerR,eulerL;
	private bool XYZW,YZXW,XZYW;
	private Renderer Xrend,Yrend,Zrend;
	private Quaternion DownR, DownL,rotR,rotL;
	private GameObject X,Y,Z,Planes;
	private Matrix4x4 T=Matrix4x4.identity,A=Matrix4x4.identity,B=Matrix4x4.identity;

	// Use this for initialization
	void Start() 
	{
		rend = GetComponent<Renderer> ();
		Planes = GameObject.Find ("Planes");
		X = Planes.transform.Find ("YZXW").gameObject;
		Y = Planes.transform.Find ("XZYW").gameObject;
		Z = Planes.transform.Find ("XYZW").gameObject;
		Xrend = X.GetComponent<Renderer> ();
		Yrend = Y.GetComponent<Renderer> ();
		Zrend = Z.GetComponent<Renderer> ();
		Xrend.material.color = Color.green;
		Yrend.material.color = Color.red;
		Zrend.material.color = Color.red;
		/*X.SetActive (true);
		Y.SetActive (false);
		Z.SetActive (false);*/
		XYZW = false;
		YZXW = true;
		XZYW = false;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (OVRInput.GetDown (OVRInput.RawButton.A) && XYZW == true) {
			XYZW = false;
			Zrend.material.color = Color.red;
			//Z.SetActive (false);
			YZXW = true;
			Xrend.material.color = Color.green;
			//X.SetActive (true);

		} else if (OVRInput.GetDown (OVRInput.RawButton.A) && YZXW == true) {
			YZXW = false;
			Xrend.material.color = Color.red;
			//X.SetActive (false);
			XZYW = true;
			Yrend.material.color = Color.green;
			//Y.SetActive (true);

		} else if (OVRInput.GetDown (OVRInput.RawButton.A) && XZYW == true) {
			XZYW = false;
			Yrend.material.color = Color.red;
			//Y.SetActive (false);
			XYZW = true;
			Zrend.material.color = Color.green;
			//Z.SetActive (true);
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
			A = Matrix4x4.identity;
			B = Matrix4x4.identity;
		}

		Quaternion R = Quaternion.Inverse(DownR)*rotR;
		Quaternion L = Quaternion.Inverse(DownL)*rotL;

		eulerL= L.eulerAngles * (Mathf.PI / 180);
		eulerR =R.eulerAngles * (Mathf.PI / 180);

		if (XYZW) {
			if (OVRInput.Get (OVRInput.RawButton.RHandTrigger)) {
				A = new Matrix4x4 (
					new Vector4 (Mathf.Cos (eulerR.z), (-1) * Mathf.Sin (eulerR.z), 0f, 0f),
					new Vector4 (Mathf.Sin (eulerR.z), Mathf.Cos (eulerR.z), 0f, 0f),
					new Vector4 (0f, 0f, 1, 0),
					new Vector4 (0f, 0f, 0, 1));
			}
			if (OVRInput.Get (OVRInput.RawButton.LHandTrigger) && XYZW == true) {
				B = new Matrix4x4 (
					new Vector4 (1, 0, 0f, 0f),
					new Vector4 (0, 1, 0f, 0f),
					new Vector4 (0f, 0f, Mathf.Cos (eulerL.z), (-1) * Mathf.Sin (eulerL.z)),
					new Vector4 (0f, 0f, Mathf.Sin (eulerL.z), Mathf.Cos (eulerL.z)));
			}
			rend.material.SetMatrix ("_fdtransform", A*B*T);
		}

		if (YZXW) {
			if (OVRInput.Get (OVRInput.RawButton.RHandTrigger)) {
				A= new Matrix4x4 (
					new Vector4 (1, 0, 0, 0),
					new Vector4 (0, Mathf.Cos (eulerR.z), Mathf.Sin (eulerR.z), 0),
					new Vector4 (0, (-1) * Mathf.Sin (eulerR.z), Mathf.Cos (eulerR.z), 0),
					new Vector4 (0, 0, 0, 1));
			}
			if (OVRInput.Get (OVRInput.RawButton.LHandTrigger)) {
				B = new Matrix4x4(
					new Vector4 (Mathf.Cos (eulerL.z), 0, 0, Mathf.Sin (eulerL.z)),
					new Vector4 (0, 1, 0, 0),
					new Vector4 (0, 0,1, 0),
					new Vector4 ((-1) * Mathf.Sin (eulerL.z), 0, 0, Mathf.Cos (eulerL.z)));
			}
			rend.material.SetMatrix ("_fdtransform", A*B*T);
		}

		if (XZYW) {
			if (OVRInput.Get (OVRInput.RawButton.RHandTrigger)) {
				A = new Matrix4x4 (
					new Vector4 (Mathf.Cos (eulerR.z), 0, (-1) * Mathf.Sin (eulerR.z), 0),
					new Vector4 (0, 1, 0, 0),
					new Vector4 (Mathf.Sin (eulerR.z), 0, Mathf.Cos (eulerR.z), 0),
					new Vector4 (0, 0, 0, 1));
			}
			if (OVRInput.Get (OVRInput.RawButton.LHandTrigger)) {
				B = new Matrix4x4(
					new Vector4 (1, 0, 0, 0),
					new Vector4 (0, Mathf.Cos (eulerL.z), 0, (-1) * Mathf.Sin (eulerL.z)),
					new Vector4 (0, 0,1, 0),
					new Vector4 (0, Mathf.Sin (eulerL.z), 0, Mathf.Cos (eulerL.z)));
					}
			rend.material.SetMatrix ("_fdtransform", A*B*T);
		}

		if (OVRInput.GetDown (OVRInput.RawButton.B)) {
			A = Matrix4x4.identity;
			B = Matrix4x4.identity;
			T = Matrix4x4.identity;
		}
	}
}
