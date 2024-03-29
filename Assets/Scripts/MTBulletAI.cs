using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class MTBulletAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D myCollider;
    public Transform playerTransform; 
    public LayerMask groundMask;

    [SerializeField] private float linearSpeed = 8f;
    private Vector2 baseVector;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        baseVector = new Vector2(playerTransform.position.x - transform.position.x, playerTransform.position.y - transform.position.y).normalized;
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = baseVector * linearSpeed;
        if (myCollider.IsTouchingLayers(groundMask))
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int damage = UnityEngine.Random.Range(6, 12);
            collision.GetComponent<PlayerController>().GetHit(damage, 0.2f);
            Destroy(gameObject);
        }
    }

}