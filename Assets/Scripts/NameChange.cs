using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameChange : MonoBehaviour {

	private GameObject FiveCell, EightCell, RP2, RP2cut, Torus;
	private Renderer A, B, C, D, E;
	private GameObject Runner;
	private MeshLoader M;
	private int P;
	// Use this for initialization
	void Start () {
		Runner = GameObject.Find ("Runner");
		FiveCell = GameObject.Find ("5-Cell");
		A = FiveCell.GetComponent<Renderer> ();
		EightCell = GameObject.Find ("8-Cell");
		B=EightCell.GetComponent<Renderer> ();
		RP2 = GameObject.Find ("RP2");
		C=RP2.GetComponent<Renderer> ();
		RP2cut = GameObject.Find ("RP2 Cut Open");
		D=RP2cut.GetComponent<Renderer> ();
		Torus = GameObject.Find ("Torus");
		E=Torus.GetComponent<Renderer> ();
		M = Runner.GetComponent<MeshLoader>();
	}
	
	// Update is called once per frame
	void Update () {
		P = M.P;
		if (P == 0)
			A.material.color = Color.yellow;
		else
			A.material.color = Color.white;
		if (P == 1)
			B.material.color = Color.yellow;
		else
			B.material.color = Color.white;
		if (P == 2)
			C.material.color = Color.yellow;
		else
			C.material.color = Color.white;
		if (P == 3)
			D.material.color = Color.yellow;
		else
			D.material.color = Color.white;
		if (P == 4)
			E.material.color = Color.yellow;
		else
			E.material.color = Color.white;
	}
}
