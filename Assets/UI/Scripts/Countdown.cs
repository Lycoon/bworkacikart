using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [Header("References")]
    public TMPro.TextMeshProUGUI countdownText;
    public AudioSource tickAudioSource;

    [Header("Settings")]
    public float timeLeft;

    private bool countdownEnabled = false;

    void Start()
    {
        countdownEnabled = true;
    }

    void Update()
    {
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

    void UpdateCountdown(float currentTime)
    {
        currentTime++;
        tickAudioSource.Stop();
        tickAudioSource.Play();

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
