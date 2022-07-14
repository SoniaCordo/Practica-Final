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
            yield return new WaitForSeconds(Random.Range(2, 5));
            int randomPosition = Random.Range(0, SpawnPoints.Length);
            Instantiate(GoldDuckPrefab, SpawnPoints[randomPosition].position, Quaternion.identity);
            InitialDucks++;
        }
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