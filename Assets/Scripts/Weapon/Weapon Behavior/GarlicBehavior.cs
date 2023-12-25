using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBehavior : MeleeWeaponBehavior
{
    List<GameObject> markedEnemies;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy") && !markedEnemies.Contains(collider.gameObject))
        {
            EnemyStat enemy = collider.GetComponent<EnemyStat>();
            enemy.TakeDamage(GetCurrentDamage(), transform.position);

            markedEnemies.Add(collider.gameObject); // marked enemy add to the list
        }
        else if (collider.CompareTag("Prop") && !markedEnemies.Contains(collider.gameObject))
        {
            if (collider.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
                markedEnemies.Add(collider.gameObject);
            }
        }
    }
}
