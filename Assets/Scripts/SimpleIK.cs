using UnityEngine;

public class SimpleIK : MonoBehaviour
{
    public Transform joint0;
    public Transform joint1;
    public Transform hand;

    private float length0;
    private float length1;

    public float TotalLength => length0 + length1;

    public SimpleIK(Transform joint0, Transform joint1, Transform hand)
    {
        this.joint0 = joint0;
        this.joint1 = joint1;
        this.hand = hand;

        length0 = Vector3.Distance(joint0.position, joint1.position);
        length1 = Vector3.Distance(joint1.position, hand.position);
    }

    public void Solve(Vector3 targetPosition)
    {
        float jointAngle0;
        float jointAngle1;

        float length2 = Vector3.Distance(joint0.position, targetPosition);

        Vector3 diff = targetPosition - joint0.position;

        // tan^-1(Δz / Δx) (angle in XY plane, top down view)
        float atanXZ = Mathf.Atan2(diff.z, diff.x) * Mathf.Rad2Deg;

        // tan^-1(Δy / Δx) (angle in XZ plane, side view)
        float deltaW = (new Vector2(targetPosition.x - joint0.position.x, targetPosition.z - joint0.position.z)).magnitude;
        float atanXY = Mathf.Atan2(diff.y, deltaW) * Mathf.Rad2Deg;

        if (length0 + length1 < length2)
        {
            jointAngle0 = atanXY;
            jointAngle1 = 0;
        }
        else
        {
            // (b² + c² - a²) / 2bc
            float cosAngle0 = ((length2 * length2) + (length0 * length0) - (length1 * length1)) / (2 * length2 * length0);
            // (a² + c² - b²) / 2ac
            float cosAngle1 = ((length1 * length1) + (length0 * length0) - (length2 * length2)) / (2 * length1 * length0);

            float angle0 = Mathf.Acos(cosAngle0) * Mathf.Rad2Deg;
            float angle1 = Mathf.Acos(cosAngle1) * Mathf.Rad2Deg;

            jointAngle0 = angle0 + atanXY;
            jointAngle1 = angle1 - 180;
        }

        Vector3 Euler0 = joint0.transform.eulerAngles;
        Euler0.y = -atanXZ; // Rotate the whole arm around the vertical axis
        joint0.transform.eulerAngles = Euler0;

        Euler0 = joint0.transform.localEulerAngles;
        // Euler0.y = -atanXZ; // Rotate the whole arm around the vertical axis
        Euler0.z = jointAngle0;
        joint0.transform.localEulerAngles = Euler0;

        Vector3 Euler1 = joint1.transform.localEulerAngles;
        Euler1.z = jointAngle1;
        joint1.transform.localEulerAngles = Euler1;
    }
}
