using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleIK : MonoBehaviour
{
    [Header("Joints")]
    public Transform joint0;
    public Transform joint1;
    public Transform hand;

    [Header("Target")]
    public Transform target;

    private float length0;
    private float length1;

    void Start()
    {
        length0 = Vector3.Distance(joint0.position, joint1.position);
        length1 = Vector3.Distance(joint1.position, hand.position);
    }

    void Update()
    {
        float jointAngle0;
        float jointAngle1;

        float length2 = Vector3.Distance(joint0.position, target.position);

        Vector3 diff = target.position - joint0.position;

        // tan^-1(Δz / Δx) (angle in XY plane, top down view)
        float atanXZ = Mathf.Atan2(diff.z, diff.x) * Mathf.Rad2Deg;

        // tan^-1(Δy / Δx) (angle in XZ plane, side view)
        float deltaW = (new Vector2(target.position.x - joint0.position.x, target.position.z - joint0.position.z)).magnitude;
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

    // void Update()
    // {

    //     Vector3 diff = target.position - joint0.position;

    //     float atanXY = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

    //     float deltaW = (new Vector2(target.position.x - joint0.position.x, target.position.y - joint0.position.y)).magnitude;
    //     float atanXZ = Mathf.Atan2(diff.z, deltaW) * Mathf.Rad2Deg;

    //     float length2 = Vector3.Distance(joint0.position, target.position);

    //     float jointAngle0 = 0.0f;
    //     float jointAngle1 = 0.0f;

    //     // Out of reach, fully extend arm
    //     if (length0 + length1 < length2)
    //     {
    //         jointAngle0 = atanXY;
    //         jointAngle1 = 0;
    //     }
    //     else
    //     {
    //         float cosAngle0 = ((length2 * length2) - (length0 * length0) - (length1 * length1)) / (2 * length2 * length0);
    //         float cosAngle1 = ((length1 * length1) + (length0 * length0) - (length2 * length2)) / (2 * length1 * length0);

    //         float angle0 = Mathf.Acos(cosAngle0) * Mathf.Rad2Deg;
    //         float angle1 = Mathf.Acos(cosAngle1) * Mathf.Rad2Deg;

    //         jointAngle0 = -angle0 - atanXZ;
    //         jointAngle1 = 180 - angle1;
    //     }

    //     Vector3 Euler0 = joint0.transform.localEulerAngles;
    //     Euler0.z = atanXY * Mathf.Rad2Deg;
    //     joint0.transform.localEulerAngles = Euler0;

    //     Vector3 Euler1 = joint0.transform.localEulerAngles;
    //     Euler1.y = jointAngle0 * Mathf.Rad2Deg;
    //     joint0.transform.localEulerAngles = Euler1;

    //     Vector3 Euler2 = joint1.transform.localEulerAngles;
    //     Euler2.y = jointAngle1 * Mathf.Rad2Deg;
    //     joint1.transform.localEulerAngles = Euler2;
    // }
}
