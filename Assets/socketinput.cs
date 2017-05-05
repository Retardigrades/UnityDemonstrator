using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;


public class socketinput : MonoBehaviour {
    public static string IP ="";
    public  int Port;
    public bool usePort;
    public string debug ="";
    bool useSocket;
    // n Array wär mir ja lieber aber erstmal single input wär ja auch schon was.
    public List<Color> DataOut;
    UdpClient client = new UdpClient();
    IPEndPoint endpoint; 
     byte[] DataIn;
  

    //da war doch irgendwas mit 12 byte oder so.. keine Ahnung.. 
    List<Color> BytetoColor(byte[] bytes)
    {
        List<Color> collist = new List<Color>();
        if (bytes.Length % 3 == 0 && bytes.Length > 2)
        {
            for (int i = 0; i < bytes.Length; i = i + 3)
            {
                Color col = new Color();
                col.r = bytes[i] / 255.0f;
                col.g = bytes[i + 1];
                col.b = bytes[i + 2];
                col.a = 1.0f;
                collist.Add(col);

            }
            debug = "";
        }
        else debug = ("bytebuffer corrupt. Length Mod 3 not 0.");


        return collist;

    }

	// Use this for initialization
	void Start () {
        usePort = false;
        useSocket = false;
        DataOut = new List<Color>();
        

    }

    // Update is called once per frame
    void Update () {

        if (usePort)
        {
            endpoint = new IPEndPoint(IPAddress.Any, Port);
            useSocket = true;
        }
        if(useSocket)
        {     
            DataIn = client.Receive(ref endpoint);
            DataOut = BytetoColor(DataIn);
        }
        
	}
}
