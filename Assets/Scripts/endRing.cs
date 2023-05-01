using UnityEngine;

// This code is to control the robotic arm model in the shop floor scene.
// When a object collides with the robotic arm end ring, the robotic arm will turn accoriding to the color of the block.
public class endRing : MonoBehaviour
{
    public float speed = 100;
    public GameObject[] objects;
    public Rigidbody arm;
    private float baseYRot = 0.0f;
    private bool isCollided = false;
    private bool isBack = false;
    private Vector3 dir = new Vector3(0, 0, 0);

    private float angle = 0;

    private GameObject myCollider;


    void Update()
    {
        arm.GetComponent<Transform>().localPosition = new Vector3(0.001289082f, 0.253f, -0.004275528f);

        if (isCollided) ProcessMovement(angle, dir);
        else if (isBack) ProcessBackMovement();
        else
        {
            arm.GetComponent<Transform>().localEulerAngles = new Vector3(arm.GetComponent<Transform>().localEulerAngles.x, 0.0f, 0.0f);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "subject")
        {
            myCollider = collision.gameObject;
            dir = new Vector3(0, 0, -2);
            angle = 90.0f;
            isCollided = true;
        }
        else if (collision.collider.tag == "cyl")
        {
            myCollider = collision.gameObject;
            dir = new Vector3(0, 0, -2);
            angle = 30.0f;
            isCollided = true;
        }
        else if (collision.collider.tag == "cone")
        {
            myCollider = collision.gameObject;
            dir = new Vector3(2, 0, 0);
            angle = 120.0f;
            isCollided = true;
        }
    }

    void ProcessMovement(float angle, Vector3 dir)
    {
        baseYRot -= 40 * Time.deltaTime * 1.0f;
        baseYRot = Mathf.Clamp(baseYRot, -180.0f, 180.0f);
        arm.GetComponent<Transform>().localEulerAngles = new Vector3(arm.GetComponent<Transform>().localEulerAngles.x, baseYRot, arm.GetComponent<Transform>().localEulerAngles.z);
        if (baseYRot <= -angle)
        {
            myCollider.GetComponent<Rigidbody>().transform.Translate(dir);
            isCollided = false;
            ProcessBackMovement();
        }
    }

    void ProcessBackMovement()
    {
        isBack = true;
        baseYRot += 50 * Time.deltaTime * 1.0f;
        baseYRot = Mathf.Clamp(baseYRot, -180.0f, 180.0f);
        arm.GetComponent<Transform>().localEulerAngles = new Vector3(arm.GetComponent<Transform>().localEulerAngles.x, baseYRot, arm.GetComponent<Transform>().localEulerAngles.z);
        if (baseYRot >= 0.00f)
        {
            baseYRot = 0.0f;
            arm.GetComponent<Transform>().localEulerAngles = new Vector3(arm.GetComponent<Transform>().localEulerAngles.x, baseYRot, 0.0f);
            isBack = false;
            int i = Random.Range(0, objects.Length);
            Instantiate(objects[i], new Vector3(10, 5, 20), new Quaternion(0, 0, 0, 0));
        }
    }

}
