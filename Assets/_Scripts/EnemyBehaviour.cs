using UnityEngine;
using System;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Boundary speedRange;
    [SerializeField] private Boundary moveRange;
    [SerializeField] private Boundary frameDelayRange;

    private float randomSpeed;
    private float randomMoveDist;
    private int frameDelay;

    [SerializeField] private Boundary battleXPos;
    [SerializeField] private Boundary battleYPos;
    [SerializeField] private float spawnYPos = 6f;
    private Vector2 battlePos;
    private float traveledDist;

    private SpriteRenderer sr;
    private Collider2D col;
    private bool isDead;

    [SerializeField] private Transform bulletSpawn;
    public int score = 100;

    [SerializeField] private GameObject explosionPrefab;

    private void Start()
    {
        randomSpeed = UnityEngine.Random.Range(speedRange.min, speedRange.max);
        randomMoveDist = UnityEngine.Random.Range(moveRange.min, moveRange.max);
        battlePos.x = UnityEngine.Random.Range(battleXPos.min, battleXPos.max);
        battlePos.y = UnityEngine.Random.Range(battleYPos.min, battleYPos.max);
        transform.position = battlePos;

        traveledDist = 0f;

        frameDelay = (int)UnityEngine.Random.Range(frameDelayRange.min, frameDelayRange.max);

        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }
    private void Update()
    {
        if (isDead) return;

        //in a nutshell:
        //randomMoveDist = d, traveledDist = 0, PingPong(d, 2d) - d = 0
        traveledDist = Mathf.Repeat(traveledDist + randomSpeed * Time.deltaTime, randomMoveDist * 4f);
        //the same as above, except the number will grow infinitely
        //with repeat, it wrap itself every whatever value you set (here is 4d)
        //traveledDist += randomSpeed * Time.deltaTime;
        var offset = Mathf.PingPong(traveledDist + randomMoveDist, randomMoveDist * 2f) - randomMoveDist;
        transform.position = new Vector2(battlePos.x + offset, battlePos.y);
    }
    private void FixedUpdate()
    {
        if (!isDead && Time.frameCount % frameDelay == 0)
        {
            BulletManager.Instance.GetBullet(bulletSpawn.position, BulletType.Enemy);
            SoundManager.Instance.PlaySfx(AudioName.SHOOT);
        }
    }

    public event Action<int> OnDied;
    public void TakeHit()
    {
        if (isDead) return;
        isDead = true;
        col.enabled = false;
        SetAlpha(0.5f);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        transform.position = new Vector2(0f, spawnYPos);
        SoundManager.Instance.PlaySfx(AudioName.EXPLODE);
        OnDied?.Invoke(score);
        StartCoroutine(MoveToBattlePos());
    }
    private IEnumerator MoveToBattlePos()
    {
        while ((Vector2)transform.position != battlePos)
        {
            transform.position = Vector2.MoveTowards(transform.position, battlePos, randomSpeed * Time.deltaTime);
            yield return null;
        }
        SetAlpha(1f);
        col.enabled = true;
        traveledDist = 0f;
        isDead = false;
    }
    private void SetAlpha(float a)
    {
        var c = sr.color;
        c.a = a;
        sr.color = c;
    }
}