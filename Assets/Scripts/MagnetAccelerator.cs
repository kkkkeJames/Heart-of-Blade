using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MagnetAccelerator : MonoBehaviour
{
    //private CapsuleCollider2D magnetAccelerator;
    [SerializeField] private Vector2 lingerSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        //magnetAccelerator = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            Vector2 directionalLingerSpeed = new Vector2(lingerSpeed.x * player.GetComponent<PlayerController>().direction, lingerSpeed.y);

            player.GetComponent<PlayerController>().lingerSpeed = directionalLingerSpeed;
            player.GetComponent<PlayerController>().lingerSpeedTime = 0.2f;
        }
    }
}
