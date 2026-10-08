using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Boundary bulletBounds;
    private BulletManager bulletManager;

    private void Start() => bulletManager = FindAnyObjectByType<BulletManager>();
    private void Update()
    {
        Move();
        CheckBounds();
    }

    private void Move()
    {
        transform.position -= new Vector3(0f, speed, 0f) * Time.deltaTime;
    }
    private void CheckBounds()
    {
        if (transform.position.y < bulletBounds.min)
            bulletManager.ReturnBullet(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            bulletManager.ReturnBullet(gameObject);
            SceneManager.LoadScene("End");
        }
    }
}