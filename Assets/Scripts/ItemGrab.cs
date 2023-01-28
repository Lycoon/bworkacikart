using UnityEngine;
using UnityEngine.InputSystem;

public class ItemGrab : MonoBehaviour
{
    public InputActionProperty grabAction;
    public float radius = 0.1f;
    public LayerMask grabbableLayer;

    private FixedJoint fixedJoint;
    private bool isGrabbing = false;

    void FixedUpdate()
    {
        bool isGrabButtonPressed = grabAction.action.ReadValue<float>() > 0.1f;

        if (isGrabButtonPressed && !isGrabbing)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, grabbableLayer, QueryTriggerInteraction.Ignore);

            if (colliders.Length > 0)
            {
                Rigidbody nearbyRigidbody = colliders[0].GetComponent<Rigidbody>();

                fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.autoConfigureConnectedAnchor = false;

                if (nearbyRigidbody != null)
                {
                    fixedJoint.connectedBody = nearbyRigidbody;
                    fixedJoint.connectedAnchor = nearbyRigidbody.transform.InverseTransformPoint(transform.position);
                }
                else
                {
                    fixedJoint.connectedBody = colliders[0].attachedRigidbody;
                }

                isGrabbing = true;
            }
        }
        else if (!isGrabButtonPressed && isGrabbing)
        {
            if (fixedJoint != null)
            {
                Destroy(fixedJoint);
            }

            isGrabbing = false;
        }
    }
}
