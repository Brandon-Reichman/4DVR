using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeshLoader: MonoBehaviour {

	public Material Stereographic,Orthographic;

	private Controls controls;
	private float[] nums=new float[4];
	private GameObject OBJ;
	private int P=1;
	private string[] locations=new string[5];
	List<int> ParseFaceLine(string faceline)
	{
		// Input "f 1//2 3//4 5//6"
		// Output {1,3,5}

		List<int> verts = new List<int> ();
		// Split the line on spaces to get fields, and drop the first one.
		foreach (string field in faceline.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries).Skip(1)) {
			// Each field consists of slash-separated ints.
			// The first one is the index of the vertex to use.
			// Extract that and ignore the rest.
			int idx = int.Parse(field.Split('/')[0]);
			verts.Add (idx);
		}
		//Debug.Log ("Parsed vertex line to: " + string.Join (", ", verts.Select (i => i.ToString ()).ToArray ()));
		return verts;
	}

	float[] ParseVertexLine(string vertexline)
	{
		List<float> coords = new List<float> ();
		vertexline = vertexline.PadRight (vertexline.Length + 1, ' ');
		vertexline = vertexline.PadRight (vertexline.Length + 1, '0');
		//Debug.Log ("parsing vertex line: "+vertexline);
		foreach(string field in vertexline.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries).Skip(1)){
			//Debug.Log (field);
			float ver = float.Parse (field);
			//Debug.Log (ver);
			coords.Add (ver);
		}
		return coords.ToArray ();
	}

	List<int[]> Triangulate(List<int> verts)
	{
		// Input { 1, 2, 3, 4, 5 }
		// Ouput { {1,2,3}, {1,3,4}, {1,4,5} }

		// TODO: Throw an exception if verts too short
		List<int[]> tris = new List<int[]> ();
		for (int i = 2; i < verts.Count; i++) {
			tris.Add (new int[] { verts [0], verts [i - 1], verts [i] });
			//Debug.Log (verts [0] + " " + verts [i - 1] + " " + verts [i]);
		}
		// TODO: change to a more readable way of making a string like 1,2,3,4 -> {1,2,3},{1,3,4}
	   /* Debug.Log(
			string.Join (", ", verts.Select (i => i.ToString ()).ToArray ())
			+ " -> "
			+ string.Join(", ", tris.Select (t => "{" + string.Join(", ",t.Select(i => i.ToString ()).ToArray()) + "}").ToArray())
		);*/
		return tris;
	}

	bool isVertLine(string s)
	{
			return s.Trim ().StartsWith ("v ");
	}

	bool isFaceLine(string s)
	{
		return s.Trim().StartsWith("f ");
	}

	int[] Flatten(List<int[]> t)
	{
		int[] tris=new int[(t.Count*3)];
		for (int o = 0; o < t.Count; o++) {
			for (int h = 0; h < 3; h++) {
				tris [3*o+h] = t [o] [h];
			}
		}
		for (int i = 0; i < tris.Length; i++)
			tris [i] = tris [i] - 1;
		return tris;
	}

	Vector3[] LoadVerts(List<float[]> V){
		Vector3[]ver=new Vector3[V.Count];
		for (int i = 0; i < V.Count; i++) {
				ver [i].x=V [i] [0];
				ver [i].y=V [i] [1];
				ver [i].z=V [i] [2];
			//Debug.Log (ver [i].x + " " + ver [i].y + " " + ver [i].z);
			}
		return ver;
	}

	Vector4[] LoadTans(List<float[]> V){
		Vector4[] tan = new Vector4[V.Count];
		for (int i = 0; i < V.Count; i++)
			tan [i].x = V [i] [3];
		return tan;
	}

	void DrawObject(Material m,Vector3[] v,Vector4[]t,int[]tri)
	{
		Mesh stuff = new Mesh ();
		OBJ = new GameObject ("Drawn Object");
		OBJ.transform.gameObject.AddComponent<MeshRenderer> ();
		OBJ.transform.gameObject.AddComponent<MeshFilter> ().sharedMesh = stuff;
		OBJ.AddComponent (Type.GetType ("Controls"));
		/*controls = OBJ.GetComponent<Controls> ();
		controls.enabled = false;
		controls.enabled = true;*/
		OBJ.GetComponent<MeshRenderer> ().material = m;
		OBJ.transform.localPosition = new Vector3 (0, -0.25f, 2);
		stuff.vertices = v;
		stuff.triangles = tri;
		stuff.tangents = t;
		stuff.RecalculateNormals ();
	}

	// Use this for initialization
	void Start () 
	{
		string[] lines;

		locations [0] = "Assets/Meshes/5-cell.obj";
		locations [1] = "Assets/Meshes/8-cell.obj";
		locations [2] = "Assets/Meshes/rp2-40x140.obj";
		locations [3] = "Assets/Meshes/rp2-cut-open-40x140.obj";
		locations [4] = "Assets/Meshes/torus-4d-50x50.obj";

		lines = System.IO.File.ReadAllLines (locations[1]);

		List<int[]> triangles = new List<int[]> ();
		List<float[]> vertices = new List<float[]> ();
		foreach (string s in lines) {
			if (isFaceLine(s)) {
				List<int> faceverts = ParseFaceLine(s);
				List<int[]> T=Triangulate(faceverts);
				triangles.AddRange (T);
			}
			if (isVertLine (s)) {
				float[] V=ParseVertexLine (s);
				vertices.Add(V);
			}
		}
		DrawObject (Orthographic, LoadVerts (vertices), LoadTans (vertices), Flatten (triangles));
			}
			
	// Update is called once per frame
	void Update () 
	{
		if (OVRInput.GetDown (OVRInput.RawButton.X))
			OBJ.GetComponent<MeshRenderer> ().material = Orthographic;
		if (OVRInput.GetDown (OVRInput.RawButton.Y))
			OBJ.GetComponent<MeshRenderer> ().material = Stereographic;
		
		if (OVRInput.GetDown (OVRInput.RawButton.RIndexTrigger)) {
			Destroy (OBJ);
			P++;
			if (P > 4)
				P = 0;
			string[] lines = System.IO.File.ReadAllLines (locations[P]);

			List<int[]> triangles = new List<int[]> ();
			List<float[]> vertices = new List<float[]> ();
			foreach (string s in lines) {
				if (isFaceLine(s)) {
					List<int> faceverts = ParseFaceLine(s);
					List<int[]> T=Triangulate(faceverts);
					triangles.AddRange (T);
				}
				if (isVertLine (s)) {
					float[] V=ParseVertexLine (s);
					vertices.Add(V);
				}
			}
			DrawObject (Orthographic, LoadVerts (vertices), LoadTans (vertices), Flatten (triangles));
		}
		if (OVRInput.GetDown (OVRInput.RawButton.LIndexTrigger)) {
			Destroy (OBJ);
			P--;
			if (P < 0)
				P = 4;
			string[] lines = System.IO.File.ReadAllLines (locations[P]);

			List<int[]> triangles = new List<int[]> ();
			List<float[]> vertices = new List<float[]> ();
			foreach (string s in lines) {
				if (isFaceLine(s)) {
					List<int> faceverts = ParseFaceLine(s);
					List<int[]> T=Triangulate(faceverts);
					triangles.AddRange (T);
				}
				if (isVertLine (s)) {
					float[] V=ParseVertexLine (s);
					vertices.Add(V);
				}
			}
			DrawObject (Orthographic, LoadVerts (vertices), LoadTans (vertices), Flatten (triangles));
		}
		if (OVRInput.GetDown (OVRInput.Button.Start))
			Application.Quit ();
	}

}

