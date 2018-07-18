using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChallengingObjRemover : MonoBehaviour {
    public float distance;
    public string[] keepobjs;
    public bool rendercenter;

	void Start () {
        List<string> kobjs;
        kobjs = keepobjs.ToList();
        GameObject[] objs;
        objs = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject gms in objs)
        {
            if (((!rendercenter && Vector3.Distance(transform.position, gms.transform.position) <= distance) || (rendercenter && gms.GetComponent<MeshRenderer>()!=null && Vector3.Distance(transform.position, gms.GetComponent<MeshRenderer>().bounds.center) <= distance)) && !kobjs.Contains(gms.gameObject.name) && gms.GetInstanceID()!=gameObject.GetInstanceID())
            {
                Destroy(gms);
            }
        }
        Destroy(gameObject);
    }
}
