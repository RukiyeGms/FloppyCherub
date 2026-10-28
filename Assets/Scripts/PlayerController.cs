using UnityEngine;
using UnityEngine.InputSystem; // Yeni input sistemini ekliyoruz

public class FlappyBirdController : MonoBehaviour
{
    [Header("Zıplama Ayarları")]
    [SerializeField] private float jumpForce = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Yeni Input System ile tuş kontrolü (Space tuşu veya Farenin Sol Tıkı)
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame ||
            Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
}