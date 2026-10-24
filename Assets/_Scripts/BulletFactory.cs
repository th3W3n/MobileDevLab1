using UnityEngine;

public enum BulletType
{
    Enemy,
    Player
}

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private GameObject playerBullet;

    private void Awake()
    {
        BulletManager.Instance.Init(this);
    }
    public GameObject CreateBullet(BulletType _type = BulletType.Enemy)
    {
        GameObject tempBullet = null;
        switch (_type)
        {
            case BulletType.Enemy:
                tempBullet = Instantiate(enemyBullet);
                break;
            case BulletType.Player:
                tempBullet = Instantiate(playerBullet);
                break;
        }
        tempBullet.transform.SetParent(transform);
        tempBullet.SetActive(false);
        return tempBullet;
    }
}
