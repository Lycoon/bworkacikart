using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    int lastCollectiblePosition = 0;

    [Header("Prefabs")]
    public GameObject collectiblePrefab;
    public GameObject collectiblePositions;
    public GameObject groundBreakParticlesPrefab;
    public AudioSource tickAudioSource;
    public Health health;

    [Header("Countdown settings")]
    public TMPro.TextMeshProUGUI countdownText;
    public float timeLeft;

    [Header("Giant settings")]
    public Transform giant;
    public InputActionProperty locomotionAction;

    [Header("Settings")]
    public float collectibleHealth = 10f;
    public float giantRotationAngleStep = 10f;
    private float giantRotateDelay = 0.5f;

    private float lastRotate = 0f;
    private bool countdownEnabled = false;
    private bool isNewSecond = false;

    // Singleton
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
        countdownEnabled = true;
        SpawnNewCollectible();
        InvokeRepeating("PlayTickSound", 0f, 1f);
    }

    void Update()
    {
        // Giant rotation
        float rotate = locomotionAction.action.ReadValue<Vector2>().x;
        if (rotate != 0 && Time.time > lastRotate + giantRotateDelay)
        {
            lastRotate = Time.time;
            giant.RotateAround(Vector3.zero, Vector3.up, -rotate * giantRotationAngleStep);
        }
        
        // Countdown
        if (countdownEnabled)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateCountdown(timeLeft);
            }
            else
            {
                timeLeft = 0;
                countdownEnabled = false;
            }
        }
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

    void UpdateCountdown(float currentTime)
    {
        currentTime++;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void PlayTickSound()
    {
        tickAudioSource.Play();
    }
}
