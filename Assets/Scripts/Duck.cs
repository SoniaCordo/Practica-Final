using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Duck : MonoBehaviour
{
    public static Duck Instance;
    [SerializeField] private Animator myDuckAnim;
    public SpriteRenderer DuckSprite;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject duckPrefab;

    [SerializeField] private float Speed;
    [SerializeField] private float x = 10, y, z;

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
            DuckSprite.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (rb != null && duckPrefab.transform.position == Game.Instance.SpawnPoints[2].position)
        {
            Vector3 velocity = new Vector3(x, y, z);
            rb.velocity = -velocity;
            Destroy(gameObject, 3.5f);
            DuckSprite.GetComponent<SpriteRenderer>().flipX = true;
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

        myDuckAnim.SetTrigger("Die");

        yield return new WaitForSeconds(0.4f);

        rb.isKinematic = false;
        Destroy(gameObject, 2.25f);
    }
}