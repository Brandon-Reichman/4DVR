using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitFDTransform : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(OVRInput.Get(OVRInput.RawButton.A)){
			Debug.Log ("rotate");
		Matrix4x4 M = new Matrix4x4 (
				new Vector4 (Mathf.Cos(Time.time), (-1)*Mathf.Sin(Time.time), 0f, 0f),
				new Vector4 (Mathf.Sin(Time.time),Mathf.Cos(Time.time), 0f, 0f),
				new Vector4 (0f, 0f, Mathf.Cos(Time.time), (-1)*Mathf.Sin(Time.time)),
				new Vector4 (0f, 0f,Mathf.Sin(Time.time),Mathf.Cos(Time.time)));
		gameObject.GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		}
		if (OVRInput.GetDown (OVRInput.RawButton.B)) {
			Debug.Log ("reset");
			Matrix4x4 M = Matrix4x4.identity;
			gameObject.GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		}
	}
}
