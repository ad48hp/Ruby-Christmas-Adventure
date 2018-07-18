using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class ConkerMultiplayerSettingMachine : NetworkBehaviour
{
    NetworkClient myclient;
    private short chatMessage = short.Parse("169");
    public bool registered = false;
    public Vector3 lastposition;
    public float walkspeed;
    public float jumpspeed;
    public Vector3 gravity;

    private void ReceiveMessage(NetworkMessage message)
    {
        string text = message.ReadMessage<StringMessage>().value;
        Debug.Log(text);
    }

    private void ServerReceiveMessage(NetworkMessage message)
    {
        StringMessage myMessage = new StringMessage();
        myMessage.value = message.conn.connectionId + ": " + message.ReadMessage<StringMessage>().value;
        //NetworkServer.SendToAll(chatMessage, myMessage);
    }

    private void SendMsg(string input, bool isServer)
    {
        StringMessage myMessage = new StringMessage();
        myMessage.value = input;
        /*if (!isServer)
        {*/
        NetworkManager.singleton.client.Send(chatMessage, myMessage);

    }

    void Start()
    {
        //RenderSettings.ambientIntensity = 0.05F;
        if (isLocalPlayer)
        {
            GameObject spawnp;
            spawnp = GameObject.Find("SpawnPlayerPoint");
            transform.position = spawnp.transform.position;
            transform.rotation = spawnp.transform.rotation;
            GameObject mcamera;
            mcamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>().player = gameObject;
            if (!CameraScript.Vector3Equals(spawnp.transform.localScale, Vector3.one))
            {
                transform.localScale = spawnp.transform.localScale;
                GetComponent<ConkerNotRubyScript>().myspeed = walkspeed * CameraScript.Vector3Prumer(transform.localScale);
                GetComponent<ConkerNotRubyScript>().jumpforce = jumpspeed * CameraScript.Vector3Prumer(transform.localScale);
                GetComponent<ConkerNotRubyScript>().gravity = Vector3.Scale(gravity, transform.localScale);
            }
            GetComponent<ConkerNotRubyScript>().enabled = true;
        }
        else
        {
            Destroy(GetComponent<Rigidbody>());
        }
    }

    void LateUpdate()
    {
        /* if (!registered)
         {
             if (NetworkServer.active)
             {
                 //isServer = true;
                 NetworkServer.RegisterHandler(chatMessage, ServerReceiveMessage);
                 //registered = true;
             }
             NetworkManager.singleton.client.RegisterHandler(chatMessage, ReceiveMessage);
         }
         if (isLocalPlayer)
         {
             SendMsg(transform.position.ToString(), isServer);
         }*/
        if (!isLocalPlayer)
        {
            if (Vector3.Distance(transform.position, lastposition) > 0.21F && !GetComponent<Animation>().IsPlaying("RubyWalk"))
            {
                GetComponent<Animation>().Play("conkerwalk");
                lastposition = transform.position;
            }
            else
            {
                //GetComponent<Animation>().Stop("RubyWalk");
            }
        }
    }

}
