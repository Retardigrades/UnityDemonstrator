using System;
using System.Net.Sockets;
using System.Net;
using UnityEngine;


public class socketinput : MonoBehaviour {
    public static string IP ="";
    public static int Port;
    // n Array wär mir ja lieber aber erstmal single input wär ja auch schon was.
    public Color DataOut;

    public static byte[] DataIn;

    //from: https://msdn.microsoft.com/en-us/library/kb5kfec7(v=vs.110).aspx  << lets see if it does the trick
    public static void Startclient()
    {
        byte[] bytes = new byte[12];

        try
        { IPAddress IPadress = IPAddress.Parse(IP);
          IPEndPoint remoteEP = new IPEndPoint(IPadress, Port);
          Socket sender = new Socket(AddressFamily.InterNetwork,
          SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sender.Connect(remoteEP);
                int received = sender.Receive(bytes);
                DataIn = bytes;

                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (ArgumentNullException ane)
            {
                Debug.Log("ArgumentNullException : {0}" + ane.ToString());
            }
            catch (SocketException se)
            {
                Debug.Log("SocketException : {0}" + se.ToString());
            }
            catch (Exception e)
            {
               Debug.Log("Unexpected exception : {0}" + e.ToString());
            }

        }
        catch (Exception e)
        {
           Debug.Log(e.ToString());
        }


    }

    //da war doch irgendwas mit 12 byte oder so.. keine Ahnung.. 
    Color BytetoColor(byte[] bytes)
    {
        Color col = new Color();
        col.r = BitConverter.ToSingle(bytes, 0);
        col.g = BitConverter.ToSingle(bytes, 4);
        col.b = BitConverter.ToSingle(bytes, 8);
        col.a = 1.0f;  // alpha is nich. 
        return col;

    }

	// Use this for initialization
	void Start () {
        Startclient();
        
	}
	
	// Update is called once per frame
	void Update () {
		if(DataIn.Length > 11)
        {
            DataOut = BytetoColor(DataIn);
        }
	}
}
