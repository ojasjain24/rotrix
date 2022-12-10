using Leap;
using Leap.Unity;
using UnityEngine;

public class Example : MonoBehaviour
{
    public LeapServiceProvider LeapServiceProvider;
    public Rigidbody rb;

    void Update()
    {
        for (int i = 0; i < LeapServiceProvider.CurrentFrame.Hands.Count; i++)
        {
            Hand _hand = LeapServiceProvider.CurrentFrame.Hands[i];

            Arm _arm = _hand.Arm;

            float _armLength = _arm.Length;
            float _armWidth = _arm.Width;
            Vector3 _elbowPosition = _arm.ElbowPosition;

            if (!_hand.Fingers[0].IsExtended && !_hand.Fingers[1].IsExtended && !_hand.Fingers[2].IsExtended && !_hand.Fingers[3].IsExtended && !_hand.Fingers[4].IsExtended)
            {
                rb.transform.position = Vector3.MoveTowards(transform.position, new Vector3(_hand.PalmPosition.x, _hand.PalmPosition.y, _hand.PalmPosition.z + 0.5f), 1000 * Time.deltaTime);
            };

            rb.transform.position = Vector3.MoveTowards(transform.position, new Vector3(_hand.PalmPosition.x * 2, _hand.PalmPosition.y, _hand.PalmPosition.z + 2), 10 * Time.deltaTime);
            // transform.Translate(_hand.PalmPosition * Time.deltaTime);
        }
    }
}