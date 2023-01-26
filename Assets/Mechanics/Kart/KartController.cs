using UnityEngine;

public class KartController : MonoBehaviour
{
    const float GRAVITY = 9.81f;

    private float rotate, currentRotate;
    private float speed, currentSpeed;

    [Header("References")]
    [SerializeField] private Rigidbody kartRigidbody;
    [SerializeField] private LayerMask layerMask;

    [Header("Kart properties")]
    [SerializeField] private float acceleration = 60f;
    [SerializeField] private float steering = 10f;

    private void Update()
    {
        transform.position = kartRigidbody.transform.position - new Vector3(0, 1f, 0);

        //Accelerate
        if (Input.GetButton("Fire1"))
            speed = acceleration;

        //Steer
        if (Input.GetAxis("Horizontal") != 0)
        {
            int dir = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
            float amount = Mathf.Abs((Input.GetAxis("Horizontal")));
            Steer(dir, amount);
        }

        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f);
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f);

        speed = 0;
        rotate = 0;
    }

    private void FixedUpdate()
    {
        // Forward acceleration
        kartRigidbody.AddForce(transform.forward * currentSpeed, ForceMode.Acceleration);

        // Gravity
        kartRigidbody.AddForce(Vector3.down * GRAVITY, ForceMode.Acceleration);

        // Steering
        /*transform.eulerAngles = Vector3.Lerp(
            transform.eulerAngles, 
            new Vector3(0, transform.eulerAngles.y + currentRotate, 0),
            Time.deltaTime * 5f
        );
        */

        RaycastHit hitOn, hitNear;
        Physics.Raycast(transform.position + (transform.up * 0.1f), Vector3.down, out hitOn, 1.1f, layerMask);
        Physics.Raycast(transform.position + (transform.up * 0.1f), Vector3.down, out hitNear, 2.0f, layerMask);

        // Normal rotation
        Quaternion oldRotation = transform.rotation;
        transform.up = Vector3.Lerp(transform.up, hitNear.normal, Time.deltaTime * 8.0f);
        transform.rotation = Quaternion.Lerp(
            oldRotation, 
            Quaternion.Euler(oldRotation.eulerAngles.x, oldRotation.eulerAngles.y + currentRotate, oldRotation.eulerAngles.z), 
            Time.deltaTime * 5.0f
        );
        //transform.RotateAroundLocal(transform.up, currentRotate);
    }

    public void Steer(int direction, float amount)
    {
        rotate = (steering * direction) * amount;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
