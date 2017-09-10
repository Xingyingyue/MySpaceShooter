using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour {
    public Text scoreText;
    public int score;
    public Text gameOverText;
    public Vector3 spawnValues;
    public Text reStartText;

    private Vector3 spawnPosition = Vector3.zero;
    private Quaternion spawnRotation;
    private int asteroidAmount = 10;
    private float spawnWait = 0.5f;
    private float startWait = 1.0f;
    private bool gameOver;
    private bool reStart;

    private readonly string textPrefix = "得分:";
    private readonly string textOfGameOver = "游戏结束";
    private readonly string textOfReStart = "按[R]重新开始";

    private void Start()
    {
        score = 0;
        gameOver = false;
        gameOverText.text = "";
        reStartText.text = "";
        reStart = false;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (reStart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main",LoadSceneMode.Single);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            if (gameOver)
            {
                reStartText.text = textOfReStart;
                reStart = true;
                break;
            }

            for (int i = 0; i < asteroidAmount; i++)
            {
                spawnPosition.x = Random.Range(-spawnValues.x, spawnValues.x);
                spawnPosition.z = spawnValues.z;
                spawnRotation = Quaternion.identity;

                GameObject asteroid = AsteroidPoolScript.asteroidPoolInstance.GetAsteroid();
                if (asteroid != null)
                {
                    asteroid.SetActive(true);
                    asteroid.transform.position = spawnPosition;
                    asteroid.transform.rotation = spawnRotation;
                }
                yield return new WaitForSeconds(spawnWait);
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = textPrefix + score;
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = textOfGameOver;
    }
}
