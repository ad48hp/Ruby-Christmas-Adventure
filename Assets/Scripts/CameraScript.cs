using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Renderer myrend;
    //public float defaultdist = 69.8F;
    public GameObject player;
    public float curspeed = 0;
    public float speedtime;
    public float speedmultiplier;
    public Vector3 mr;
    public float mdistance = 8;
    public float origmdistance;
    public float mdistancespeed = 3;
    public float minDist = 0;
    public float toRotSpeed = 1;
    public Vector3 previousRot;
    public Vector3 curRot;
    public float mydist = 0.2F;
    public static List<string> desetags =  ("solid").Split(new string[] { "," },StringSplitOptions.RemoveEmptyEntries).ToList();
    public float fallspeed = 0;
    public float maxfallspeed = 10;
    public float speedfalltime = 2;
    public float maxDist2 = 0.2F;
    public float mystepdist = 0.1F;
    public Renderer rubyrend;
    public float distšit;
    public float lerpspýd = 0.05F;
    public bool bewareofclosesurface = false;
    public float zoomcount = 4;
    public bool allowverticalscroll = true;
    public Vector3 addtoppos = Vector3.zero;
    public bool experimentalbewareofclosesurface;

    void Start () {
        //rubyrend = player.GetComponent<Renderer>();
        origmdistance = mdistance;
	}

    void FixedUpdate() {
        if (player != null)
        {
            float scrollwh = Input.GetAxis("Mouse ScrollWheel");
            if (Application.isFocused)
            {
                if (scrollwh > 0)
                {
                    mdistance += -zoomcount * Time.deltaTime;
                }
                if (scrollwh < 0)
                {
                    mdistance += zoomcount * Time.deltaTime;
                }
            }
            if (experimentalbewareofclosesurface)
            {
                int iw;
                iw = 1;
                bool haveused;
                haveused = false;
                List<RaycastHit> hits2;
                hits2 = new List<RaycastHit>();
                List<RaycastHit> toremove;
                toremove = new List<RaycastHit>();
                float maxdist;
                maxdist = float.MaxValue;
                float playerdist;
                playerdist = 0;
                RaycastHit[] myšit;
                myšit = Physics.RaycastAll(((transform.position + (addtoppos * player.GetComponent<ConkerNotRubyScript>().walkspeed) - player.transform.position).normalized * mdistance + player.transform.position), transform.forward, maxDist2);
                /*while (iw<500 && myšit.Length>0 && myšit[0].collider.gameObject.tag!="Player")
                {
                    myšit = Physics.RaycastAll(((transform.position + (addtoppos * player.GetComponent<ConkerNotRubyScript>().walkspeed) - player.transform.position).normalized * mdistance + player.transform.position), transform.forward, maxDist2);
                    Debug.Log(myšit[0].collider.gameObject.name);
                    mdistance += -lerpspýd;
                    iw += 1;
                }*/
                while (!haveused || (hits2.Count>0 && (toremove.Count==0 || playerdist > maxdist)))
                {
                    haveused = true;
                    hits2 = Physics.RaycastAll(((transform.position + (addtoppos * player.GetComponent<ConkerNotRubyScript>().walkspeed) - player.transform.position).normalized * mdistance + player.transform.position), transform.forward, maxDist2).ToList();
                    toremove = new List<RaycastHit>();
                    foreach (RaycastHit mhit in hits2)
                    {
                        if (mhit.collider.gameObject.tag == "Player")
                        {
                            toremove.Add(mhit);
                            playerdist = mhit.distance;
                        }
                        else
                        {
                            if (mhit.distance < maxdist)
                            {
                                maxdist = mhit.distance;
                            }
                        }
                    }
                    RemoveFromList(hits2, toremove);
                    if (hits2.Count > 0)
                    {
                        mdistance += -lerpspýd;
                    }
                    Debug.Log(hits2.Count);
                    if (iw > 500)
                    {
                        break;
                    }
                    iw += 1;
                }
            }
            if (bewareofclosesurface)
            {
                distšit = 0;
                RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, 50);
                foreach (RaycastHit mhit in hits)
                {
                    if (mhit.collider.gameObject.tag != "Player" && mhit.distance < Vector3.Distance(transform.position, player.transform.position) && mhit.distance > distšit)
                    {
                        //Debug.Log(mhit.collider.gameObject.name);
                        distšit = mhit.distance;
                    }
                }
                if (distšit > 0)
                {
                    mdistance = 0;
                    //mdistance=Mathf.Lerp(mdistance)
                }
                List<RaycastHit> hits2 = new List<RaycastHit>();
                List<RaycastHit> toremove;
                bool didused;
                didused = false;
                int iw;
                iw = 1;
                while (mdistance < origmdistance && (!didused || hits2.Count == 0))
                {
                    mdistance += lerpspýd;
                    didused = true;
                    hits2 = Physics.RaycastAll(transform.position, -transform.forward, maxDist2).ToList();
                    toremove = new List<RaycastHit>();
                    foreach (RaycastHit mhit in hits2)
                    {
                        if (mhit.collider.gameObject.tag == "Player")
                        {
                            toremove.Add(mhit);
                        }
                    }
                    RemoveFromList(hits2, toremove);
                    if (iw > 500)
                    {
                        break;
                    }
                    iw += 1;
                }
                //Debug.Log(hits2.Count.ToString());
            }
            if (!IsFreeCircle(transform.position, maxDist2, new string[] { "solid", "water" }.ToList()) /*&& !rubyrend.isVisible*/)
            {
                //Debug.Log("ME");
                //transform.RotateAround(player.transform.position, -Vector3.right, mystepdist);
                //mdistance += -mystepdist;
            }
            //if (IsFree(transform.position,mr, maxDist2, new string[] { "Player" }.ToList(),true)) {
            transform.RotateAround(player.transform.position, mr, curspeed * speedmultiplier);
            //}
            /*if (IsFree(-transform.up)) {
                fallspeed = Mathf.MoveTowards(fallspeed, maxfallspeed, speedfalltime*Time.deltaTime);
                transform.position += -transform.up * fallspeed;
            }
            else {
                fallspeed = 0;
            }*/
            //transform.position = (player.transform.position - transform.position) * mdistance;
            if (mdistance < minDist)
            {
                mdistance = minDist;
            }
            //curRot = Vector3.Lerp(curRot, mr, toRotSpeed);
            if (mr != previousRot)
            {
                //previousRot = mr;
            }
            //transform.Translate(mr * Time.deltaTime * curspeed * speedmultiplier);
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            {
                curspeed = Mathf.Lerp(curspeed, 1, speedtime * Time.deltaTime);
            }
            else
            {
                curspeed = Mathf.Lerp(curspeed, 0, speedtime * Time.deltaTime);
            }
            Vector3 playerpos;
            playerpos = player.transform.position; // + addtoppos;
            transform.position = Vector3.Slerp(transform.position, ((transform.position + (addtoppos*player.GetComponent<ConkerNotRubyScript>().walkspeed) - playerpos).normalized * mdistance + playerpos), Time.deltaTime * mdistancespeed);
            transform.LookAt(player.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.W) && allowverticalscroll)
        {
            mr = -Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            mr = Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.S) && allowverticalscroll)
        {
            mr = Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            mr = -Vector3.up;
        }
        else
        {
            GameObject[] testps;
            testps = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject myobj in testps)
            {
                /*if (myobj.GetComponent<RubyScript>()!=null && myobj.GetComponent<RubyScript>().enabled)
                {
                    player = myobj;
                }*/
            }
            }
        }

    public static float Vector3Prumer(Vector3 vc)
    {
        return (vc.x + vc.y + vc.z) / 3;
    }

    public static bool Vector3Equals(Vector3 vc1, Vector3 vc2)
    {
        return (vc1.x == vc2.x && vc1.y == vc2.y && vc1.z == vc2.z);
    }

    private void RemoveFromList(List<RaycastHit> arr, List<RaycastHit> toremove)
    {
        foreach (RaycastHit itemarr in toremove)
        {
            arr.Remove(itemarr);
        }
    }

    public static bool IsFree(Vector3 tp, Vector3 myvec, float mydist, List<string> distags, bool inversetags)
    {
        RaycastHit[] hits = Physics.RaycastAll(tp, myvec, mydist);
        foreach (RaycastHit mhit in hits)
        {
            if ((!inversetags && distags.Contains(mhit.collider.gameObject.tag)) || (inversetags && !distags.Contains(mhit.collider.gameObject.tag)))
            {
                return false;
            }
        }
        return true;
    }

    public static bool IsFree(Vector3 tp, Vector3 myvec, float mydist, List<string> distags)
    {
        return IsFree(tp, myvec, mydist, distags, false);
    }

    public static List<Collider> GetObjs(Vector3 tp, Vector3 myvec, float mydist, List<string> distags)
    {
        List<Collider> cols = new List<Collider>();
        RaycastHit[] hits = Physics.RaycastAll(tp, myvec, mydist);
        foreach (RaycastHit mhit in hits)
        {
            if (distags.Contains(mhit.collider.gameObject.tag))
            {
                cols.Add(mhit.collider);
            }
        }
        return cols;
    }

    public static List<Collider> GetObjs(Vector3 tp, float mydist, List<string> distags)
    {
        List<Collider> cols = new List<Collider>();
        Collider[] hits = Physics.OverlapSphere(tp, mydist);
        foreach (Collider mhit in hits)
        {
            if (distags.Contains(mhit.gameObject.tag))
            {
                cols.Add(mhit);
            }
        }
        return cols;
    }

    public static bool IsFreeCircle(Vector3 tp,float mydist, List<string> distags)
    {
         Collider[] hits = Physics.OverlapSphere(tp,mydist);
        foreach (Collider mhit in hits)
        {
            if (distags.Contains(mhit.gameObject.tag))
            {
                return false;
            }
        }
        return true;
    }

    /*void Update()
    {
        transform.position = Vector3.Slerp(transform.position, (transform.position - player.transform.position).normalized * mdistance + player.transform.position, Time.deltaTime * mdistancespeed);
        transform.LookAt(player.transform);

    }*/
}
