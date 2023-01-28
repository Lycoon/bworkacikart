using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem particles;
    public GameObject collectibleMesh;

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
        collectibleMesh.SetActive(false);

        Destroy(gameObject, 2.5f);
    }
}
