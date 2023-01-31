using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("References")]
    public Slider healthSlider;
    public SpiderAnimation spiderAnimation;
    public GameObject gameOverScreen;

    [Header("Settings")]
    public float maxHealth = 100;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        healthSlider.value = currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            spiderAnimation.SetDead(true);
            ToggleGameOverScreen();
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    IEnumerator ToggleGameOverScreen()
    {
        yield return new WaitForSeconds(2.5f);
        gameOverScreen.SetActive(true);
    }
}
