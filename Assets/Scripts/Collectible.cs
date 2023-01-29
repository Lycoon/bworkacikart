using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem particles;
    public GameObject collectibleMesh;
    public SphereCollider sphereCollider;
    public Light pointLight;
    public AudioSource audioSource;

    [Header("Settings")]
    public float amplitude = 0.5f;
    public float frequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Start() 
    {
        posOffset = transform.position;
    }

    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude + 1.2f;

        transform.position = tempPos;
    }

    public void Collect()
    {
        particles.Play();
        audioSource.Play();

        collectibleMesh.SetActive(false);
        pointLight.enabled = false;
        sphereCollider.enabled = false; // otherwise the player can collect it again

        Destroy(gameObject, 2.5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector3.up * 35f);
    }
}
