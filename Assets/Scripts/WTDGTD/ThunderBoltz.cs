using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBoltz : MonoBehaviour {
    public float standardIntensity=0F;
    public float intens1 = 1.2F;
    public float intens2 = 0.6F;
    public float speedintens1 = 0.8F;
    public float speedintens2 = 0.3F;
    public List<Light> lights;
    public float waittime = 6;
    public DateTime lstdate;
    public bool candonow;
    public float ltIns;

    public float intens1_;
    public float intens2_;
    public float speedintens1_;
    public float speedintens2_;
  
    void Start () {
        foreach (Transform trn in transform) {
            lights.Add(trn.GetComponent<Light>());
        }
        lstdate = DateTime.Now.AddSeconds(4);
	}
	
	void FixedUpdate () {
        if (!candonow && DateTime.Now > lstdate)
        {
            intens1_ = intens1 + (-0.3F + (UnityEngine.Random.Range(0, 40) / 100));
            intens2_ = intens1_;
            bool moardan;
            moardan = false;
            int i;
            i = 0;
            while (intens2_ >= intens1_ && moardan)
            {
                intens2_ = intens2 + (-0.29F + (UnityEngine.Random.Range(0, 30) / 100));
                i += 1;
                if (i > 13)
                {
                    break;
                }
            }
            if (intens2_ >= intens1_) {
                intens2_ = intens1_ - (UnityEngine.Random.Range(1, 100) / 100);
            }
            speedintens1_ = speedintens1 + (-0.12F + (UnityEngine.Random.Range(0, 2) / 10));
            speedintens2_ = speedintens2;
            foreach (Light lgt in lights)
            {
                lgt.intensity = intens2_;
            }
            ltIns = intens2_;
            candonow = true;
        }
        if (candonow)
        {
       
               if (ltIns > intens2_)
            {
                ltIns = Mathf.MoveTowards(ltIns, intens2_, speedintens1_);
            }
            if (ltIns > standardIntensity && ltIns <= intens2_)
            {
                ltIns = Mathf.MoveTowards(ltIns, standardIntensity, speedintens2_);
            }
            if (ltIns <= 0)
            {
                ltIns = 0;
                candonow = false;
                lstdate = DateTime.Now.AddSeconds(waittime + (-1.6F + (UnityEngine.Random.Range(0, 20) / 10)));
            }
            foreach (Light lgt in lights)
            {
                lgt.intensity = ltIns;   
            }
        }
	}
}
