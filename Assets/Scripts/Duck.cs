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

    private float dirX = 0f;

    private void Start()
    {
        myDuckAnim = GetComponent<Animator>();
        DuckSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Die());
            //Kinematic();
            StartCoroutine(IsKinematic());
        }
        else
        {
        }
        FlipCharacter();
    }

    public IEnumerator IsKinematic()
    {
        yield return new WaitForSeconds(1f);
        rb.isKinematic = false;
    }

    //public void Kinematic()
    //{
    //    rb.isKinematic = false;
    //}

    public IEnumerator Die()
    {
        Game.Instance.Hit();
        isDead = true;
        myDuckAnim.SetTrigger("Die");
        yield return new WaitForSeconds(0.4f);
        isFalling = true;
        Destroy(gameObject, 3);
    }

    public void Time()
    {
        Speed *= 2;
        positioned = transform.position + new Vector3(0, 20, 0);
    }

    public void FlipCharacter()
    {
        if (dirX > 0)
        {
            transform.rotation = Quaternion.identity;
        }
        else if (dirX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}