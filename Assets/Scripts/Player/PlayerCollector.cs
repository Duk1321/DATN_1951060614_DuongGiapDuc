using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStat player;
    CircleCollider2D playerCollector;
    public float pullSpeed;

    void Start()
    {
        player = FindObjectOfType<PlayerStat>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        playerCollector.radius = player.CurrentMagnet;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // check if the game object has the ICollectable interface
        if(collision.gameObject.TryGetComponent(out ICollectable collectable))
        {
            /*collision.gameObject.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, pullSpeed * Time.deltaTime).normalized;*/
            
            
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(forceDirection * pullSpeed);


            //if it does, call the method
            collectable.Collect();
        }    
    }
}
