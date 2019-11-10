using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  public GameObject hazard;
  public Vector3 spawnValues;
  public int hazardCount;
  public float spawnWait;
  public float startWait;
  public float waveWait;

  public Text ScoreText;
  public Text restartText;
  public Text gameOverText;

  private int score;
  private bool gameOver;
  private bool restart;

  void Start()
  {
    score = 0;
    gameOver = false;
    restart = false;
    restartText.text = "";
    gameOverText.text = "";
    UpdateScore();
    StartCoroutine(SpawnWaves());
  }

  void Update()
  {
      if (Input.GetKey("escape"))
      {
        Application.Quit();
      }

      if (restart)
      {
        if (Input.GetKeyDown (KeyCode.R))
        {
          SceneManager.LoadScene("Scene1");
        }
      }
  }

  IEnumerator SpawnWaves()
  {
    yield return new WaitForSeconds(startWait);
    while (true)
    {
      for (int i = 0; i < hazardCount; i++)
      {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(hazard, spawnPosition, spawnRotation);
        yield return new WaitForSeconds(spawnWait);
      }
      yield return new WaitForSeconds(waveWait);
      if (gameOver)
      {
        restartText.text = "Press R for Restart";
        restart = true;
        break;
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
    ScoreText.text = "Score: " + score;
  }

  public void GameOver()
  {
    gameOverText.text = "Game Over!";
    gameOver = true;
  }
}
