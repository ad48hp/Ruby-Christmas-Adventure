using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]

public class Anim8Me : MonoBehaviour {
    public string[] anim;
    private string[] animprvs;
    public float[] animspýdz;
    public Animation animp;

	void Start () {
        animp = GetComponent<Animation>();
        if (!Application.isPlaying && anim==null) {
           List<string> anm1 = new List<string>();
            List<float> anm2 = new List<float>();
            foreach (AnimationState nm in animp)
            {
                anm1.Add(nm.clip.name);
                anm2.Add(1);
            }
            anim = anm1.ToArray();
            animspýdz = anm2.ToArray();
        }
    }
	
	void Update () {
        for (int i=0;i<anim.Length; i++) {
            animp[anim[i]].speed = animspýdz[i];
            if (!animp.IsPlaying(anim[i])) {
                animp.Play(anim[i]);
            }
        }
        if (anim != animprvs) {
            List<string> animlzt;
            animlzt = anim.ToList();
            foreach (string anm in animprvs) {
                if (!animlzt.Contains(anm)) {
                    animp.Stop(anm);
                }
        }
            animprvs = anim;
        }
	}
}
