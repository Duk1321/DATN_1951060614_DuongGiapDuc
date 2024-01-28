using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStat enemy;
    Transform player;
    Rigidbody2D rb;

    Vector2 knockbackVelocity;
    float knockbackDuration;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyStat>();
        player = FindObjectOfType<PlayerMovement>().transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(knockbackDuration > 0)
        {
            transform.position += (Vector3)knockbackVelocity * Time.deltaTime;
            knockbackDuration -= Time.deltaTime;
        }
        else
        {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();
            rb.velocity = new Vector2(direction.x * enemy.currentMoveSpeed, direction.y * enemy.currentMoveSpeed);
        }
    }

    public void KnockBack(Vector2 velocity, float duration)
    {
        if (knockbackDuration > 0) return;

        knockbackDuration = duration;
        knockbackVelocity = velocity;
    }
}
