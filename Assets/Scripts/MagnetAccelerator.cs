using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MagnetAccelerator : MonoBehaviour
{
    private CapsuleCollider2D magnetAccelerator;
    [SerializeField] private Vector2 lingerSpeed;
    // Start is called before the first frame update
    void Start()
    {
        magnetAccelerator = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().lingerSpeed = lingerSpeed;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().lingerSpeedTime = 0.2f;
        }
    }
}
