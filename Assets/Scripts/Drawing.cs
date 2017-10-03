using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour {

	public GameObject cube;

	private GameObject draw;
	private Vector3 IntPos;
	private Color c=new Color(0,1,1,1);
	private Material m;

	// Use this for initialization
	void Start () {
		IntPos = cube.transform.localPosition;
		m = cube.GetComponent<Renderer> ().sharedMaterial;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.Escape))
			UnityEditor.EditorApplication.isPlaying = false;
		
		if (Input.GetKeyDown (KeyCode.A)) {
			Instantiate (cube);
			cube.transform.localPosition=IntPos+new Vector3(1, 1, 1);
		}
		IntPos = cube.transform.localPosition;
		if (Input.GetKeyDown (KeyCode.D))
			DrawLine ();
		if (Input.GetKeyDown (KeyCode.F))
			EraseLine ();
	}
	void DrawLine()
	{
		draw = new GameObject ();
		draw.transform.position = new Vector3 (0, 0, 0);
		draw.AddComponent<LineRenderer> ();
		LineRenderer L = draw.GetComponent<LineRenderer> ();
		L.material = m;
		L.SetColors (c, c);
		L.SetWidth (0.5f, 0.5f);
		L.SetPosition (0, new Vector3(0,0,0));
		L.SetPosition (1, new Vector3 (.03f, .03f, .03f));
		//GameObject.Destroy (draw, 5f);
	}
	void EraseLine()
	{
		GameObject.Destroy (draw);
	}
}