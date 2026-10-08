using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forceH = 50f;

    private Rigidbody2D rb;
    private float inputX;
    [Range(0f, 1f)] public float decay = 0.2f;

    [SerializeField] private Boundary bounds;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        inputX = GetTouchDir();
    }
    private void FixedUpdate()
    {
        Move();
        CheckBounds();
    }

    private float GetTouchDir()
    {
        if (Touchscreen.current == null) return 0f;

        var touch = Touchscreen.current.primaryTouch;
        if (!touch.press.isPressed) return 0f;
        Vector2 pos = touch.position.ReadValue();
        if (pos.x < Screen.width / 2) return -1f;
        return 1f;
    }
    private void Move()
    {
        rb.AddForce(new Vector2(inputX * forceH, 0f));
        rb.linearVelocity *= 1 - decay;
    }
    private void CheckBounds()
    {
        if (transform.position.x < bounds.min)
            transform.position = new Vector2(bounds.min, transform.position.y);
        else if (transform.position.x > bounds.max)
            transform.position = new Vector2(bounds.max, transform.position.y);
    }
}