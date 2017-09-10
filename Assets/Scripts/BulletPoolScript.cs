using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolScript : MonoBehaviour {
    public static BulletPoolScript bulletPoolInstance;
    public GameObject prefab;
    public int poolAmount = 30;

    private List<GameObject> bulletList;
    private int currentIndex = 0;

    private void Awake()
    {
        bulletPoolInstance = this;
    }

    private void Start()
    {
        bulletList = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject bullet = Instantiate(prefab);
            bullet.SetActive(false);
            bulletList.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        for (int i = 0; i < bulletList.Count; ++i)
        {
            int index = (currentIndex + i) % bulletList.Count;
            if (!bulletList[index].activeInHierarchy)
            {
                currentIndex = (index + 1) % bulletList.Count;
                return bulletList[index];             
            }
        }

        GameObject bullet = Instantiate(prefab);
        bulletList.Add(bullet);
        poolAmount++;
        Debug.Log("all bullet are used,now create new one,amount is" + poolAmount);
        return bullet;
    }
}
