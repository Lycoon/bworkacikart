using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int lastCollectiblePosition = 0;

    [Header("References")]
    public GameObject collectiblePrefab;
    public GameObject collectiblePositions;
    public TMPro.TextMeshProUGUI scoreDisplay;
    public GameObject groundBreakParticlesPrefab;

    [Header("Game Settings")]
    public int score = 0;

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
        AddToScore(1);
        SpawnNewCollectible();
    }

    public void AddToScore(int amount)
    {
        score += amount;
        scoreDisplay.text = "Score " + score.ToString();
    }
}
