using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int damage;
    protected float immuneTime;
    public Transform playerTransform;
    // Start is called before the first frame update
    protected void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (immuneTime > 0) immuneTime -= Time.deltaTime;
        if (health <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().health += 20;
            Destroy(gameObject);
        }
    }
    public void GetHit(int getdamage, float immunetime)
    {
        if (immuneTime <= 0)
        {
            immuneTime = immunetime;
            health -= getdamage;
        }
    }
}
