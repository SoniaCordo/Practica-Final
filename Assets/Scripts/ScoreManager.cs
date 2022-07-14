using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [SerializeField] private Text scoreText, shotsText, maxScoreText, ScoreEndGame, maxScoreEnd;

    private int Score, HitShots, clicks, totalhitShots, maxScore;
    [SerializeField] private LayerMask GoldDuck;
    public int IncreaseScore;

    private void Start()
    {
        Instance = this;
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

    public IEnumerator HitShot()
    {
        totalhitShots++;
        Score += 10;
        HitShots++;

        yield return new WaitForSeconds(0.2f);
    }
}