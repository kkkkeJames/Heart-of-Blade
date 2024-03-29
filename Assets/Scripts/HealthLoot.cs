using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class HealthLoot : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D myCollider;
    public Transform playerTransform;
    public LayerMask groundMask;
    public float TimeExist;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeExist += Time.deltaTime; 
        if (TimeExist > 30) Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().GetHeal(2);
            Destroy(gameObject);
        }
    }

}