using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class ItemGrab : MonoBehaviour
{
    public InputActionProperty grabAction;
    public InputActionProperty triggerAction;
    public LayerMask grabbableLayer;

    public float radius = 2f;
    public Transform grabPoint;
    public Transform projectileSpawnPoint;

    public GameObject projectilePrefab;

    private FixedJoint fixedJoint;
    private bool isGrabbing = false;

    private GameObject spawnedProjectile;

    void FixedUpdate()
    {
        bool isGrabButtonPressed = grabAction.action.ReadValue<float>() > 0.1f;
        bool isTriggerButtonPressed = triggerAction.action.ReadValue<float>() > 0.1f;

        if (isGrabButtonPressed && !isGrabbing)
        {
            Collider[] colliders = Physics.OverlapSphere(grabPoint.position, radius, grabbableLayer, QueryTriggerInteraction.Ignore);
            Collider[] orderedByProximity = colliders.OrderBy(c => (grabPoint.position - c.transform.position).sqrMagnitude).ToArray();

            if (orderedByProximity.Length > 0)
            {
                Collider collider = orderedByProximity[0];

                fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.autoConfigureConnectedAnchor = false;

                Rigidbody nearbyRigidbody = collider.GetComponent<Rigidbody>();
                if (nearbyRigidbody != null)
                {
                    fixedJoint.connectedBody = nearbyRigidbody;
                    fixedJoint.connectedAnchor = nearbyRigidbody.transform.InverseTransformPoint(transform.position);
                }
                else
                {
                    fixedJoint.connectedBody = collider.attachedRigidbody;
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

        if (isTriggerButtonPressed && !isGrabbing && spawnedProjectile == null)
        {
            spawnedProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (grabPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(grabPoint.position, radius);
        }
    }
}
