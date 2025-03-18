using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded2;
    private Rigidbody2D rb;
    private Animator animator;

    private bool isJumping2; // Флаг, чтобы отслеживать, прыгает ли персонаж

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isJumping2 = false;
    }

    void Update()
    {
        Move();
        Jump();
        UpdateAnimation();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Отзеркаливание персонажа в зависимости от направления движения
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Лицом вправо
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Лицом влево
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded2)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping2 = true; // Устанавливаем флаг при прыжке
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded2 = true;
            isJumping2 = false; // Сбрасываем флаг при приземлении
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded2 = false;
        }
    }

    void UpdateAnimation()
    {
        if (isGrounded2)
        {
            if (rb.velocity.x != 0)
            {
                animator.SetBool("isRunning2", true);
                animator.SetBool("isJumping2", false);
                Debug.Log("Running");
            }
            else
            {
                animator.SetBool("isRunning2", false);
                animator.SetBool("isJumping2", false);
                Debug.Log("Idle");
            }
        }
        else
        {
            if (isJumping2) // Проверяем флаг, чтобы воспроизвести анимацию прыжка
            {
                animator.SetBool("isJumping2", true);
                Debug.Log("Jumping");
            }
        }
    }
}