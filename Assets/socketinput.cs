
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


public class socketinput : MonoBehaviour
{
    public static string IP = "";
    Thread ReceiveThread;
    public int Port;
    public bool usePort;
    public string debug = "";
    bool useSocket;
    // n Array wär mir ja lieber aber erstmal single input wär ja auch schon was.
    public List<Color> DataOut;
    UdpClient client;
    IPEndPoint endpoint;
    byte[] DataIn;



    //da war doch irgendwas mit 12 byte oder so.. keine Ahnung.. 
    List<Color> BytetoColor(byte[] bytes)
    {
        List<Color> collist = new List<Color>();
        if (bytes == null) return collist;
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
    void Start()
    {
        usePort = false;
        useSocket = false;
        DataOut = new List<Color>();


    }


  public  void ListenToPort()
    {
        if (ReceiveThread != null)
        {
            ReceiveThread.Abort();
            ReceiveThread = null;
        }


        ReceiveThread = new Thread(
             new ThreadStart(ReceiveData));
        ReceiveThread.IsBackground = true;
        ReceiveThread.Start();
    }

    private void ReceiveData()
    {

        client = new UdpClient(Port);
        while (true)
        {

            try
            {
                // Bytes empfangen.
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                DataIn = client.Receive(ref anyIP);
            }
            catch (Exception e)
            {   print(e.ToString());
                debug = e.Message + " PortInput was: " + Port.ToString();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (null != DataIn && DataIn.Length > 0) DataOut = BytetoColor(DataIn);
    }
    void OnApplicationQuit()
    {
        if (ReceiveThread != null)
        {
            Debug.Log("Killed Thread " + ReceiveThread.GetHashCode().ToString());
            ReceiveThread.Abort();
            ReceiveThread = null;
            
        }
    }
}

