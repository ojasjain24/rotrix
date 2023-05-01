using Leap;
using Leap.Unity;
using UnityEngine;
using System.IO.Ports;

// Main Code
// This code controls the robotic arm using leap motion.
public class LeapRotricsController : MonoBehaviour
{
    private SerialPort data_stream = new SerialPort("COM9", 115200);
    bool isClosed = false;
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
            Hand _hand = Hands.Left ?? Hands.Right;
            Arm _arm = _hand.Arm;
            float Y = _hand.WristPosition.y * 500;
            float X = _hand.WristPosition.x * 500;
            float Z = _hand.WristPosition.z * 500;
            if (!_hand.Fingers[0].IsExtended && !_hand.Fingers[1].IsExtended && !_hand.Fingers[2].IsExtended && !_hand.Fingers[3].IsExtended && !_hand.Fingers[4].IsExtended)
            {
                fistClose(_hand);

            }
            if (_hand.Fingers[0].IsExtended && _hand.Fingers[1].IsExtended && !_hand.Fingers[2].IsExtended && !_hand.Fingers[3].IsExtended && !_hand.Fingers[4].IsExtended)
            {
                fistOpen();
            }
            data_stream.WriteLine("G1 X" + X);
            data_stream.WriteLine("G1 Y" + Y);
            data_stream.WriteLine("G1 Z" + Z);
        }
    }

    public void fistClose(Hand _hand)
    {
        if (data_stream.IsOpen && !isClosed)
        {
            print("CLOSE");

            data_stream.WriteLine("M1001");
            isClosed = true;
        }
    }

    public void fistOpen()
    {

        if (data_stream.IsOpen && isClosed)
        {
            print("OPEN");
            data_stream.WriteLine("M1002");
            isClosed = false;
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
