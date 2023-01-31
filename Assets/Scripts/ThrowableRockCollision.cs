using System.Collections.Generic;
using UnityEngine;

public class ThrowableRockCollision : MonoBehaviour
{
    public List<AudioClip> rockHitSounds;
    public ParticleSystem dirtParticleSystem;
    public ParticleSystem grassParticleSystem;
    public float destroyDelay = 3.5f;
    public float destroyTreeDelay = 10f;

    public float directHitDamage = 50f;
    public float aoeHitDamage = 20f;
    public float minDamageVelocity = 10f;
    public float maxDamageVelocity = 50f;

    private bool hitPlayer = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void AOEImpact(Vector3 impactPoint, float velocity)
    {
        Debug.Log("Impact radius: " + velocity);

        Collider[] colliders = Physics.OverlapSphere(impactPoint, velocity);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                float hitDistance = Vector3.Distance(collider.transform.position, impactPoint) - 0.5f;
                // 1 / e(x * 0.05)
                float distanceAttenuation = 1.0f / Mathf.Exp(hitDistance * 0.05f);
                float damage = distanceAttenuation * aoeHitDamage * ComputeDamageScale(rb.velocity.magnitude);
                Debug.Log("distance: " + hitDistance + ", factor: " + 1.0f / Mathf.Exp(hitDistance * 0.1f) + ", damage: " + aoeHitDamage * ComputeDamageScale(rb.velocity.magnitude));
                Debug.Log("AOE hit: " + damage);
                GameManager.Instance.health.TakeDamage((int)damage);
            }
        }
    }

    float ComputeDamageScale(float velocity)
    {
        if (velocity < minDamageVelocity)
            return 0f;
        else if (velocity > maxDamageVelocity)
            return 1f;
        return (velocity - minDamageVelocity) / (maxDamageVelocity - minDamageVelocity);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            dirtParticleSystem.Play();
            Destroy(gameObject, destroyDelay);
            PlayRockHit();

            if (!hitPlayer)
            {
                Debug.Log("Velocity on terrain collision: " + rb.velocity.magnitude);

                ContactPoint contactPoint = collision.GetContact(0);
                AOEImpact(contactPoint.point, rb.velocity.magnitude);
                hitPlayer = true;
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Tree"))
        {
            grassParticleSystem.Play();
            dirtParticleSystem.Play();
            if (collision.transform.parent.gameObject.GetComponent<Rigidbody>() == null)
            {
                collision.transform.parent.gameObject.AddComponent<Rigidbody>();
                Destroy(collision.transform.parent.gameObject, destroyTreeDelay);
            }
            Destroy(gameObject, destroyDelay);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Velocity on player collision: " + rb.velocity.magnitude);

            int damage = (int)(directHitDamage * ComputeDamageScale(rb.velocity.magnitude));
            Debug.Log("Direct hit: " + damage);
            GameManager.Instance.health.TakeDamage(damage);
            Destroy(gameObject, destroyDelay);
        }
    }

    void PlayRockHit()
    {
        int randomIndex = Random.Range(0, rockHitSounds.Count);
        AudioSource.PlayClipAtPoint(rockHitSounds[randomIndex], transform.position);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (transform != null && rb != null)
            Gizmos.DrawWireSphere(transform.position, rb.velocity.magnitude);
    }
}
