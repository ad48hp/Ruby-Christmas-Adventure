using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMe : MonoBehaviour {
    public Vector3 post;
    public DateTime then;
    public float waittime;
    public bool canme = false;

	void Start () {
        then = DateTime.Now.AddSeconds(waittime);
	}
	
	void FixedUpdate () {
        if (!canme && DateTime.Now > then)
        {
            canme = true;
        }
        if (canme)
        {
            transform.position += post * Time.deltaTime;
        }
        }
}
