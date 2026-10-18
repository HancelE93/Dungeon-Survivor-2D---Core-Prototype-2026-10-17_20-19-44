
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    private Rigidbody2D rd;
    private Animator animator;
    private PlayerHealth playerHealth;

    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public bool puedeMoverse = true;

    private bool facinRight = true;
    private bool isGrounded;

    [Header("Empujón al recibir daño")]
    private bool isKnockedBack = false;
    private float knockbackTimer = 0.25f;
    private float knockbackDuration = 0.25f;

    [Header("Efectos visuales")]
    public GameObject polvoSaltoPrefabs;

    [Header("Temblor de cámara")]
    public CameraShake cameraShake;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();

        if (cameraShake == null && Camera.main != null)
        {
            cameraShake = Camera.main.GetComponent<CameraShake>();
        }

        if (rd == null)
            Debug.LogError("Falta Rigidbody2D en el jugador.");

        if (animator == null)
            Debug.LogError("Falta Animator en el jugador.");

        if (animator != null)
        {
            animator.SetBool("IsJumping", false);
            animator.SetFloat("Speed", 0f);
        }

        if (cameraShake == null)
            Debug.LogWarning("No se encontró CameraShake en la cámara principal.");
    }

    void Update()
    {
        if (rd == null || animator == null)
            return;

        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;
            animator.SetFloat("Speed", 0f);

            if (knockbackTimer <= 0f)
            {
                isKnockedBack = false;
            }
            else
            {
                return;
            }
        }

        float move = puedeMoverse
            ? Input.GetAxisRaw("Horizontal")
            : 0f;

        animator.SetFloat("Speed", Mathf.Abs(move));

        if (move > 0f && !facinRight)
            Flip();
        else if (move < 0f && facinRight)
            Flip();

        if (puedeMoverse &&
            Input.GetKeyDown(KeyCode.Space) &&
            isGrounded)
        {
            rd.linearVelocity =
                new Vector2(rd.linearVelocity.x, 0f);

            rd.AddForce(
                Vector2.up * jumpForce,
                ForceMode2D.Impulse
            );

            isGrounded = false;
            animator.SetBool("IsJumping", true);

            if (polvoSaltoPrefabs != null)
            {
                Instantiate(
                    polvoSaltoPrefabs,
                    transform.position,
                    Quaternion.identity
                );
            }
        }
    }

    void FixedUpdate()
    {
        if (rd == null || isKnockedBack)
            return;

        float move = puedeMoverse
            ? Input.GetAxisRaw("Horizontal")
            : 0f;

        rd.linearVelocity = new Vector2(
            move * moveSpeed,
            rd.linearVelocity.y
        );
    }

    public void StartKnockback()
    {
        isKnockedBack = true;
        knockbackTimer = knockbackDuration;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;

                    if (animator != null)
                        animator.SetBool("IsJumping", false);

                    break;
                }
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(collision.gameObject);
            }

            // Temblor de cámara al tocar al enemigo
            if (cameraShake != null)
            {
                cameraShake.Temblar();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;

            if (animator != null)
                animator.SetBool("IsJumping", true);
        }
    }

    void Flip()
    {
        facinRight = !facinRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }
}
