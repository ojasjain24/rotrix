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
    List<Location> locations;
    // Location locationData;
    void Start()
    {
        port = 5065;
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

                // locations = JsonConvert.DeserializeObject<List<Location>>(datastfy); 
                Xval = float.Parse(json["x"].ToString());
                Yval = float.Parse(json["y"].ToString());
                // locationData = JsonUtility.FromJson<Location>(Encoding.UTF8.GetString(data).ToString());
                // print(Xval+", "+Yval);
                // jsonUtils.getJson(datastfy);
            }
            catch (Exception e)
            {
                print(e.ToString());
            }
        }
    }


    void Update()
    {
        float Zval = 0;
        float l1 = rotricsLowerBone.GetComponent<MeshFilter>().mesh.bounds.extents.y;
        float l2 = rotricsUpperBone.GetComponent<MeshFilter>().mesh.bounds.extents.y;
        // print(Xval + ", " + Yval + ", " + l1 + ", " + l2);
        float cosVal = Mathf.Clamp((((Xval * Xval) + (Yval * Yval) - (l1 * l1) - (l2 * l2)) / (2 * l1 * l2)), -1, +1);
        float beta = Mathf.Acos(cosVal);
        float theta = Mathf.Asin((Xval * l2 * Mathf.Sin(beta) - (l1 * Yval + l2 * Yval * Mathf.Cos(beta))) / (l1 * l1 + l2 * l2 + 2 * l1 * l2 * Mathf.Cos(beta)));

        float alpha = theta + beta;
        float baseAngle = Mathf.Atan(Zval / Xval) * 20;
        print("THETA : " + theta * 180 / Mathf.PI);



        rotricsLowerBone.GetComponent<Transform>().localRotation = Quaternion.Euler(-theta * 180 / Mathf.PI, 0, 0);

        rotricsUpperBone.GetComponent<Transform>().localRotation = Quaternion.Euler(alpha * 180 / Mathf.PI, 0, 0);

        // rotricsBase.GetComponent<Transform>().localEulerAngles = new Vector3(-90, 0, baseAngle * 180 / Mathf.PI);
        // robotBase.localEulerAngles = new Vector3(robotBase.localEulerAngles.x, baseYRot, robotBase.localEulerAngles.z);

    }
}
