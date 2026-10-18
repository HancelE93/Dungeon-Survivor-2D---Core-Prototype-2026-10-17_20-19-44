
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 2f;

    [Header("Detección")]
    public Transform groundCheck;
    public float distanceToGround = 1f;
    public float distanceToWall = 0.5f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento horizontal
        rb.linearVelocity = new Vector2(
            movingRight ? speed : -speed,
            rb.linearVelocity.y
        );

        // Dirección del enemigo
        Vector2 direction = movingRight
            ? Vector2.right
            : Vector2.left;

        // Comprobar el suelo delante
        RaycastHit2D groundAhead = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            distanceToGround,
            groundLayer
        );

        // Comprobar una pared delante
        RaycastHit2D wallAhead = Physics2D.Raycast(
            transform.position,
            direction,
            distanceToWall,
            groundLayer
        );

        // Cambiar de dirección
        if (groundAhead.collider == null ||
            wallAhead.collider != null)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
