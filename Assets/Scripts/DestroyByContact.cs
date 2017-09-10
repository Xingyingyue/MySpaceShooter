using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
    public GameObject asteroidExplosion;
    public GameObject playerExplosion;
    public int scoreValue;

    private GameControllerScript gameControllerScript;

    private void Start()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameControllerScript = gameController.GetComponent<GameControllerScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.gameObject.transform.position, other.gameObject.transform.rotation);
            gameControllerScript.GameOver();
            Destroy(other.gameObject);
        }
        if (other.tag == "Bullet")
        {
            other.gameObject.SetActive(false);
        }

        Instantiate(asteroidExplosion, gameObject.transform.position, gameObject.transform.rotation);
        gameControllerScript.AddScore(scoreValue);

        gameObject.SetActive(false);
    }
}
