using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitFDTransform : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(OVRInput.GetDown(OVRInput.RawButton.A)){
			Debug.Log ("rotate");
		Matrix4x4 M = new Matrix4x4 (
				new Vector4 (Mathf.Cos(0.0f*Mathf.PI), (-1)*Mathf.Sin(0.0f*Mathf.PI), 0f, 0f),
				new Vector4 (Mathf.Sin(0.0f*Mathf.PI),Mathf.Cos(0.0f*Mathf.PI), 0f, 0f),
				new Vector4 (0f, 0f, Mathf.Cos(0.25f*Mathf.PI), (-1)*Mathf.Sin(0.25f*Mathf.PI)),
				new Vector4 (0f, 0f,Mathf.Sin(0.25f*Mathf.PI),Mathf.Cos(0.25f*Mathf.PI)));
		gameObject.GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		}
		if (OVRInput.GetDown (OVRInput.RawButton.B)) {
			Debug.Log ("reset");
			Matrix4x4 M = Matrix4x4.identity;
			gameObject.GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		}
	}
}
