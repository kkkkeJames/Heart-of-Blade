using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int damage;
    [SerializeField] private GameObject healthloot;
    protected float immuneTime;
    public int direction;
    public bool Stunned;
    public float StunTime;
    public Transform playerTransform;
    // Start is called before the first frame update
    protected void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Stunned)
        {
            StunTime -= Time.deltaTime;
            if (StunTime <= 0)
            {
                StunTime = 0;
                Stunned = false;
            }
        }
        if (immuneTime > 0) immuneTime -= Time.deltaTime;
        if (health <= 0)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health < GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().healthMax - 20)
            {
                int loot = Random.Range(4, 6);
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health < 30)
                    loot = Random.Range(7, 10);
                for (int i = 0; i < loot; i++)
                {
                    Instantiate(healthloot, new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y), Quaternion.identity);
                }
            }
            Destroy(gameObject);
        }

        if (direction == 1) transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(-1, 1, 1);
    }
    public void GetHit(int getdamage, float immunetime)
    {
        if (immuneTime <= 0)
        {
            immuneTime = immunetime;
            health -= getdamage;
        }
    }
    public void GetStun(float stuntime)
    {
        Stunned = true;
        StunTime = stuntime;
    }
}
