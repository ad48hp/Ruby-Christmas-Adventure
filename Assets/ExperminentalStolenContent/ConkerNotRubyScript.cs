using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConkerNotRubyScript : MonoBehaviour
{
    public Vector3 gravity = new Vector3(0, -0.5F, 0);
    public Rigidbody rig;
    public float speedtime;
    public float myspeed = 3.102F;
    public float walkspeed = 0;
    public float minws = 0.2F;
    public float rotspeed = 0.5F;
    public bool canjump;
    public bool canjump2 = true;
    public float jumpforce = 250;
    public bool supposedToJump = false;
    public bool supposedToJump2 = false;
    public Animation manimation;
    public bool canfly = true;
    public float flygravity = 0.25F;
    public bool isFlying = false;
    public float[] cons = { 0.01656464062F };
    public bool canfly2;
    public HeadLook mheadlook;
    public float ledspeed = 0.15F;
    public Vector3 mforce = new Vector3(0, 0, 0);
    public float delforce = 3.14F;
    public float coldwaterjumpspeed = 325;
    public GameObject mcar;
    public float ccounter = 0.25F;
    public int idlei;
    public int maxidlei;
    public float idlespeed;
    public float preparejumpspeed = 1;
    public float ajumpspeed = 1;
    public bool playme;
    public int myšit;
    public bool enidleanim;
    public Vector3 rotaxis = Vector3.forward;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        manimation = GetComponent<Animation>();
        if (mcar == null)
        {
            mcar = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    void Update()
    {
        /*List<Collider> colssz = CameraScript.GetObjs(transform.position,-Vector3.up, 0.25F, new string[] { "led" }.ToList());
            if (colssz.Count > 0)
            {
                Collider col = colssz[0];
                if (col.gameObject.tag == "led")
                {
                    if (col.gameObject.GetComponent<RotateMeScript>() != null && col.gameObject.GetComponent<RotateMeScript>().enabled)
                    {
                        //transform.position += Quaternion.Euler(col.GetComponent<RotateMeScript>().rot) * (transform.position - col.gameObject.transform.position) * Time.deltaTime;
                    }
                    if (col.gameObject.transform.parent.GetComponent<TavecScript>() != null && col.gameObject.transform.parent.GetComponent<TavecScript>().enabled)
                    {
                        TavecScript myscr = col.transform.parent.gameObject.GetComponent<TavecScript>();
                        if (myscr.canmove())
                        {
                        transform.position = TavecScript.WhatMove(transform.position, myscr.points[myscr.currentindex].transform.position, myscr.movespeed);
                            mcar.transform.position = TavecScript.WhatMove(mcar.transform.position, myscr.points[myscr.currentindex].transform.position, myscr.movespeed);
                        }
                    }
                }
            }*/
        if (Input.GetKeyDown(KeyCode.L))
        {
            mheadlook.enabled = !mheadlook.enabled;
        }
        if (Input.GetKeyDown(KeyCode.Space) && canjump2 && canjump && !manimation.IsPlaying("conkerpreparetojump"))
        {
            canjump = false;
            supposedToJump = true;
           manimation.Play("conkerpreparetojump");
            manimation["conkerpreparetojump"].speed = preparejumpspeed;

        }
        if (Input.GetKeyDown(KeyCode.Space) && canjump2 && canfly2 && !canjump && !isFlying && !supposedToJump)
        {
            isFlying = true;
            manimation.Play("conkerfly");
            manimation["conkerfly"].speed = ajumpspeed;
        }
        if (Input.GetKeyUp(KeyCode.Space) && isFlying)
        {
            isFlying = false;
            manimation.Stop("conkerfly");
        }
        //Debug.Log(manimation["RubyJumpStartLegacy"].time.ToString());
        if (supposedToJump && manimation.IsPlaying("conkerpreparetojump") && manimation["conkerpreparetojump"].time >= 12 * cons[0])
        {
            supposedToJump = false;
            rig.AddForce((transform.forward * jumpforce));
            canfly2 = true;
        }
        if (supposedToJump2 && !manimation.IsPlaying("conkerpreparetojump"))
        {
            supposedToJump2 = false;
            manimation.Play("conkerfly");
            manimation["conkerfly"].speed = ajumpspeed;
        }
        if (walkspeed < minws)
        {
            if (manimation.IsPlaying("conkerwalk"))
            {
                manimation.Stop("conkerwalk");
            }
            if (enidleanim)
            {
                idlei += 1;
                if (idlespeed > 0 && !manimation.IsPlaying("conkeridle") && !manimation.IsPlaying("conkeridlenormal"))
                {
                    if (idlei > maxidlei)
                    {
                        if (!playme)
                        {
                            idlei = 0;
                            playme = true;
                            myšit = Random.Range(1, 2 + 1);
                            switch (myšit)
                            {
                                case 1:
                                    manimation.Play("conkeridle");
                                    manimation["conkeridle"].speed = idlespeed;
                                    break;
                                case 2:
                                    manimation.Play("conkeridlenormal");
                                    manimation["conkeridlenormal"].speed = idlespeed;
                                    break;
                            }
                        }
                    }
                    if (playme)
                    {
                        playme = false;
                        switch (myšit)
                        {
                            case 1:
                                manimation.Play("conkeridle");
                                manimation["conkeridle"].time = manimation["conkeridle"].length;
                                manimation["conkeridle"].speed = -idlespeed;
                                break;
                            case 2:
                                manimation.Play("conkeridlenormal");
                                manimation["conkeridlenormal"].time = manimation["conkeridlenormal"].length;
                                manimation["conkeridlenormal"].speed = -idlespeed;
                                break;
                        }
                    }
                }
            }
        }
        else
        {
            if (!manimation.IsPlaying("conkerwalk"))
            {
                /*if (manimation.IsPlaying("conkeridle"))
                {
                    manimation.Stop("conkeridle");
                }
                if (manimation.IsPlaying("conkeridlenormal"))
                {
                    manimation.Stop("conkeridlenormal");
                }*/
                manimation.Blend("conkerwalk");
            }
            else
            {
                manimation["conkerwalk"].speed = walkspeed;
            }
        }
        if (CameraScript.IsFree(transform.position, -transform.up, 0.3F, new string[] { "led" }.ToList()))
        {
            if (Input.GetKey(KeyCode.UpArrow) /*|| Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow)*/)
            {
                walkspeed = Mathf.Lerp(walkspeed, 1, speedtime * Time.deltaTime);
            }
            else
            {
                walkspeed = Mathf.Lerp(walkspeed, 0, speedtime * Time.deltaTime);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow) /*|| Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow)*/)
            {
                walkspeed = Mathf.Lerp(walkspeed, 1, speedtime * ledspeed * Time.deltaTime);
            }
            else
            {
                walkspeed = Mathf.Lerp(walkspeed, 0, speedtime * ledspeed * Time.deltaTime);
            }
        }
    }

    void FixedUpdate()
    {
        if (!isFlying)
        {
            rig.AddForce(gravity);
        }
        else
        {
            rig.AddForce(-transform.up * flygravity);
        }
        mforce = mforce / delforce;
        mforce += -transform.up * myspeed * walkspeed * Time.deltaTime;
        //if (Input.GetKey(KeyCode.UpArrow)) {
        transform.position += mforce;
        //}
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(-rotaxis * rotspeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(rotaxis * rotspeed);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "water")
        {
            rig.AddForce(transform.up * coldwaterjumpspeed);
        }
    }

    void OnCollisionStay(Collision col)
    {
        /*if (col.collider.gameObject.tag == "pohyb")
        {
            if (col.collider.gameObject.transform.parent.GetComponent<TavecScript>() != null && col.collider.gameObject.transform.parent.GetComponent<TavecScript>().enabled)
            {
                TavecScript myscr = col.collider.transform.parent.gameObject.GetComponent<TavecScript>();
                if (myscr.canmove())
                {
                    transform.position = TavecScript.WhatMove(transform.position, myscr.points[myscr.currentindex].transform.position, myscr.movespeed);
                    mcar.transform.position = TavecScript.WhatMove(mcar.transform.position, myscr.points[myscr.currentindex].transform.position, myscr.movespeed);
                }
            }
        }

        if (col.collider.gameObject.tag == "led")
        {
            if (col.collider.gameObject.GetComponent<RotateMeScript>() != null && col.collider.gameObject.GetComponent<RotateMeScript>().enabled)
            {
                //transform.position += Quaternion.Euler(col.collider.GetComponent<RotateMeScript>().rot) * (transform.position - col.collider.gameObject.transform.position) * Time.deltaTime;
            }
            if (col.collider.gameObject.transform.parent.GetComponent<TavecScript>() != null && col.collider.gameObject.transform.parent.GetComponent<TavecScript>().enabled)
            {
                TavecScript myscr = col.collider.transform.parent.gameObject.GetComponent<TavecScript>();
                if (myscr.canmove())
                {
                    transform.position = TavecScript.WhatMove(transform.position, myscr.points[myscr.currentindex].transform.position, myscr.movespeed);
                    mcar.transform.position = TavecScript.WhatMove(mcar.transform.position, myscr.points[myscr.currentindex].transform.position, myscr.movespeed);
                }
            }
        }
        if (col.collider.gameObject.tag == "moving")
        {
            MoveMe myscr = col.collider.gameObject.GetComponent<MoveMe>();
            transform.position += myscr.post * Time.deltaTime;
        }*/
        //if (col.collider.gameObject.tag == "solid" || col.collider.gameObject.tag == "pohyb" || col.collider.gameObject.tag == "led" || col.collider.gameObject.tag == "moving")
        if (col.collider.gameObject.tag != "Player")
        {
            if (isFlying)
            {
                manimation.Stop("conkerfly");
                isFlying = false;
            }
            canfly2 = true;
            canjump = true;
        }
    }
}
