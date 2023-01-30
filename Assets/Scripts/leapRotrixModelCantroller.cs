using Leap;
using Leap.Unity;
using UnityEngine;

public class leapRotrixModelCantroller : MonoBehaviour
{
    public LeapServiceProvider LeapServiceProvider;
    public Rigidbody arm;

    void Update()
    {
        for (int i = 0; i < LeapServiceProvider.CurrentFrame.Hands.Count; i++)
        {
            Hand _hand = LeapServiceProvider.CurrentFrame.Hands[i];

            Arm _arm = _hand.Arm;

            float _armLength = _arm.Length;
            float _armWidth = _arm.Width;
            Vector3 _elbowPosition = _arm.ElbowPosition;

            arm.GetComponent<Transform>().localEulerAngles = new Vector3(arm.GetComponent<Transform>().localEulerAngles.x, _hand.PalmPosition.y * 500, arm.GetComponent<Transform>().localEulerAngles.z);

        }
    }
}
