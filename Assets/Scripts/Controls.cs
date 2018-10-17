using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    //private OVRInput.Controller RightHand=OVRInput.Controller.RTouch,LeftHand=OVRInput.Controller.LTouch;
    int right = 1;
    int left = 0;
	private Renderer rend;
	private Vector3 eulerR,eulerL;
    private Vector3 startPosition;
    private bool XYZW,YZXW,XZYW;
	private Renderer Xrend,Yrend,Zrend;
	private Quaternion DownR, DownL,rotR,rotL;
	private GameObject X,Y,Z,Planes;
	private Matrix4x4 T=Matrix4x4.identity,A=Matrix4x4.identity,B=Matrix4x4.identity;

    public float MouseInputX=10000;
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
		XYZW = false;
		YZXW = true;
		XZYW = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit ();

		if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && XYZW == true) {
			XYZW = false;
			Zrend.material.color = Color.red;
			YZXW = true;
			Xrend.material.color = Color.green;

		} else if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && YZXW == true) {
			YZXW = false;
			Xrend.material.color = Color.red;;
			XZYW = true;
			Yrend.material.color = Color.green;

		} else if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && XZYW == true) {
			XZYW = false;
			Yrend.material.color = Color.red;
			XYZW = true;
			Zrend.material.color = Color.green;
		}

        /*
		rotR = OVRInput.GetLocalControllerRotation (RightHand);
		rotL = OVRInput.GetLocalControllerRotation (LeftHand);
        */

        rotR= Quaternion.AngleAxis(MouseInputX * 10 * Time.deltaTime, Vector3.up);
        rotL = Quaternion.AngleAxis(MouseInputX * 10 * Time.deltaTime, Vector3.up);

        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log(Input.mousePosition);
            Vector3 mouseDelta = Input.mousePosition - startPosition;

            if (mouseDelta.sqrMagnitude < 0.1f)
            {
                return; // don't do tiny rotations.
            }

            float angle = Mathf.Atan2(mouseDelta.y, mouseDelta.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360;


            eulerL = new Vector3(transform.localEulerAngles.x,
                                                      transform.localEulerAngles.y,
                                                      angle);
        }
        float rotation_angleRight = eulerR.z * (Mathf.PI / 180);
        float rotation_angleLeft = eulerL.z * (Mathf.PI / 180);

        if (Input.GetMouseButtonDown(1))
        {
            startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Debug.Log(Input.mousePosition);
            Vector3 mouseDelta = Input.mousePosition - startPosition;

            if (mouseDelta.sqrMagnitude < 0.1f)
            {
                return; // don't do tiny rotations.
            }

            float angle = Mathf.Atan2(mouseDelta.y, mouseDelta.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360;


            eulerR = new Vector3(transform.localEulerAngles.x,
                                                      transform.localEulerAngles.y,
                                                      angle);
        }


        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
			//DownR = OVRInput.GetLocalControllerRotation (RightHand);
			//DownL = OVRInput.GetLocalControllerRotation (LeftHand);
			T = rend.material.GetMatrix ("_fdtransform");
		}

		if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) {
			//DownR = OVRInput.GetLocalControllerRotation (RightHand);
			//DownL = OVRInput.GetLocalControllerRotation (LeftHand);
			T = rend.material.GetMatrix ("_fdtransform");
			A = Matrix4x4.identity;
			B = Matrix4x4.identity;
		}

		Quaternion R = Quaternion.Inverse(DownR)*rotR;
		Quaternion L = Quaternion.Inverse(DownL)*rotL;

		//eulerL= L.eulerAngles * (Mathf.PI / 180);
		//eulerR =R.eulerAngles * (Mathf.PI / 180);


        

        if (XYZW) {
			if (Input.GetMouseButton(right)) {
				A = new Matrix4x4 (
					new Vector4 (Mathf.Cos (rotation_angleRight), (-1) * Mathf.Sin (rotation_angleRight), 0f, 0f),
					new Vector4 (Mathf.Sin (rotation_angleRight), Mathf.Cos (rotation_angleRight), 0f, 0f),
					new Vector4 (0f, 0f, 1, 0),
					new Vector4 (0f, 0f, 0, 1));
			}
			if (Input.GetMouseButton(left) && XYZW == true) {
				B = new Matrix4x4 (
					new Vector4 (1, 0, 0f, 0f),
					new Vector4 (0, 1, 0f, 0f),
					new Vector4 (0f, 0f, Mathf.Cos (rotation_angleLeft), (-1) * Mathf.Sin (rotation_angleLeft)),
					new Vector4 (0f, 0f, Mathf.Sin (rotation_angleLeft), Mathf.Cos (rotation_angleLeft)));
			}
			rend.material.SetMatrix ("_fdtransform", A*B*T);
		}

		if (YZXW) {
			if (Input.GetMouseButton(right)) {
				A= new Matrix4x4 (
					new Vector4 (1, 0, 0, 0),
					new Vector4 (0, Mathf.Cos (rotation_angleRight), Mathf.Sin (rotation_angleRight), 0),
					new Vector4 (0, (-1) * Mathf.Sin (rotation_angleRight), Mathf.Cos (rotation_angleRight), 0),
					new Vector4 (0, 0, 0, 1));
			}
			if (Input.GetMouseButton(left)) {
				B = new Matrix4x4(
					new Vector4 (Mathf.Cos (rotation_angleLeft), 0, 0, Mathf.Sin (rotation_angleLeft)),
					new Vector4 (0, 1, 0, 0),
					new Vector4 (0, 0,1, 0),
					new Vector4 ((-1) * Mathf.Sin (rotation_angleLeft), 0, 0, Mathf.Cos (rotation_angleLeft)));
			}
			rend.material.SetMatrix ("_fdtransform", A*B*T);
		}

		if (XZYW) {
			if (Input.GetMouseButton(right)) {
				A = new Matrix4x4 (
					new Vector4 (Mathf.Cos (rotation_angleRight), 0, (-1) * Mathf.Sin (rotation_angleRight), 0),
					new Vector4 (0, 1, 0, 0),
					new Vector4 (Mathf.Sin (rotation_angleRight), 0, Mathf.Cos (rotation_angleRight), 0),
					new Vector4 (0, 0, 0, 1));
			}
			if (Input.GetMouseButton(left)) {
				B = new Matrix4x4(
					new Vector4 (1, 0, 0, 0),
					new Vector4 (0, Mathf.Cos (rotation_angleLeft), 0, (-1) * Mathf.Sin (rotation_angleLeft)),
					new Vector4 (0, 0,1, 0),
					new Vector4 (0, Mathf.Sin (rotation_angleLeft), 0, Mathf.Cos (rotation_angleLeft)));
					}
			rend.material.SetMatrix ("_fdtransform", A*B*T);
		}

		if (Input.GetMouseButtonDown(2)) {
			A = Matrix4x4.identity;
			B = Matrix4x4.identity;
			T = Matrix4x4.identity;
		}
	}
}
