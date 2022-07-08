using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    public Animator myDuckAnim;
    public SpriteRenderer DuckSprite;
    public Rigidbody2D rb;

    private bool isDead;
    private bool isFalling;

    public float Speed;
    public float x = 10;
    public float y;
    public float z;

    private Vector3 positioned;

    public Vector3 direction;

    private void Start()
    {
        myDuckAnim = GetComponent<Animator>();
        DuckSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        DuckMovement();
    }

    private void Update()
    {
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
            //StartCoroutine(IsKinematic());
        }
    }

    public void DuckMovement()
    {
        //rb.velocity = transform.right;
        if (rb != null)
        {
            //rb.AddForce(positioned.right * force, ForceMode2D.Force);
            Vector3 velocity = new Vector3(x, y, z);
            rb.velocity = velocity;
            Destroy(gameObject, 3);
        }
    }

    //public IEnumerator IsKinematic()
    //{
    //    yield return new WaitForSeconds(1);
    //    rb.isKinematic = false;
    //}

    public IEnumerator Die()
    {
        Game.Instance.Hit();
        isDead = true;

        myDuckAnim.SetTrigger("Die");
        //StartCoroutine(IsKinematic());
        yield return new WaitForSeconds(0.4f);
        isFalling = true;
        rb.isKinematic = false;
        Destroy(gameObject, 0.5f);
    }

    public void Time()
    {
        Speed *= 2;
        positioned = transform.position + new Vector3(0, 20, 0);
    }
}