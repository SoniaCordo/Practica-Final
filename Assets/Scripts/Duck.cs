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
    private Vector3 positioned;

    public Vector3 direction;

    private void Start()
    {
        myDuckAnim = GetComponent<Animator>();
        DuckSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
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
            //StartCoroutine(IsKinematic());
        }
    }

    public void DuckMovement()
    {
        rb.velocity = transform.right;
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
        rb.isKinematic = false;
        yield return new WaitForSeconds(0.4f);
        isFalling = true;

        Destroy(gameObject, 2);
    }

    public void Time()
    {
        Speed *= 2;
        positioned = transform.position + new Vector3(0, 20, 0);
    }

    //public void FlipCharacter()
    //{
    //    if (Game.Instance.SpawnPoints)
    //    {
    //        transform.rotation = Quaternion.identity;
    //    }
    //    else if (dirX < 0)
    //    {
    //        transform.rotation = Quaternion.Euler(0, 180, 0);
    //    }
    //}
}