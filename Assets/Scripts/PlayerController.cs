
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd;
    private SpriteRenderer spriteRenderer;

    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private bool isGrounded = false;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // Volteo visual
        float move = Input.GetAxis("Horizontal");

        if (spriteRenderer != null)
        {
            if (move > 0)
                spriteRenderer.flipX = false;
            else if (move < 0)
                spriteRenderer.flipX = true;
        }
    }

    void FixedUpdate()
    {
        // Movimiento horizontal
        float move = Input.GetAxis("Horizontal");

        rd.linearVelocity = new Vector2(
            move * moveSpeed,
            rd.linearVelocity.y
        );
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
