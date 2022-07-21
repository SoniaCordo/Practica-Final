using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public Transform[] SpawnPoints, SpawnPointsGold;

    [SerializeField] private Text scoreText, shotsText, maxScoreText, ScoreEndGame, maxScoreEnd;

    private int Score, HitShots, clicks, maxScore;

    [SerializeField] private GameObject duckPrefab, GoldDuckPrefab;

    [SerializeField] private GameObject EndGameScore, MenuPause, ButtonPausa, ButtonReload, ButtonReloadMS;

    private float TimeToSpawn = 1.25f;
    public Countdown myCountdown;

    [SerializeField] private Collider2D Grid;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        StartCoroutine(CreateDucks());
        StartCoroutine(CreateGoldDucks());
        myCountdown.OnTimeIsOver += EndGame;
        AudioManager.Instance.PlayAmbientalMusic();
    }

    public IEnumerator CreateDucks()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeToSpawn);
            int randomPosition = Random.Range(0, SpawnPoints.Length);
            Instantiate(duckPrefab, SpawnPoints[randomPosition].position, Quaternion.identity);
        }
    }

    public IEnumerator CreateGoldDucks()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2.5f, 3.75f));
            int randomPosition = Random.Range(0, SpawnPointsGold.Length);
            Instantiate(GoldDuckPrefab, SpawnPointsGold[randomPosition].position, Quaternion.identity);
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
        if (Input.GetButtonDown("Fire1") && Time.timeScale == 1.0f)
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

    public IEnumerator HitShot()
    {
        HitShots++;
        Score += 10;

        yield return new WaitForSeconds(0.2f);
    }

    public void HitGold()
    {
        StartCoroutine(HitShotGold());
    }

    public IEnumerator HitShotGold()
    {
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
        if (Time.timeScale == 0.0f)
        {
            AudioManager.Instance.AmbientalMusic.Pause();
            MenuPause.SetActive(true);
            ButtonPausa.SetActive(false);
            ButtonReload.SetActive(false);
            ButtonReloadMS.SetActive(false);
        }
        else
        {
            AudioManager.Instance.PlayAmbientalMusic();
            MenuPause.SetActive(false);
            ButtonPausa.SetActive(true);
            ButtonReload.SetActive(true);
            ButtonReloadMS.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NotClickButton()
    {
        clicks--;
    }

    public void EndGame()
    {
        StopAllCoroutines();
        EndGameScore.SetActive(true);
        AudioManager.Instance.AmbientalMusic.Stop();
        AudioManager.Instance.PlayEndGameMusic();
    }
}