using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureMove : MonoBehaviour {
    public Vector2 tmovie = new Vector2(3.1F, 0);
    //public float rotateme = 0;
    public Material mat;

	void Start () {
        mat = GetComponent<MeshRenderer>().material; ;
	}
	
	void FixedUpdate () {
        if (mat != null)
        {
            mat.SetTextureOffset("_MainTex", mat.GetTextureOffset("_MainTex") + (tmovie * Time.deltaTime));
            //mat.SetMatrix("_Rotation", mat.GetFloat("_Rotation") + (rotateme * Time.deltaTime));
        }
        }
}
