
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ajustes de Ataque")]
    public Transform attackPoint;
    public float attackRange = 0.8f;
    public LayerMask enemyLayer;
    public int attackDamage = 1;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("No se encontró el Animator del jugador.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) ||
            Input.GetMouseButtonDown(0))
        {
            attackDamage = 1;
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            attackDamage = 2;
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            attackDamage = 3;
            Attack();
        }
    }

    void Attack()
    {
        if (attackPoint == null)
        {
            Debug.LogWarning(
                "No has asignado el Attack Point en el Inspector."
            );
            return;
        }

        // Activar la animación de ataque
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Detectar enemigos dentro del rango
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer
        );

        Debug.Log("Enemigos detectados: " + hitEnemies.Length);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth =
                enemy.GetComponentInParent<EnemyHealth>();

            if (enemyHealth != null)
            {
                Debug.Log("¡Estoy golpeando al enemigo!");
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            attackPoint.position,
            attackRange
        );
    }
}
