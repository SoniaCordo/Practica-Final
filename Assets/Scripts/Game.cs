using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public Transform[] SpawnPoints;
    public Transform[] SpawnPointsGold;

    [SerializeField] private Text scoreText, shotsText, maxScoreText, ScoreEndGame, maxScoreEnd;

    private int Score, HitShots, clicks, totalhitShots, maxScore;

    [SerializeField] private GameObject duckPrefab;
    [SerializeField] private GameObject GoldDuckPrefab;
    [SerializeField] private GameObject EndGameScore;

    private int duckCreated = 0;
    private int InitialDucks = 2;

    private float TimeToSpawn = 1.5f;
    public Countdown myCountdown;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        StartCoroutine(CreateDucks());
        StartCoroutine(CreateGoldDucks());
        myCountdown.OnTimeIsOver += EndGame;
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
            yield return new WaitForSeconds(Random.Range(2.5f, 7));
            int randomPosition = Random.Range(0, SpawnPoints.Length);
            Instantiate(GoldDuckPrefab, SpawnPointsGold[randomPosition].position, Quaternion.identity);
            InitialDucks++;
        }
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

    public void Hit()
    {
        StartCoroutine(HitShot());
    }

    public void HitGold()
    {
        StartCoroutine(HitShotGold());
    }

    public IEnumerator HitShot()
    {
        totalhitShots++;
        HitShots++;
        Score += 10;

        yield return new WaitForSeconds(0.2f);
    }

    public IEnumerator HitShotGold()
    {
        totalhitShots++;
        HitShots++;
        Score += 30;

        yield return new WaitForSeconds(0.2f);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartMaxScore()
    {
        PlayerPrefs.DeleteAll();
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