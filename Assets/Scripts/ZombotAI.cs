using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ZombotAI : Enemy
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        Attack();
        if (math.abs(playerTransform.transform.position.x - transform.position.x) > 1)
        {
            if (playerTransform.transform.position.x >= transform.position.x)
                transform.position += new Vector3(0.01f, 0, 0);
            else transform.position -= new Vector3(0.01f, 0, 0);
        }
    }
    void Attack()
    {
        if (math.abs(playerTransform.transform.position.x - transform.position.x) <= 1)
        {
            anim.SetBool("attack", true);
        }
        else anim.SetBool("attack", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().GetHit(10, 0.2f);
        }
    }
}
