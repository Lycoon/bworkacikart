using UnityEngine;

public class IKLeg : MonoBehaviour
{
    [Header("Joints")]
    public Transform joint0;
    public Transform joint1;
    public Transform hand;

    [Header("Target")]
    public Transform target;

    public float distanceBeforeTargetUpdate = 1f;
    public float legPlacementSpeed = 30f;
    public float legPivotCenterOffset = 0.3f;

    private SimpleIK legIK;

    private Transform worldTarget;
    private Vector3 startTargetPosition;
    private Vector3 endTargetPosition;
    private float lastTargetUpdate;
    private float legPlacementDuration => distanceBeforeTargetUpdate / legPlacementSpeed;


    void Start()
    {
        legIK = new SimpleIK(joint0, joint1, hand);

        worldTarget = Object.Instantiate(target.gameObject, Utils.GroundPosition(target.position), target.rotation).transform;
        startTargetPosition = worldTarget.position;
        endTargetPosition = worldTarget.position;
        lastTargetUpdate = Time.time;
    }

    void Update()
    {
        Vector3 targetPosition = Utils.GroundPosition(target.position);

        float elapsed = Time.time - lastTargetUpdate;

        if (Vector3.Distance(worldTarget.position, targetPosition) > distanceBeforeTargetUpdate && elapsed > Time.deltaTime)
        {
            startTargetPosition = worldTarget.position;
            endTargetPosition = targetPosition;
            lastTargetUpdate = Time.time;
        }

        Vector3 center = (startTargetPosition + endTargetPosition) / 2;
        center -= Vector3.up * legPivotCenterOffset;

        Vector3 relativeStart = startTargetPosition - center;
        Vector3 relativeEnd = endTargetPosition - center;

        worldTarget.position = center + Vector3.Slerp(relativeStart, relativeEnd, elapsed / legPlacementDuration);
        legIK.Solve(worldTarget.position);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(target.position, 0.1f);
    }
}
