using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private BoxCollider2D currentOneWayPlatform;
    [SerializeField] private CapsuleCollider2D playerCapsuleCollider;
    [SerializeField] private BoxCollider2D playerFeetCollider;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s") && currentOneWayPlatform != null) {
            StartCoroutine("DisableCollision");
        }
    }

    //gets reference to current platform that player is standing on
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "OneWayPlatform") {
            Debug.Log("Entering");
            currentOneWayPlatform = collision.gameObject.GetComponent<BoxCollider2D>();
        }   
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "OneWayPlatform") {
            Debug.Log("Exiting");
            currentOneWayPlatform = null;
        }   
    }

    //disables collision between player and current platform for given period of time
    private IEnumerator DisableCollision() {
        BoxCollider2D platformToDisable = currentOneWayPlatform;
        Physics2D.IgnoreCollision(playerCapsuleCollider, platformToDisable);
        // Physics2D.IgnoreCollision(playerFeetCollider, currentOneWayPlatform);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(playerCapsuleCollider, platformToDisable, false);         
        // Physics2D.IgnoreCollision(playerFeetCollider, currentOneWayPlatform, false);
    }
}
