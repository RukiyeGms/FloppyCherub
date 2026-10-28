using UnityEngine;
using UnityEngine.InputSystem; // Yeni input sistemini ekliyoruz

public class FlappyBirdController : MonoBehaviour
{
    [Header("Zıplama Ayarları")]
    [SerializeField] private float jumpForce = 5f;

    GameManager gameManager;
    bool isDead;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindAnyObjectByType<GameManager>();
        isDead = false;
    }

    void Update()
    {
        // Yeni Input System ile tuş kontrolü (Space tuşu)
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Jump();
        }
    }

    void Jump()
    {
        if (gameManager.CheckFlying())
        {
            if (!isDead) {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                SoundEffectManager.Play("Jump");
            }
            

        }
        else { gameManager.StartFlying(); }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            gameManager.GameOver();
            isDead = true;
            SoundEffectManager.Play("Bump");
        }
    }
}