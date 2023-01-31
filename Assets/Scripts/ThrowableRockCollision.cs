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
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Tree"))
        {
            grassParticleSystem.Play();
            dirtParticleSystem.Play();
            if (collision.transform.parent.gameObject.GetComponent<Rigidbody>() == null)
            {
                collision.transform.parent.gameObject.AddComponent<Rigidbody>();
                Destroy(collision.transform.parent.gameObject, destroyDelay);
            }
            Destroy(gameObject, destroyDelay);
        }
    }
}
