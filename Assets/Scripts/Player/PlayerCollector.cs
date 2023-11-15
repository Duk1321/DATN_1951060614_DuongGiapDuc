using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // check if the game object has the ICollectable interface
        if(collision.gameObject.TryGetComponent(out ICollectable collectable))
        {
            //if it does, call the method
            collectable.Collect();
        }    
    }
}
