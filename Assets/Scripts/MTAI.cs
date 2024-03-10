using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class MTAI : Enemy
{
    private Rigidbody2D rb;
    private BoxCollider2D myCollider;

    [SerializeField] private float shootDuration = 2f;
    [SerializeField] private float shootCD = 2f;
    [SerializeField] private float bulletCD = 0.2f;

    private bool isShooting;
    private float currShootDuration = 2f;
    private float currShootCD = 2f;
    private float currBulletCD = 0.2f;
    private Vector2 baseSpeed = new Vector2(6f, 0f);
    [SerializeField] private GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        health = 400;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        // shoot when cooldown complete
        if (currShootCD <= 0) {
            isShooting = true;
        }

        // end shooting
        if (currShootDuration <= 0) {
            isShooting = false;
            currShootDuration = shootDuration;
            currShootCD = shootCD;
        }

        if (isShooting)
        {
            currShootDuration -= Time.deltaTime;
            currBulletCD -= Time.deltaTime;
            if (currBulletCD <= 0)
            {
                Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                currBulletCD = bulletCD;
            }
        }
        else 
        {
            currShootCD -= Time.deltaTime;
        }

        if (playerTransform.position.x >= transform.position.x)
        {
            if (rb.velocity.x < 5f)
            {
                rb.velocity = new Vector2(rb.velocity.x + 0.5f, rb.velocity.y);
            }
            else rb.velocity = new Vector2(5f, rb.velocity.y);
        }
        else
        {
            if (rb.velocity.x > -5f)
            {
                rb.velocity = new Vector2(rb.velocity.x - 0.5f, rb.velocity.y);
            }
            else rb.velocity = new Vector2(-5f, rb.velocity.y);
        }
    }

    void GetHit(int getdamage, float immunetime)
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        shootCD = 0.5f;
        base.GetHit(getdamage, immunetime);
    }
}