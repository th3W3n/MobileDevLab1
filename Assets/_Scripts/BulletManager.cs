using System.Collections.Generic;
using UnityEngine;

public class BulletManager
{
    private static BulletManager instance = null;
    public static BulletManager Instance
    {
        get
        {
            if (instance == null)
                instance = new();
            return instance;
        }
    }

    private Queue<GameObject> bulletPoolEnemy, bulletPoolPlayer;
    private int bulletNumEnemy = 50;
    private int bulletNumPlayer = 30;

    private BulletFactory factory;

    public void Init(BulletFactory _factory)
    {
        factory = _factory;
        bulletPoolEnemy = new();
        bulletPoolPlayer = new();
        BuildBulletPool();
    }

    private void BuildBulletPool()
    {
        for (int i = 0; i < bulletNumEnemy; i++)
            AddBullet(BulletType.Enemy);
        for (int i = 0; i < bulletNumPlayer; i++)
            AddBullet(BulletType.Player);
    }
    public GameObject GetBullet(Vector3 _spawnPos, BulletType _type)
    {
        Queue<GameObject> bulletPool = null;
        switch (_type)
        {
            case BulletType.Enemy:
                bulletPool = bulletPoolEnemy;
                break;
            case BulletType.Player:
                bulletPool = bulletPoolPlayer;
                break;
        }

        if (bulletPool.Count < 1)
        {
            AddBullet(_type);
            switch (_type)
            {
                case BulletType.Enemy:
                    bulletNumEnemy++; break;
                case BulletType.Player:
                    bulletNumPlayer++; break;
            }
        }
        var bullet = bulletPool.Dequeue();
        bullet.transform.position = _spawnPos;
        bullet.SetActive(true);
        return bullet;
    }
    public void ReturnBullet(GameObject bullet, BulletType _type)
    {
        bullet.SetActive(false);
        switch (_type)
        {
            case BulletType.Enemy:
                bulletPoolEnemy.Enqueue(bullet);
                break;
            case BulletType.Player:
                bulletPoolPlayer.Enqueue(bullet);
                break;
        }
    }
    private void AddBullet(BulletType _type)
    {
        /*var tempBullet = Instantiate(bulletPrefab);
        tempBullet.SetActive(false);
        tempBullet.transform.SetParent(transform);
        bulletPool.Enqueue(tempBullet);*/
        var tempBullet = factory.CreateBullet(_type);
        switch (_type)
        {
            case BulletType.Enemy:
                bulletPoolEnemy.Enqueue(tempBullet);
                break;
            case BulletType.Player:
                bulletPoolPlayer.Enqueue(tempBullet);
                break;
        }
    }
}