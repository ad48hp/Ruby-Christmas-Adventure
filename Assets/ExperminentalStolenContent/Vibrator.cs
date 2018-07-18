using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrator : MonoBehaviour {
    public Vector3 origpos;
    public Vector3 origrot;
    public Vector3 shakepos;
    public Vector3 shakerot;
    public float amplitudepos;
    public float amplituderot;
    public Vector3 curpos;
    public Vector3 currot;
    public float tpos = 1;
    public float trot = 1;
    public Vector3 prevcurpos;
    public Vector3 prevcurrot;

    void Start() {
        origpos = transform.localPosition;
        origrot = transform.localEulerAngles;
    }
	
	void Update () {
        if (/*Vector3.Distance(transform.localPosition, curpos) < maxdist*/tpos >= 1)
        {
            tpos = 0;
            prevcurpos = curpos;
            curpos = new Vector3(origpos.x + Random.Range(-shakepos.x / 2, shakepos.x / 2), origpos.y + Random.Range(-shakerot.y / 2, shakepos.y / 2), origpos.z + Random.Range(-shakepos.z / 2, shakepos.z / 2));
        }
        if (trot>=1)
        {
            trot = 0;
            prevcurrot = currot;
            currot = new Vector3(origrot.x + Random.Range(-shakerot.x / 2, shakerot.x / 2), origrot.y + Random.Range(-shakerot.y / 2, shakerot.y / 2), origrot.z + Random.Range(-shakerot.z / 2, shakerot.z / 2));
        }
        transform.localPosition = Vector3SmoothStep(prevcurpos, curpos, ref tpos, amplitudepos);
        transform.localEulerAngles = Vector3SmoothStep(prevcurrot, currot, ref trot, amplituderot);
    }

    public static Vector3 Vector3SmoothStep(Vector3 vc1, Vector3 vc2, ref float tp, float amp)
    {
        Vector3 ret = new Vector3(Mathf.SmoothStep(vc1.x, vc2.x, tp), Mathf.SmoothStep(vc1.y, vc2.y, tp), Mathf.SmoothStep(vc1.z, vc2.z, tp));
        tp += amp;
        return ret;
    }
}
