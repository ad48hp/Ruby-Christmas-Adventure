using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {
    public Vector3 origPos;
    //public Quaternion origRot;
    public GameObject toFollow;
    public Vector3 myrot;
    public Vector3 mypos;

	void Start () {
        origPos = transform.position-toFollow.transform.position;
        //origRot = transform.rotation * Quaternion.Inverse(ComputeAngles(toFollow));
	}
	
	void Update () {
        //Vector3 curOffset = toFollow.transform.rotation * origPos;
        transform.position = toFollow.transform.position + origPos;
        //transform.rotation = ComputeAngles(toFollow) * origRot;
        //this will rotate the offset position by the parent's rotation
        //transform.position = toFollow.transform.position + curOffset + mypos;
        //move this object        
        //transform.rotation = toFollow.transform.rotation * Quaternion.Euler(myrot);
    }

    public Quaternion ComputeAngles(GameObject toFollow)
    {
        Quaternion tempRot = toFollow.transform.rotation;
        List<Transform> trz = GetParents(toFollow.transform.parent.gameObject);
        foreach (Transform tr in trz) {
            tempRot *= tr.rotation;
        }
        return tempRot;
    }

   public List<Transform> GetParents(GameObject tfw)
    {
        List<Transform> trs = new List<Transform>();
        if (tfw != null)
        {
            trs.Add(tfw.transform);
            if (tfw.transform.parent != null) {
                   trs.AddRange(GetParents(tfw.transform.parent.gameObject));
             }
        }
        return trs;
    }
}
