
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public int CurrentHealth => currentHealth;

    private Animator animator;

    void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(GameObject enemy)
    {
        if (currentHealth <= 0)
            return;

        currentHealth--;

        Debug.Log("Vidas restantes: " + currentHealth);

        if (animator != null)
            animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
