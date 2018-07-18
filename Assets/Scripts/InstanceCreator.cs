using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceCreator : MonoBehaviour {
    public float minDistance = 2;
    public float maxDistance = 15;
    public Vector3 relativePos = Vector3.zero;
    public GameObject instante;
    public Vector3 objrot = Vector3.zero;
    public float fadeinspeed = 0.03F;
    public float fadeoutspeed = 0.03F;
    public float minfade = 0.06F;
    public float maxfade = 0.96F;
    public GameObject curobj;
    public float sizeMultiplier = 1;

    void Start()
    {

    }


    void FixedUpdate() {
        float mymin = Mathf.Infinity;
        foreach (Transform trans in transform)
        {
            if (Vector3.Distance(transform.position, trans.position) > maxDistance)
            {
                Color clr = trans.gameObject.GetComponent<Renderer>().material.color;
                if (clr.a > minfade)
                {
                    clr.a += -fadeoutspeed;
                    trans.gameObject.GetComponent<Renderer>().material.SetColor("_Color", clr);
                }
                else
                {
                    Destroy(trans.gameObject);
                }

            }
            mymin = Mathf.Min(mymin, Vector3.Distance(transform.position, trans.position));
        }
        if (mymin > minDistance) {
            GameObject myobj;
            myobj = Instantiate(instante, transform.position + relativePos, Quaternion.Euler(objrot));
            myobj.transform.localScale *= sizeMultiplier;
            myobj.GetComponent<MoveMe>().enabled = false;
            Color clr = myobj.gameObject.GetComponent<Renderer>().material.color;
            clr.a = 0;
            myobj.gameObject.GetComponent<Renderer>().material.SetColor("_Color", clr);
                myobj.name = instante.name;
            myobj.transform.parent = transform;
            curobj = myobj;
        }
        if (curobj != null) {
        Color clr = curobj.gameObject.GetComponent<Renderer>().material.color;
            if (clr.a < maxfade)
            {
                clr.a += fadeinspeed;
                curobj.gameObject.GetComponent<Renderer>().material.SetColor("_Color", clr);
            }
            else
            {
                curobj.GetComponent<MoveMe>().enabled = true;
                curobj = null;
            }
    }
    }
}
