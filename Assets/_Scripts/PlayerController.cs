using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forceH = 50f;
    [Range(0f, 1f)] public float decay = 0.2f;
    private float inputX;

    [SerializeField] private Boundary bounds;

    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private int frameDelay = 10;

    [SerializeField] private float invincibleTime = 2f;
    [SerializeField] private float blinkInterval = 0.1f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D col;
    private Vector2 spawnPos;
    private bool isDead;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        spawnPos = rb.position;
    }
    private void Update()
    {
        inputX = GetTouchDir();
    }
    private void FixedUpdate()
    {
        if (!isDead) Move();
        CheckBounds();

        if (!isDead && Time.frameCount % frameDelay == 0)
            BulletManager.Instance.GetBullet(bulletSpawn.position, BulletType.Player);
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

    public event Action OnDied;
    public void TakeHit()
    {
        if (isDead) return;
        isDead = true;
        col.enabled = false;
        rb.linearVelocity = Vector2.zero;
        sr.enabled = false;
        OnDied?.Invoke();
    }
    public void Respawn()
    {
        rb.position = spawnPos;
        rb.linearVelocity = Vector2.zero;
        sr.enabled = true;
        isDead = false;
        StartCoroutine(InvincibleRoutine());
    }
    private IEnumerator InvincibleRoutine()
    {
        float end = Time.time + invincibleTime;
        while (Time.time < end)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
        sr.enabled = true;
        col.enabled = true;
    }
}