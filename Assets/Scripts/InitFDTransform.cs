using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitFDTransform : MonoBehaviour {
	// Use this for initialization
	private Matrix4x4 M=Matrix4x4.identity;
	private float sp=1;
	private Renderer rend;


	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape))
			UnityEditor.EditorApplication.isPlaying = false;
		
		if (OVRInput.Get (OVRInput.RawButton.A) && OVRInput.Get (OVRInput.RawButton.RIndexTrigger)||OVRInput.Get (OVRInput.RawButton.A) && OVRInput.Get (OVRInput.RawButton.LIndexTrigger)) {
			M = new Matrix4x4 (
				new Vector4 (Mathf.Cos (Time.time), (-1) * Mathf.Sin (Time.time), 0f, 0f),
				new Vector4 (Mathf.Sin (Time.time), Mathf.Cos (Time.time), 0f, 0f),
				new Vector4 (0f, 0f, Mathf.Cos (Time.time), (-1) * Mathf.Sin (Time.time)),
				new Vector4 (0f, 0f, Mathf.Sin (Time.time), Mathf.Cos (Time.time)));
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		} 
		else if ((OVRInput.Get (OVRInput.RawButton.A)) && (OVRInput.Get (OVRInput.RawButton.RHandTrigger))|| OVRInput.Get (OVRInput.RawButton.A) && OVRInput.Get (OVRInput.RawButton.LHandTrigger)) {
			M = new Matrix4x4 (
				new Vector4 (1, 0, 0f, 0f),
				new Vector4 (0, 1, 0f, 0f),
				new Vector4 (0f, 0f, Mathf.Cos (Time.time), (-1) * Mathf.Sin (Time.time)),
				new Vector4 (0f, 0f, Mathf.Sin (Time.time), Mathf.Cos (Time.time)));
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		}
		else if (OVRInput.Get(OVRInput.RawButton.A)){
			M = new Matrix4x4 (
				new Vector4 (Mathf.Cos(Time.time), (-1)*Mathf.Sin(Time.time), 0f, 0f),
				new Vector4 (Mathf.Sin(Time.time),Mathf.Cos(Time.time), 0f, 0f),
				new Vector4 (0f, 0f, 1, 0),
				new Vector4 (0f, 0f,0,1));
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		}
			
		if (OVRInput.GetDown (OVRInput.RawButton.Start)) {
			Debug.Log ("reset");
			Matrix4x4 M = Matrix4x4.identity;
			gameObject.GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		}

		if (OVRInput.Get (OVRInput.RawButton.Y) && OVRInput.Get (OVRInput.RawButton.RIndexTrigger)||OVRInput.Get (OVRInput.RawButton.Y) && OVRInput.Get (OVRInput.RawButton.LIndexTrigger)) {
			M = new Matrix4x4 (
				new Vector4 (Mathf.Cos (Time.time), 0, (-1) * Mathf.Sin (Time.time), 0),
				new Vector4 (0, Mathf.Cos (Time.time), 0, (-1) * Mathf.Sin (Time.time)),
				new Vector4 (Mathf.Sin (Time.time), 0, Mathf.Cos (Time.time), 0),
				new Vector4 (0, Mathf.Sin (Time.time), 0, Mathf.Cos (Time.time))
			);
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		}  else if ((OVRInput.Get (OVRInput.RawButton.Y)) && (OVRInput.Get (OVRInput.RawButton.RHandTrigger)) || OVRInput.Get (OVRInput.RawButton.Y) && OVRInput.Get (OVRInput.RawButton.LHandTrigger)) {
			M = new Matrix4x4 (
				new Vector4 (1, 0, 0, 0),
				new Vector4 (0, Mathf.Cos (Time.time), 0, (-1) * Mathf.Sin (Time.time)),
				new Vector4 (0, 0, 1, 0),
				new Vector4 (0, Mathf.Sin (Time.time), 0, Mathf.Cos (Time.time))
			);
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		} else if (OVRInput.Get (OVRInput.RawButton.Y)) {
			M = new Matrix4x4 (
				new Vector4 (Mathf.Cos (Time.time), 0, (-1) * Mathf.Sin (Time.time), 0),
				new Vector4 (0, 1, 0, 0),
				new Vector4 (Mathf.Sin (Time.time), 0, Mathf.Cos (Time.time), 0),
				new Vector4 (0, 0, 0, 1)
			);
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		}


		if (OVRInput.Get (OVRInput.RawButton.X) && OVRInput.Get (OVRInput.RawButton.RHandTrigger) || OVRInput.Get (OVRInput.RawButton.X) && OVRInput.Get (OVRInput.RawButton.LHandTrigger)) {
			M = new Matrix4x4 (
				new Vector4 (Mathf.Cos (Time.time), 0, 0, Mathf.Sin (Time.time)),
				new Vector4 (0, 1,0,0),
				new Vector4 (0, 0,1,0),
				new Vector4 ((-1) * Mathf.Sin (Time.time), 0, 0, Mathf.Cos (Time.time))
			);
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		} else if ((OVRInput.Get (OVRInput.RawButton.X)) && (OVRInput.Get (OVRInput.RawButton.RIndexTrigger)) || OVRInput.Get (OVRInput.RawButton.X) && OVRInput.Get (OVRInput.RawButton.LIndexTrigger)) {
			M= new Matrix4x4 (
				new Vector4 (Mathf.Cos (Time.time), 0, 0, Mathf.Sin (Time.time)),
				new Vector4 (0, Mathf.Cos (Time.time), Mathf.Sin (Time.time), 0),
				new Vector4 (0, (-1) * Mathf.Sin (Time.time), Mathf.Cos (Time.time), 0),
				new Vector4 ((-1) * Mathf.Sin (Time.time), 0, 0, Mathf.Cos (Time.time))
			);
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		}else if (OVRInput.Get (OVRInput.RawButton.X)) {
			M=new Matrix4x4(
				new Vector4 (1,0,0,0),
				new Vector4(0,Mathf.Cos(Time.time),Mathf.Sin(Time.time),0),
				new Vector4(0,(-1)*Mathf.Sin(Time.time),Mathf.Cos(Time.time),0),
				new Vector4(0,0,0,1)
			);
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		}
		if(OVRInput.Get(OVRInput.RawButton.B)){
			M = new Matrix4x4 (
				/*new Vector4(Mathf.Cos(Time.time)*Mathf.Cos(Time.time),(-1)*Mathf.Sin(Time.time),(-1)*Mathf.Sin(Time.time),0),
				new Vector4(Mathf.Sin(Time.time),Mathf.Cos(Time.time)*Mathf.Cos(Time.time),Mathf.Sin(Time.time),0),
				new Vector4(Mathf.Sin(Time.time),(-1)*Mathf.Sin(Time.time),Mathf.Cos(Time.time)*Mathf.Cos(Time.time),0),
				new Vector4(0,0,0,1) */
				new Vector4 (Mathf.Cos(Time.time)*Mathf.Cos(Time.time), Mathf.Cos(Time.time)*Mathf.Sin(Time.time)+Mathf.Sin(Time.time)*Mathf.Sin(Time.time)*Mathf.Cos(Time.time), Mathf.Sin(Time.time)*Mathf.Sin(Time.time)-Mathf.Cos(Time.time)*Mathf.Cos(Time.time)*Mathf.Sin(Time.time), 0),
				new Vector4 ((-1)*Mathf.Cos(Time.time)*Mathf.Sin(Time.time), Mathf.Cos(Time.time)*Mathf.Cos(Time.time)-Mathf.Sin(Time.time)*Mathf.Sin(Time.time)*Mathf.Sin(Time.time), Mathf.Cos(Time.time)*Mathf.Sin(Time.time)+Mathf.Sin(Time.time)*Mathf.Sin(Time.time)*Mathf.Cos(Time.time), 0),
				new Vector4 (Mathf.Sin(Time.time), (-1)*Mathf.Cos(Time.time)*Mathf.Sin(Time.time),Mathf.Cos(Time.time)*Mathf.Cos(Time.time), 0),
				new Vector4 (0, 0, 0, 1)
			);
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
			}

		/*if (OVRInput.Get (OVRInput.RawButton.RHandTrigger)&&OVRInput.Get (OVRInput.RawButton.LHandTrigger)) {
			M=new Matrix4x4(
				new Vector4(Mathf.Cos(Time.time),0,0,Mathf.Sin(Time.time)),
				new Vector4(0,Mathf.Cos(Time.time),0,(-1)*Mathf.Sin(Time.time)),
				new Vector4(0,0,Mathf.Cos(Time.time),(-1)*Mathf.Sin(Time.time)),
				new Vector4((-1)*Mathf.Sin(Time.time),Mathf.Sin(Time.time),Mathf.Sin(Time.time),Mathf.Cos(Time.time)*Mathf.Cos(Time.time)*Mathf.Cos(Time.time))
				);
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		}
		if (OVRInput.Get (OVRInput.RawButton.RIndexTrigger) && OVRInput.Get (OVRInput.RawButton.LIndexTrigger)) {
			M=new Matrix4x4(
				new Vector4(Mathf.Cos(Time.time),0,0,Mathf.Sin(Time.time)),
				new Vector4(0,Mathf.Cos(Time.time),0,(-1)*Mathf.Sin(Time.time)),
				new Vector4(0,0,1,0),
				new Vector4((-1)*Mathf.Sin(Time.time),Mathf.Sin(Time.time),0,Mathf.Cos(Time.time)*Mathf.Cos(Time.time))
			);
			GetComponent<Renderer> ().material.SetMatrix ("_fdtransform", M);
		}*/
	}
}   