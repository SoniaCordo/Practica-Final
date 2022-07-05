using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public Transform[] SpawnPoints;
    public GameObject duckPrefab;

    public Text scoreText, shotsText;

    private int Score, HitShots, clicks;

    public int totalhitShots;

    private int duckCreated;
    private int InitialDucks = 2;

    private int MaxDucks;

    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;

        //StartCoroutine(DuckTime());
    }

    // Update is called once per frame
    private void Update()
    {
        StartCoroutine(CreateDucks(InitialDucks));
        OnMouseDown();
        if (MaxDucks == 3)
        {
            StopCoroutine(CreateDucks(MaxDucks));
        }
    }

    public void OnMouseDown()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            clicks++;
            scoreText.text = Score.ToString("000");
            shotsText.text = HitShots.ToString() + "/" + clicks.ToString();
            //StartCoroutine(CreateDucks(InitialDucks));
        }
    }

    public IEnumerator CreateDucks(int ducks)
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < ducks; i++)
        {
            GameObject duck = Instantiate(duckPrefab, SpawnPoints[Random.Range(0, SpawnPoints.Length)].position, Quaternion.identity);
            duckCreated++;
        }
    }

    public IEnumerator DuckTime()
    {
        yield return new WaitForSeconds(0.2f);
        Duck[] ducks = FindObjectsOfType<Duck>();
        for (int i = 0; i < ducks.Length; i++)
        {
            ducks[i].Time();
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
        duckCreated--;

        if (duckCreated <= 0)
        {
            StopCoroutine(DuckTime());
        }
    }
}