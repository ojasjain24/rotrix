using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO.Ports;


// Main Code
// This code controls the robotic arm and its digital twin using computer vision.
// Acts as a server, recieves hands movement data from python client.

public class RotricsCVController : MonoBehaviour
{
    Thread receiveThread;
    UdpClient client;
    int port;
    public Rigidbody rotricsBase;
    public Rigidbody rotricsLowerBone;
    public Rigidbody rotricsUpperBone;
    float Xval;
    float Yval;
    float Zval;
    private SerialPort data_stream = new SerialPort("COM8", 115200);

    void Start()
    {
        data_stream.Open();
        port = 8000;
        InitUDP();
    }

    private void InitUDP()
    {
        print("UDP Initialized");
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

    }

    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port);
                byte[] data = client.Receive(ref anyIP);
                String datastfy = Encoding.UTF8.GetString(data);

                JObject json = JObject.Parse(datastfy);
                Xval = float.Parse(json["x"].ToString());
                Yval = float.Parse(json["y"].ToString());
                Zval = float.Parse(json["z"].ToString());
                print(json["time"]);
            }
            catch (Exception e)
            {
                print(e.ToString());
            }
        }
    }


    void Update()
    {
        float l1 = rotricsLowerBone.GetComponent<MeshFilter>().mesh.bounds.extents.y;
        float l2 = rotricsUpperBone.GetComponent<MeshFilter>().mesh.bounds.extents.y;
        // print(Xval + ", " + Yval + ", " + l1 + ", " + l2);
        float cosVal = Mathf.Clamp((((Xval * Xval) + (Yval * Yval) - (l1 * l1) - (l2 * l2)) / (2 * l1 * l2)), -1, +1);
        float beta = Mathf.Acos(cosVal);
        float theta = Mathf.Asin((Xval * l2 * Mathf.Sin(beta) - (l1 * Yval + l2 * Yval * Mathf.Cos(beta))) / (l1 * l1 + l2 * l2 + 2 * l1 * l2 * Mathf.Cos(beta)));

        float alpha = theta + beta;
        float baseAngle = Mathf.Atan(Zval / Xval) * 20;
        // print("THETA : " + theta * 180 / Mathf.PI);



        rotricsLowerBone.GetComponent<Transform>().localRotation = Quaternion.Euler(-theta * 180 / Mathf.PI, 0, 0);

        rotricsUpperBone.GetComponent<Transform>().localRotation = Quaternion.Euler(alpha * 180 / Mathf.PI, 0, 0);

        rotricsBase.GetComponent<Transform>().localEulerAngles = new Vector3(-90, 0, baseAngle * 180 / Mathf.PI);
        data_stream.WriteLine("G1 X" + Xval*20);
        data_stream.WriteLine("G1 Y" + Yval*20);
        data_stream.WriteLine("G1 Z" + Zval*20);
        // robotBase.localEulerAngles = new Vector3(robotBase.localEulerAngles.x, baseYRot, robotBase.localEulerAngles.z);

    }
}