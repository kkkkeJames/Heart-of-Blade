using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MagnetAccelerator : MonoBehaviour
{
    private CapsuleCollider2D magnetAccelerator;
    [SerializeField] private Vector2 lingerSpeed;
    [SerializeField] private float maxBoostRadius = 0.5f;
    [SerializeField] private float minBoostRadius = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        magnetAccelerator = GetComponent<CapsuleCollider2D>();
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
            Transform playerTransform = player.GetComponent<Transform>().transform;
            Vector2 tilePosition = magnetAccelerator.offset;// tilemap.CellToWorld(cellPosition);
            Vector2 playerxy = new Vector2(playerTransform.position.x, playerTransform.position.y);
            float playerDistance = (playerxy - tilePosition).magnitude;

            Vector2 playerSpeedBoost = lingerSpeed * Mathf.Min(Mathf.Max(minBoostRadius - playerDistance, 0f) / (minBoostRadius - maxBoostRadius), 1f);
            Debug.Log(playerDistance + ", " + playerSpeedBoost);
            player.GetComponent<PlayerController>().lingerSpeed = playerSpeedBoost;
            player.GetComponent<PlayerController>().lingerSpeedTime = 0.2f;
        }
    }
}
