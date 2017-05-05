using System;
using System.Net.Sockets;
using System.Net;
using UnityEngine;


public class socketinput : MonoBehaviour {
    public static string IP ="";
    public static int Port;
    // n Array wär mir ja lieber aber erstmal single input wär ja auch schon was.
    public Color DataOut;
    UdpClient client = new UdpClient();
    IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, Port);

    public static byte[] DataIn;
  

    //da war doch irgendwas mit 12 byte oder so.. keine Ahnung.. 
    Color BytetoColor(byte[] bytes)
    {
        Color col = new Color();
        col.r = BitConverter.ToSingle(bytes, 0);
        col.g = BitConverter.ToSingle(bytes, 8);
        col.b = BitConverter.ToSingle(bytes, 16);
        col.a = 1.0f;  // alpha is nich. 
        return col;

    }

	// Use this for initialization
	void Start () {
        
        
	}
	
	// Update is called once per frame
	void Update () {

    DataIn = client.Receive(ref endpoint);
		if(DataIn.Length > 11)
        {
            for(int i = 0; i < DataIn.Length; i = i+3) {
        float r = DataIn[i] / 255.0f;
        float g = DataIn[i + 1];
        float b = DataIn[i + 2];

        
      }
            DataOut = BytetoColor(DataIn);
        }
	}
}
