using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour {

	public OVRInput.Controller Lcontroller,Rcontroller;
	public GameObject righthand,threeframe,fourframe;

	private Quaternion RotStart;
	private Vector3 PosStart,Rhand;

	void Start()
	{
		RotStart = threeframe.transform.localRotation;
		PosStart = threeframe.transform.localPosition;
	}
	// Update is called once per frame
	void Update ()
	{

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
		}

	}
}
