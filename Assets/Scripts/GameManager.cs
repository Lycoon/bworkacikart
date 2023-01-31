using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    int lastCollectiblePosition = 0;

    [Header("References")]
    public GameObject collectiblePrefab;
    public GameObject collectiblePositions;
    public TMPro.TextMeshProUGUI scoreDisplay;
    public GameObject groundBreakParticlesPrefab;
    public Health health;

    public Transform giant;
    public InputActionProperty locomotionAction;

    [Header("Settings")]
    public int collectibleHealth = 10;
    public float giantRotationAngleStep = 10f;
    private float giantRotateDelay = 0.5f;

    private float lastRotate = 0f;

    private static GameManager instance = null;
    public static GameManager Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        SpawnNewCollectible();
    }

    void SpawnNewCollectible()
    {
        int newPos = Random.Range(0, collectiblePositions.transform.childCount);
        while (newPos == lastCollectiblePosition)
            newPos = Random.Range(0, collectiblePositions.transform.childCount);

        Debug.Log("New collectible spawned at position " + newPos);
        GameObject newCollectible = Instantiate(collectiblePrefab, collectiblePositions.transform.GetChild(newPos).position, Quaternion.identity);

        lastCollectiblePosition = newPos;
    }

    public void PickCollectible()
    {
        health.Heal(collectibleHealth);
        SpawnNewCollectible();
    }

    void Update()
    {
        float rotate = locomotionAction.action.ReadValue<Vector2>().x;
        if (rotate != 0 && Time.time > lastRotate + giantRotateDelay)
        {
            lastRotate = Time.time;
            giant.RotateAround(Vector3.zero, Vector3.up, -rotate * giantRotationAngleStep);
        }
    }
}
