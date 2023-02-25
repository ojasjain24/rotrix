using Leap;
using Leap.Unity;
using UnityEngine;

public class leapRotrixModelCantroller : MonoBehaviour
{
    public LeapServiceProvider LeapServiceProvider;
    public Rigidbody rotricsBase;
    public Rigidbody rotricsLowerBone;
    public Rigidbody rotricsUpperBone;

    public Rigidbody rotricsGripper;
    void Update()
    {
        for (int i = 0; i < LeapServiceProvider.CurrentFrame.Hands.Count; i++)
        {
            Hand _hand = LeapServiceProvider.CurrentFrame.Hands[i];

            Arm _arm = _hand.Arm;

            float _armLength = _arm.Length;
            float _armWidth = _arm.Width;

            Vector3 _wristPosition = _arm.WristPosition;
            Vector3 _gripperPosition = rotricsGripper.GetComponent<Transform>().localPosition;
            float Xval = (_wristPosition.x) * 5;
            float Yval = (_wristPosition.y) * 5;
            float Zval = (_wristPosition.z) * 5;
            Vector3 _elbowAngle = _arm.ElbowPosition - _arm.WristPosition;
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
            // robotBase.localEulerAngles = new Vector3(robotBase.localEulerAngles.x, baseYRot, robotBase.localEulerAngles.z);

        }
    }
}
