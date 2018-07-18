using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnim : MonoBehaviour {
    public float rozmezi = 0.16F;
    public MLight[] lightz;
    public int myi = 0;
    public int maxi = 4;

	void Start () {
        List<MLight> ltzz = new List<MLight>();
        foreach (Light ltz2 in GetComponents<Light>()) {
            MLight ltz3 = new MLight();
            ltz3.mlt = ltz2;
            ltz3.origInt = ltz2.intensity;
            ltzz.Add(ltz3);
        }
        lightz = ltzz.ToArray();
	}

    void Update() {
        myi += 1;
        if (myi > maxi)
        {
            myi = 0;
            foreach (MLight lt in lightz)
            {
                lt.mlt.intensity = lt.origInt - (rozmezi / 2) + Random.Range(0, rozmezi);
            }
        }
	}
}
