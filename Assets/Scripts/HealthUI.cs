
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public GameObject[] hearts;
    public PlayerHealth playerHealth;

    private int previousHealth = -1;

    void Start()
    {
        if (playerHealth == null)
        {
            playerHealth = FindAnyObjectByType<PlayerHealth>();
        }

        if (playerHealth == null)
        {
            Debug.LogError("No se encontró PlayerHealth.");
            return;
        }

        UpdateHearts();
    }

    void Update()
    {
        if (playerHealth == null)
            return;

        if (previousHealth != playerHealth.CurrentHealth)
        {
            UpdateHearts();
        }
    }

    void UpdateHearts()
    {
        if (playerHealth == null || hearts == null)
            return;

        int health = playerHealth.CurrentHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
            {
                hearts[i].SetActive(i < health);
            }
        }

        previousHealth = health;

        Debug.Log("Corazones actualizados. Vidas: " + health);
    }
}
