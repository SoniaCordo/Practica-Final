using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Duck : MonoBehaviour
{
    public static Duck Instance;
    [SerializeField] private Animator myDuckAnim, myGoldDuckAnim;

    public SpriteRenderer DuckSprite;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject duckPrefab, GoldDuckPrefab;

    [SerializeField] private float x, y, z;
    [SerializeField] private int health;

    private void Start()

    {
        if (Instance == null)
        {
            Instance = this;
        }
        rb.isKinematic = true;
        DuckMovement();
        GoldDuckMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnMouseDown();
    }

    public void OnMouseDown()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (health == 0 && tag == "Duck")
            {
                StartCoroutine(Die());
                AudioManager.Instance.PlayDuckSound();
            }
            else if (health == 0 && tag == "GoldDuck")
            {
                StartCoroutine(DieGold());
                AudioManager.Instance.PlayDuckSound();
            }
            else
            {
                health--;
            }
        }
    }

    public void DuckMovement()
    {
        if ((duckPrefab.transform.position == Game.Instance.SpawnPoints[3].position) || (duckPrefab.transform.position == Game.Instance.SpawnPoints[2].position))
        {
            Vector3 velocity = new Vector3(x, y, z);
            rb.velocity = -velocity;
            Destroy(gameObject, 3.5f);
            DuckSprite.GetComponent<SpriteRenderer>().flipX = true;
        }

        if ((duckPrefab.transform.position == Game.Instance.SpawnPoints[0].position) || (duckPrefab.transform.position == Game.Instance.SpawnPoints[1].position))
        {
            Vector3 velocity = new Vector3(x, y, z);
            rb.velocity = velocity;
            Destroy(gameObject, 3.5f);
            DuckSprite.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void GoldDuckMovement()
    {
        if ((GoldDuckPrefab.transform.position == Game.Instance.SpawnPointsGold[3].position) || (GoldDuckPrefab.transform.position == Game.Instance.SpawnPointsGold[2].position))
        {
            Vector3 velocity = new Vector3(x, y, z);
            rb.velocity = -velocity;
            Destroy(gameObject, 3.5f);
            DuckSprite.GetComponent<SpriteRenderer>().flipX = true;
        }

        if ((GoldDuckPrefab.transform.position == Game.Instance.SpawnPointsGold[0].position) || (GoldDuckPrefab.transform.position == Game.Instance.SpawnPointsGold[1].position))
        {
            Vector3 velocity = new Vector3(x, y, z);
            rb.velocity = velocity;
            Destroy(gameObject, 3.5f);
            DuckSprite.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public IEnumerator Die()
    {
        Game.Instance.Hit();

        myDuckAnim.SetTrigger("Die");
        rb.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(0.4f);

        rb.isKinematic = false;
        Destroy(gameObject, 2.25f);
    }

    public IEnumerator DieGold()
    {
        Game.Instance.HitGold();

        myDuckAnim.SetTrigger("Die");
        rb.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(0.4f);

        rb.isKinematic = false;
        Destroy(gameObject, 2.25f);
    }
}