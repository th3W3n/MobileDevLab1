using System;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    [SerializeField] private Boundary spawnXPos;
    [SerializeField] private float spawnYPos = 6f;
    [SerializeField] private Boundary speedYRange;
    private Vector2 spawnPos;
    private Vector3 randomVelocity;

    [SerializeField] private Boundary rotSpeedRange;
    private float rotSpeed;

    [SerializeField] private Boundary pickupBounds;

    private bool collected;

    private void Start()
    {
        spawnPos.x = UnityEngine.Random.Range(spawnXPos.min, spawnXPos.max);
        spawnPos.y = spawnYPos;
        randomVelocity.y = UnityEngine.Random.Range(speedYRange.min, speedYRange.max);
        transform.position = spawnPos;

        rotSpeed = UnityEngine.Random.Range(rotSpeedRange.min, rotSpeedRange.max);
        if (UnityEngine.Random.value < 0.5f) rotSpeed = -rotSpeed;
    }
    private void Update()
    {
        Move();
        Rotate();
        CheckBounds();
    }

    private void Move()
    {
        transform.position += randomVelocity * Time.deltaTime;
    }
    private void Rotate()
    {
        transform.Rotate(0f, 0f, rotSpeed * Time.deltaTime);
    }
    private void CheckBounds()
    {
        if (transform.position.y < pickupBounds.min || transform.position.y > pickupBounds.max)
            Destroy(gameObject);
    }

    [SerializeField] private int score = 50;
    public event Action<int> OnPickup;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (collected || !col.CompareTag("Player")) return;
            collected = true;

            SoundManager.Instance.PlaySfx(AudioName.PICKUP);
            OnPickup?.Invoke(score);
            Destroy(gameObject);
        }
    }
}
