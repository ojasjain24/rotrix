using UnityEngine;

public class endRing : MonoBehaviour
{
    public float speed = 100;
    public Rigidbody arm;
    private float baseYRot = 0.0f;
    private bool isCollided = false;

    private GameObject myCollider;
    void Update()
    {
        if (isCollided) ProcessMovement();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "subject")
        {
            myCollider = collision.gameObject;
            isCollided = true;
        }
    }

    void ProcessMovement()
    {
        baseYRot -= 20 * Time.deltaTime * 1.0f;
        baseYRot = Mathf.Clamp(baseYRot, -90.0f, 90.0f);
        arm.GetComponent<Transform>().localEulerAngles = new Vector3(arm.GetComponent<Transform>().localEulerAngles.x, baseYRot, arm.GetComponent<Transform>().localEulerAngles.z);
        if (baseYRot <= -90.0f)
        {
            myCollider.GetComponent<Rigidbody>().transform.Translate(0, 0, -2);
            isCollided = false;
        }
    }

}
