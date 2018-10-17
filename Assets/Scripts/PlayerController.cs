using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private float x,y,z;
	// Use this for initialization
	void Start () {
        float y;
	}

    // Update is called once per frame
    void Update()
    {

        x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            y = Time.deltaTime * 3.0f;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            y = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            y = Time.deltaTime * -3.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            y = 0;
        }

        transform.Rotate(0, x, 0);
        transform.Translate(0, y, z);
    }
}
