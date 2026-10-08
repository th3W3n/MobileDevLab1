using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Boundary speedRange;
    [SerializeField] private Boundary moveRange;
    [SerializeField] private Boundary startingRange;
    [SerializeField] private Boundary frameDelayRange;

    private float randomSpeedRange;
    private float randomMoveRange;
    private float startingPoint;
    private int frameDelay;

    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;

    private BulletManager bulletManager;

    private void Start()
    {
        bulletManager = FindAnyObjectByType<BulletManager>();

        randomSpeedRange = Random.Range(speedRange.min, speedRange.max);
        randomMoveRange = Random.Range(moveRange.min, moveRange.max);
        startingPoint = Random.Range(startingRange.min, startingRange.max);
        transform.position = new Vector2(startingPoint, transform.position.y);

        frameDelay = (int)Random.Range(frameDelayRange.min, frameDelayRange.max);
    }
    private void Update()
    {
        var offset = Mathf.PingPong
        (Time.time * randomSpeedRange, randomMoveRange * 2f) - randomMoveRange;
        transform.position = new Vector2(startingPoint + offset, transform.position.y);
    }
    private void FixedUpdate()
    {
        if (Time.frameCount % frameDelay == 0)
            bulletManager.GetBullet(bulletSpawn.position);
    }
}