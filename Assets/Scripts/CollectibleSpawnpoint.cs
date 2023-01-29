using UnityEngine;

public class CollectibleSpawnpoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.up * 15f);
    }
}
