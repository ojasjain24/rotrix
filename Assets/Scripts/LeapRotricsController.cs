using Leap;
using Leap.Unity;
using UnityEngine;
using System.IO.Ports;

public class LeapRotricsController : MonoBehaviour
{
    private SerialPort data_stream = new SerialPort("COM8", 115200);

    void Start()
    {
        data_stream.Open();

    }
    public LeapServiceProvider LeapServiceProvider;
    // public Rigidbody arm;

    void Update()
    {
        for (int i = 0; i < LeapServiceProvider.CurrentFrame.Hands.Count; i++)
        {
            Hand _hand = LeapServiceProvider.CurrentFrame.Hands[i];

            Arm _arm = _hand.Arm;
            float Y = _hand.PalmPosition.y * 500;
            data_stream.WriteLine("G0 Y" + Y);
        }
    }

    public void moveToHome()
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
