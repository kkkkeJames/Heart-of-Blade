using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class SwordWave2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D myCollider;
    public Transform playerTransform; 
    public LayerMask groundMask;

    [SerializeField] private float linearSpeed = 8f;
    private int direction;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        direction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().direction;
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        timeLeft = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft += Time.deltaTime;
        rb.velocity = new Vector2(direction * linearSpeed, 0);
        if (timeLeft > 20)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().GetHit(120, 0.2f);
            collision.GetComponent<Enemy>().GetStun(0.8f);
        }
        else if (collision.CompareTag("EliteEnemy"))
        {
            collision.GetComponent<Enemy>().GetHit(120, 0.2f);
            collision.GetComponent<Enemy>().GetStun(0.4f);
        }
    }

}