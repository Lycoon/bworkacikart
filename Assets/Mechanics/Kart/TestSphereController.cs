using UnityEngine;

public class TestSphereController : MonoBehaviour
{
    public Rigidbody sphereRigidbody;
    public Transform modelTransform;

    private float speed;


    public float acceleration = 10f;

    private void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            float forward = Input.GetAxis("Vertical");
            //Debug.Log("forward: " + forward);

            sphereRigidbody.AddForce(modelTransform.forward * forward, ForceMode.Acceleration);
        }
        
        if (Input.GetAxis("Horizontal") != 0)
        {
            float horizontal = Input.GetAxis("Horizontal");
            //Debug.Log("horizontal: " + horizontal);

            sphereRigidbody.AddForce(modelTransform.right * horizontal, ForceMode.Acceleration);
        }
    }

    private void FixedUpdate()
    {
        Vector3 headingToward = new Vector3(sphereRigidbody.velocity.x, 0, sphereRigidbody.velocity.z);
        Vector3 forwardWithoutY = new Vector3(modelTransform.forward.x, 0, modelTransform.forward.z);
        float angle = Vector3.SignedAngle(forwardWithoutY, headingToward, modelTransform.up);

        Debug.Log("angle: " + angle);

        modelTransform.Rotate(modelTransform.up, angle);
        modelTransform.position = sphereRigidbody.transform.position;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(transform.position, transform.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(modelTransform.position, modelTransform.forward * 4);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(modelTransform.position, sphereRigidbody.velocity * 4);
    }
}
