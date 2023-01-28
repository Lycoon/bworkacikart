using UnityEngine;

public class LegTargetPlacement : MonoBehaviour
{
    void Update()
    {
        Vector3 castPosition = new Vector3(transform.position.x, transform.parent.position.y, transform.position.z);

        if (Physics.Raycast(castPosition, Vector3.down, out RaycastHit hit, 10f))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            Debug.DrawRay(castPosition, Vector3.down * hit.distance, Color.red);
        }
    }
}
