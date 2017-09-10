using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPoolScript : MonoBehaviour {
    public static AsteroidPoolScript asteroidPoolInstance;
    public GameObject prefab;
    public int asteroidAmount = 10;

    private List<GameObject> asteroidList;
    private int currentIndex = 0;

    private void Awake()
    {
        if (asteroidPoolInstance == null)
        {
            asteroidPoolInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        asteroidList = new List<GameObject>();
        for (int i = 0; i < asteroidAmount; i++)
        {
            GameObject asteroid = Instantiate(prefab);
            asteroid.SetActive(false);
            asteroidList.Add(asteroid);
        }
    } 

    public GameObject GetAsteroid()
    {
        for (int i = 0; i < asteroidList.Count; ++i)
        {
            int index = (currentIndex + i) % asteroidList.Count;
            if (!asteroidList[index].activeInHierarchy)
            {
                currentIndex = (index + 1) % asteroidList.Count;
                return asteroidList[index];
            }
        }

        GameObject asteroid = Instantiate(prefab);
        asteroidList.Add(asteroid);
        asteroidAmount++;
        Debug.Log("all asteroid are used,now create new one,amount is" + asteroidAmount);
        return asteroid;
    }
}
