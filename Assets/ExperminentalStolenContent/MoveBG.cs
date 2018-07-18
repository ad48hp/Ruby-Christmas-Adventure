using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBG : MonoBehaviour {
    public Vector3 movespeed;
    public bool[] axises = { false, false, false};
    //public Vector3 origvec;
    public Vector3 kraj;

    void Start () {
        //origvec = transform.localPosition;
        //txtr = GetComponent<SpriteRenderer>().sprite.texture;
	}
	
	void FixedUpdate () {
        //Debug.Log(GetComponent<SpriteRenderer>().sprite.textureRect.width);
        float[] mego;
        mego = new float[] { 0, 0, 0 };
        if (axises[0])
        {
            mego[0] = 1;
        }
        if (axises[1])
        {
            mego[1] = 1;
        }
        if (axises[2])
        {
            mego[2] = 1;
        }
        transform.localPosition += new Vector3(movespeed.x * mego[0], movespeed.y * mego[1], movespeed.z*mego[2]);
        if (axises[0] && transform.localPosition.x > kraj.x)
        {
            transform.localPosition += new Vector3(-kraj.x*2, 0, 0);
        }
        if (axises[0] && transform.localPosition.x < -kraj.x)
        {
            transform.localPosition += new Vector3(kraj.x * 2, 0, 0);
        }
        if (axises[1] && transform.localPosition.y > kraj.y)
        {
            transform.localPosition += new Vector3(0, -kraj.y * 2, 0);
        }
        if (axises[1] && transform.localPosition.y < -kraj.y)
        {
            transform.localPosition += new Vector3(0, kraj.y * 2, 0);
        }
        if (axises[2] && transform.localPosition.y > kraj.z)
        {
            transform.localPosition += new Vector3(0, 0, -kraj.z * 2);
        }
        if (axises[2] && transform.localPosition.y < -kraj.z)
        {
            transform.localPosition += new Vector3(0, 0, kraj.z * 2);
        }
 }
}
