using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public Transform[] SpawnPoints;

    [SerializeField] private GameObject duckPrefab;
    [SerializeField] private GameObject GoldDuckPrefab;
    [SerializeField] private GameObject EndGameScore;

    [SerializeField] private Text scoreText, shotsText, maxScoreText, ScoreEndGame, maxScoreEnd;

    private int Score, HitShots, clicks, totalhitShots, maxScore;

    private int duckCreated = 0;
    private int InitialDucks = 2;

    private float TimeToSpawn = 1.5f;
    public Countdown myCountdown;

    private void Start()
    {
        Instance = this;

        StartCoroutine(CreateDucks());
        StartCoroutine(CreateGoldDucks());
        myCountdown.OnTimeIsOver += EndGame;
    }

    private void Update()
    {
        OnMouseDown();
        SaveScore();
        GetScore();
    }

    public void OnMouseDown()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            clicks++;
            scoreText.text = Score.ToString("000");
            ScoreEndGame.text = Score.ToString("000");
            shotsText.text = HitShots.ToString() + "/" + clicks.ToString();
        }
    }

    public void SaveScore()
    {
        maxScore = PlayerPrefs.GetInt("MaxScore");
        if (maxScore < Score)
        {
            maxScore = Score;
            PlayerPrefs.SetInt("MaxScore", maxScore);
        }
    }

    public void GetScore()
    {
        int score = PlayerPrefs.GetInt("MaxScore");

        maxScoreText.text = maxScore.ToString("000");
        maxScoreEnd.text = maxScore.ToString("000");
    }

    public IEnumerator CreateDucks()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeToSpawn);
            int randomPosition = Random.Range(0, SpawnPoints.Length);
            Instantiate(duckPrefab, SpawnPoints[randomPosition].position, Quaternion.identity);
            InitialDucks++;
        }
    }

    public IEnumerator CreateGoldDucks()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 9));
            int randomPosition = Random.Range(0, SpawnPoints.Length);
            Instantiate(GoldDuckPrefab, SpawnPoints[randomPosition].position, Quaternion.identity);
            InitialDucks++;
        }
    }

    public void Hit()
    {
        StartCoroutine(HitShot());
        StartCoroutine(HitGoldShot());
    }

    public IEnumerator HitShot()
    {
        totalhitShots++;
        Score += 10;
        HitShots++;

        yield return new WaitForSeconds(0.2f);
    }

    public IEnumerator HitGoldShot()
    {
        totalhitShots++;
        Score += 30;
        HitShots++;

        yield return new WaitForSeconds(0.2f);
    }

    public void RestarLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        Time.timeScale = Mathf.Approximately(Time.timeScale, 0.0f) ? 1.0f : 0.0f;
    }

    public void EndGame()
    {
        StopAllCoroutines();
        EndGameScore.SetActive(true);
    }
}