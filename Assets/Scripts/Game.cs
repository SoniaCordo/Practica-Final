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

    public Text scoreText, shotsText;

    private int Score, HitShots, clicks;

    public int totalhitShots;

    private int duckCreated;
    private int InitialDucks = 2;
    private float TimeToSpawn = 2;
    private int MaxDucks;

    private void Awake()
    {
    }

    private void Start()
    {
        Instance = this;
        StartCoroutine(CreateDucks());
    }

    private void Update()
    {
        OnMouseDown();

        if (MaxDucks == 4)
        {
            StopCoroutine(CreateDucks());
        }
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

    public IEnumerator CreateDucks()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeToSpawn);
            int randomPosition = Random.Range(0, SpawnPoints.Length);
            Instantiate(duckPrefab, SpawnPoints[randomPosition].position, Quaternion.identity);
            InitialDucks += 2;

            DuckSprite.GetComponent<SpriteRenderer>().flipX = true;
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
        InitialDucks--;
    }
}