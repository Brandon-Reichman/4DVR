using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour {

	public OVRInput.Controller Lcontroller,Rcontroller;
	public GameObject righthand,threeframe,fourframe;
	public float RotateSpeed=100f;



	private Material cube;
	private Renderer ren;
	private Quaternion RotStart;
	private Vector3 PosStart,Rhand;

	void Start()
	{
		cube = threeframe.GetComponent<Renderer> ().sharedMaterial;
		RotStart = threeframe.transform.localRotation;
		PosStart = threeframe.transform.localPosition;
		fourframe.transform.localPosition = PosStart;
	}
	// Update is called once per frame
	void Update ()
	{
		Vector4 eat = new Vector4 (Mathf.Cos(RotateSpeed*Time.deltaTime),(-1)*Mathf.Sin(RotateSpeed*Time.deltaTime), 0, 0);
		Vector4 one = new Vector4 (Mathf.Sin(RotateSpeed*Time.deltaTime),Mathf.Cos(RotateSpeed*Time.deltaTime),0,0);
		Vector4 two = new Vector4 (0,0,Mathf.Cos(RotateSpeed*Time.deltaTime),(-1)*Mathf.Sin(RotateSpeed*Time.deltaTime));
		Vector4 bii = new Vector4 (0, 0, Mathf.Sin(RotateSpeed*Time.deltaTime),Mathf.Cos(RotateSpeed*Time.deltaTime));
		if (OVRInput.Get (OVRInput.RawButton.LHandTrigger)) 
		{
			threeframe.transform.localRotation = OVRInput.GetLocalControllerRotation (Lcontroller);
			fourframe.transform.localRotation = OVRInput.GetLocalControllerRotation (Lcontroller);
			//this.transform.localPosition = (OVRInput.GetLocalControllerPosition (Lcontroller))+(PosStart);
		}

		if (OVRInput.Get (OVRInput.RawButton.RHandTrigger))
		{

		}

		if (OVRInput.Get(OVRInput.RawButton.Y)) {
			fourframe.SetActive (false);
			threeframe.SetActive(true);
		}
		if (OVRInput.Get(OVRInput.RawButton.X)) {
			fourframe.SetActive(true);
			threeframe.SetActive(false);
		}


		//reset position and rotation
		if (OVRInput.Get (OVRInput.RawButton.A)) {
			threeframe.transform.localRotation = RotStart;
			threeframe.transform.localPosition = PosStart;

			fourframe.transform.localRotation = RotStart;
			fourframe.transform.localPosition = PosStart;
			Matrix4x4 ident = Matrix4x4.identity;
			cube.SetMatrix ("_objectpose", ident);
		}

		/*if (OVRInput.Get (OVRInput.RawButton.B)) {
			/*threeframe.transform.Rotate (Vector3.up, RotateSpeed * Time.deltaTime);
			fourframe.transform.Rotate(Vector3.up, RotateSpeed * Time.deltaTime);*/

			//test
			/*var cubepose=cube.GetMatrix("_objectpose");
			cubepose.SetRow (0, cubepose.GetRow (0) + eat);
			cubepose.SetRow (1, cubepose.GetRow (1) + one);
			//cubepose.SetRow (2, cubepose.GetRow (2) + two);
			cubepose.SetRow (3, cubepose.GetRow (3) + bii);
			//cubepose.SetColumn (1, (/*cubepose.GetColumn (1) + eat));/*
			cube.SetMatrix ("_objectpose", cubepose);
		}*/


	}
}
