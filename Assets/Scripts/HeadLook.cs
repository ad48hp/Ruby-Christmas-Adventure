using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLook : MonoBehaviour {
    public float LookSpeed = 3;
    public GameObject target;
    public Vector3 mup;
	
    void Start () {
		
	}

    void FixedUpdate()
    {
        if (target != null)
        {
            Quaternion q = Quaternion.LookRotation(target.transform.position - transform.position) * Quaternion.Euler(mup);
            if (LookSpeed > 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, q, LookSpeed * Time.deltaTime);
            }
            else
            {
                transform.rotation = q;
            }
        }
    }
}
