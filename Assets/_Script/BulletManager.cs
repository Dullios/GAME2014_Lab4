using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    public GameObject bullet;
    public int maxBullets;

    private Queue<GameObject> bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        BuildBulletPool();
    }


    private void BuildBulletPool()
    {
        // Creates empty queue structure
        bulletPool = new Queue<GameObject>();

        for(int count = 0; count < maxBullets; count++)
        {
            GameObject tempBullet = Instantiate(bullet, gameObject.transform);
            tempBullet.SetActive(false);
            bulletPool.Enqueue(tempBullet);
        }
    }

    public GameObject GetBullet(Vector3 position)
    {
        GameObject newBullet = bulletPool.Dequeue();
        newBullet.SetActive(true);
        newBullet.transform.position = position;

        return newBullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
