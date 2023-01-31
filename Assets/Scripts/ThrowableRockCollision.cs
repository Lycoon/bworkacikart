using UnityEngine;

public class ThrowableRockCollision : MonoBehaviour
{
    public ParticleSystem dirtParticleSystem;
    public ParticleSystem grassParticleSystem;
    public float destroyDelay = 3.5f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            dirtParticleSystem.Play();
            Destroy(gameObject, destroyDelay);
        }
    }
}
