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
        //if (duckPrefab.transform.position == Game.Instance.SpawnPoints[3].position)
        //{
        //    Vector3 velocity = new Vector3(x, y, z);
        //    rb.velocity = -velocity;
        //    Destroy(gameObject, 3.5f);
        //    DuckSprite.GetComponent<SpriteRenderer>().flipX = true;
        //}
        //if (duckPrefab.transform.position == Game.Instance.SpawnPoints[2].position)
        //{
        //    Vector3 velocity = new Vector3(x, y, z);
        //    rb.velocity = -velocity;
        //    Destroy(gameObject, 3.5f);
        //    DuckSprite.GetComponent<SpriteRenderer>().flipX = true;
        //}
        //if (duckPrefab.transform.position == Game.Instance.SpawnPoints[0].position)
        //{
        //    Vector3 velocity = new Vector3(x, y, z);
        //    rb.velocity = velocity;
        //    Destroy(gameObject, 3.5f);
        //    DuckSprite.GetComponent<SpriteRenderer>().flipX = false;
        //}
        //if (duckPrefab.transform.position == Game.Instance.SpawnPoints[1].position)
        //{
        //    Vector3 velocity = new Vector3(x, y, z);
        //    rb.velocity = velocity;
        //    DuckSprite.GetComponent<SpriteRenderer>().flipX = false;
        //    Destroy(gameObject, 3.5f);
        //}
    }

    public void GoldDuckMovement()
    {
        if (rb != null && GoldDuckPrefab.transform.position == Game.Instance.SpawnPointsGold[3].position)
        {
            Vector3 velocity = new Vector3(x, y, z);
            rb.velocity = -velocity;
            Destroy(gameObject, 3.5f);
            DuckSprite.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (rb != null && GoldDuckPrefab.transform.position == Game.Instance.SpawnPointsGold[2].position)
        {
            Vector3 velocity = new Vector3(x, y, z);
            rb.velocity = -velocity;
            Destroy(gameObject, 3.5f);
            DuckSprite.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (rb != null && GoldDuckPrefab.transform.position == Game.Instance.SpawnPointsGold[0].position)
        {
            Vector3 velocity = new Vector3(x, y, z);
            rb.velocity = velocity;
            Destroy(gameObject, 3.5f);
            DuckSprite.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (rb != null && GoldDuckPrefab.transform.position == Game.Instance.SpawnPointsGold[1].position)
        {
            Vector3 velocity = new Vector3(x, y, z);
            rb.velocity = velocity;
            DuckSprite.GetComponent<SpriteRenderer>().flipX = false;
            Destroy(gameObject, 3.5f);
        }
    }

    public IEnumerator Die()
    {
        Game.Instance.Hit();

        myDuckAnim.SetTrigger("Die");

        yield return new WaitForSeconds(0.4f);

        rb.isKinematic = false;
        Destroy(gameObject, 2.25f);
    }

    public IEnumerator DieGold()
    {
        Game.Instance.HitGold();

        myDuckAnim.SetTrigger("Die");

        yield return new WaitForSeconds(0.4f);

        rb.isKinematic = false;
        Destroy(gameObject, 2.25f);
    }
}