using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureMoveOn : MonoBehaviour {
    public Vector2 speed;
    public Material mt;

	void Start () {
        mt = GetComponent<Renderer>().material;
	}
	
	void FixedUpdate () {
        mt.mainTextureOffset += speed * Time.deltaTime;
	}
}
