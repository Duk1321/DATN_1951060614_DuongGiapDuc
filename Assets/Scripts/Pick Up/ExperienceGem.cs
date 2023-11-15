using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : MonoBehaviour, ICollectable
{
    public int experienceGranted;
    public void Collect()
    {
        PlayerStat player = FindObjectOfType<PlayerStat>();
        player.IncreaseExperience(experienceGranted);
        Destroy(gameObject);
    }
}
