using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Duck : MonoBehaviour
{
    public static Duck Instance;
    public Animator myDuckAnim;
    public SpriteRenderer DuckSprite;
    public Rigidbody2D rb;
    public GameObject duckPrefab;

    private bool isDead;
    private bool isFalling;

    public float Speed;
    public float x = 10;
    public float y;
    public float z;

    private void Start()

    {
        Instance = this;
        rb.isKinematic = true;
        DuckMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnMouseDown();
    }

    public void OnMouseDown()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Die());
        }
    }

    public void DuckMovement()
    {
        if (rb != null && duckPrefab.transform.position == Game.Instance.SpawnPoints[3].position)
        {
            Vector3 velocity = new Vector3(x, y, z);
            rb.velocity = -velocity;
            Destroy(gameObject, 3.5f);
        }
        if (rb != null && duckPrefab.transform.position == Game.Instance.SpawnPoints[2].position)
        {
            Vector3 velocity = new Vector3(x, y, z);
            rb.velocity = -velocity;
            Destroy(gameObject, 3.5f);
        }
        if (rb != null && duckPrefab.transform.position == Game.Instance.SpawnPoints[0].position)
        {
            Vector3 velocity = new Vector3(x, y, z);
            rb.velocity = velocity;
            Destroy(gameObject, 3.5f);
            DuckSprite.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (rb != null && duckPrefab.transform.position == Game.Instance.SpawnPoints[1].position)
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
        isDead = true;

        myDuckAnim.SetTrigger("Die");

        yield return new WaitForSeconds(0.4f);
        isFalling = true;
        rb.isKinematic = false;
        Destroy(gameObject, 1f);
    }
}