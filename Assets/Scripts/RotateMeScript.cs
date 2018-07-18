using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMeScript : MonoBehaviour {
    public Vector3 rot;

    void Start () {
		
	}
	
	void FixedUpdate () {
        transform.Rotate(rot);
	}
}
