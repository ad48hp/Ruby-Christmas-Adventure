using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Pole : MonoBehaviour {
    public GameObject obj;
    public float dist;
    public int instances;
    public bool turned;
	
	void Update () {
        if (turned && instances > 0)
        {
            turned = false;
            foreach (Transform tr in transform)
            {
                if (Application.isPlaying)
                {
                    Destroy(tr.gameObject);
                }
                else
                {
                    DestroyImmediate(tr.gameObject);
                }
                }
            int ang;
            ang = 0;
            for (int i = 0; i < instances; i++)
            {
                GameObject inst;
                inst = Instantiate(obj, transform.position + new Vector3(Mathf.Cos(ang)*dist, Mathf.Sin(ang)* dist, 0), obj.transform.rotation);
                inst.transform.parent = transform;
                inst.SetActive(true);
                /*if (i > 0)
                {*/
                    ang += (360 / instances);
                //}
            }
        }
	}
}
