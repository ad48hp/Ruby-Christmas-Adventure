using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbAnimationControl : MonoBehaviour {
    public Animation anim;
    public string[] info;

	void Start () {
        anim = GetComponent<Animation>();
	}
	
	void FixedUpdate () {
        foreach (string item in info) {
            string[] items = item.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            anim[items[0].Replace(";", "")].speed = float.Parse(items[1].Replace(";", ""));
        }
	}
}
