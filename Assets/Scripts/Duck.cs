using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    public static Duck Instance;
    public Animator myDuckAnim;
    public SpriteRenderer DuckSprite;
    public Rigidbody2D rb;

    private bool isDead;
    private bool isFalling;

    public float Speed;
    public float x = 10;
    public float y;
    public float z;

    private void Start()

    {
        Instance = this;
        //myDuckAnim = GetComponent<Animator>();
        //DuckSprite = GetComponent<SpriteRenderer>();
        //rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        DuckMovement();
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnMouseDown();
        if (collision.gameObject.tag == "Duck")
        {
            DuckSprite.GetComponent<SpriteRenderer>().flipX = true;
        }
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
        if (rb != null)
        {
            Vector3 velocity = new Vector3(x, y, z);
            rb.velocity = -velocity;
            Destroy(gameObject, 2.5f);
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