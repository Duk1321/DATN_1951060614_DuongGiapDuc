using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    public LayerMask terrainMask;
   /* PlayerMovement pm;*/
    public GameObject currentChunk;
    Vector3 playerLastPosition;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject lastestChunk;
    public float maxOpDist;
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDuration;

    void Start()
    {
        /*StartedMapSpawn();*/

        playerLastPosition = player.transform.position;
    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimize();
    }

    private void ChunkChecker()
    {
        if(!currentChunk)
        {
            return;
        }

        Vector3 moveDir = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        string directionName = GetDirectionName(moveDir);

        CheckAndSpawnChunk(directionName);

        if (directionName.Contains("Up")){
            CheckAndSpawnChunk("Up");
        }
        if (directionName.Contains("Down"))
        {
            CheckAndSpawnChunk("Down");
        }
        if (directionName.Contains("Right"))
        {
            CheckAndSpawnChunk("Right");
        }
        if (directionName.Contains("Left"))
        {
            CheckAndSpawnChunk("Left");
        }

    }

    void CheckAndSpawnChunk(string direction)
    {
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(direction).position, checkerRadius, terrainMask))
        {
            SpawnChunk(currentChunk.transform.Find(direction).position);
        }
    }

    string GetDirectionName(Vector3 direction)
    {
        direction = direction.normalized;

        if (Math.Abs(direction.x) > Math.Abs(direction.y))
        {
            if(direction.y > 0.5)
            {
                return direction.x > 0 ? "Up Right" : "Up Left";
            }
            else if(direction.y < -0.5)
            {
                return direction.x > 0 ? "Down Right" : "Down Left";
            }
            else
            {
                return direction.x > 0 ? "Right" : "Left";
            }
        }
        else
        {
            if (direction.x > 0.5)
            {
                return direction.y > 0 ? "Up Right" : "Down Right";
            }
            else if (direction.x < -0.5)
            {
                return direction.y > 0 ? "Up Left" : "Down Left";
            }
            else
            {
                return direction.y > 0 ? "Up" : "Down";
            }
        }
    }

    void SpawnChunk(Vector3 spawnPosition)
    {
        int rand = UnityEngine.Random.Range(0, terrainChunks.Count);
        lastestChunk = Instantiate(terrainChunks[rand], spawnPosition, Quaternion.identity);
        spawnedChunks.Add(lastestChunk);
    }
    private void ChunkOptimize()
    {
        optimizerCooldown -= Time.deltaTime;

        if(optimizerCooldown <= 0) 
        {
            optimizerCooldown = optimizerCooldownDuration;
        }
        else
        {
            return;
        }

        foreach(GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }

}
