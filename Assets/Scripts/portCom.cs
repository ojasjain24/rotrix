using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO.Ports;
using UnityEngine;

public class portCom : MonoBehaviour
{
    private SerialPort data_stream = new SerialPort("COM8", 115200);
    void Start()
    {
        data_stream.Open();

    }

    void Update()
    {

    }

    public void moveToWorkHeight()
    {
        if (data_stream.IsOpen)
        {
            data_stream.WriteLine("M1112");
        }
    }

    public void moveToXYZ()
    {
        if (data_stream.IsOpen)
        {
            data_stream.WriteLine("G0 X350 Y50");
        }
    }
}