using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Queue<GameObject> bulletPool;
    [SerializeField] private int bulletNum = 50;
    [SerializeField] private GameObject bulletPrefab;

    private void Start()
    {
        bulletPool = new();
        BuildBulletPool();
    }

    private void BuildBulletPool()
    {
        for (int i = 0; i < bulletNum; i++)
            AddBullet();
    }
    public GameObject GetBullet(Vector3 spawnPos)
    {
        if (bulletPool.Count < 1)
        {
            AddBullet();
            bulletNum++;
        }
        var bullet = bulletPool.Dequeue();
        bullet.transform.position = spawnPos;
        bullet.SetActive(true);
        return bullet;
    }
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
    private void AddBullet()
    {
        var tempBullet = Instantiate(bulletPrefab);
        tempBullet.SetActive(false);
        tempBullet.transform.SetParent(transform);
        bulletPool.Enqueue(tempBullet);
    }
}