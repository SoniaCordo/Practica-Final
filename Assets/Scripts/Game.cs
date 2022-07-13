using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public Transform[] SpawnPoints;

    public GameObject duckPrefab;
    public SpriteRenderer DuckSprite;

    public Text scoreText, shotsText, maxScoreText;

    private int Score, HitShots, clicks, totalhitShots, maxScore;

    private int duckCreated = 0;
    private int InitialDucks = 2;

    private float TimeToSpawn = 1.5f;

    private void Start()
    {
        Instance = this;

        StartCoroutine(CreateDucks());
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

    public void Hit()
    {
        StartCoroutine(HitShot());
    }

    public IEnumerator HitShot()
    {
        totalhitShots++;
        Score += 10;
        HitShots++;
        yield return new WaitForSeconds(0.2f);
    }
}