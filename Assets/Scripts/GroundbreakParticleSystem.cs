using UnityEngine;

public class GroundbreakParticleSystem : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem dirtParticleSystem;
    public ParticleSystem grassParticleSystem;

    void Start()
    {
        dirtParticleSystem.Play();
        grassParticleSystem.Play();

        Destroy(gameObject, 3.5f);
    }
}
