using UnityEngine;

public class DeathAnimationLegTarget : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
