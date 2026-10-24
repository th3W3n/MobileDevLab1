using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Boundary bulletBounds;
    [SerializeField] private BulletType type;

    private void Update()
    {
        Move();
        CheckBounds();
    }

    private void Move()
    {
        transform.position += velocity * Time.deltaTime;
    }
    private void CheckBounds()
    {
        if (transform.position.y < bulletBounds.min && type == BulletType.Enemy)
            BulletManager.Instance.ReturnBullet(gameObject, type);
        else if (transform.position.y > bulletBounds.max && type == BulletType.Player)
            BulletManager.Instance.ReturnBullet(gameObject, type);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && type == BulletType.Enemy)
        {
            BulletManager.Instance.ReturnBullet(gameObject, type);
            var player = col.GetComponent<PlayerController>();
            player.TakeHit();
        }
        if (col.CompareTag("Enemy") && type == BulletType.Player)
        {
            BulletManager.Instance.ReturnBullet(gameObject, type);
            var enemy = col.GetComponent<EnemyBehaviour>();
            enemy.TakeHit();
        }
    }
}