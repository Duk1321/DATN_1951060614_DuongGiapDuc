using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    PlayerMovement pm;
    public GameObject currentChunk;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject lastestChunk;
    public float maxOpDist;
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDuration;

    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        /*StartedMapSpawn();*/
    }

    private void StartedMapSpawn()
    {
        noTerrainPosition = currentChunk.transform.Find("Right").position;
        SpawnChunk();
        noTerrainPosition = currentChunk.transform.Find("Left").position;
        SpawnChunk();
        noTerrainPosition = currentChunk.transform.Find("Up").position;
        SpawnChunk();
        noTerrainPosition = currentChunk.transform.Find("Down").position;
        SpawnChunk();
        noTerrainPosition = currentChunk.transform.Find("Down Right").position;
        SpawnChunk();
        noTerrainPosition = currentChunk.transform.Find("Down Left").position;
        SpawnChunk();
        noTerrainPosition = currentChunk.transform.Find("Up Right").position;
        SpawnChunk();
        noTerrainPosition = currentChunk.transform.Find("Up Left").position;
        SpawnChunk();
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
        if(pm.moveDir.x > 0 && pm.moveDir.y == 0) // player move to the right
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                SpawnChunk();
                /*noTerrainPosition = currentChunk.transform.Find("Down Right").position;
                SpawnChunk();
                noTerrainPosition = currentChunk.transform.Find("Up Right").position;
                SpawnChunk();*/
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y == 0) // player move to the left
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                SpawnChunk();
                /*noTerrainPosition = currentChunk.transform.Find("Up Left").position;
                SpawnChunk();
                noTerrainPosition = currentChunk.transform.Find("Down Left").position;
                SpawnChunk();*/
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir.y > 0) // player move up
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position;
                SpawnChunk();
               /* noTerrainPosition = currentChunk.transform.Find("Up Right").position;
                SpawnChunk();
                noTerrainPosition = currentChunk.transform.Find("Up Left").position;
                SpawnChunk();*/
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir.y < 0) // player move down
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                SpawnChunk();
                /*noTerrainPosition = currentChunk.transform.Find("Down Right").position;
                SpawnChunk();
                noTerrainPosition = currentChunk.transform.Find("Down Left").position;
                SpawnChunk();*/
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y < 0) // player move down right
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down Right").position;
                SpawnChunk();
                /*noTerrainPosition = currentChunk.transform.Find("Right").position;
                SpawnChunk();
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                SpawnChunk();*/
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y < 0) // player move down left
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down Left").position;
                SpawnChunk();
                /*noTerrainPosition = currentChunk.transform.Find("Left").position;
                SpawnChunk();
                noTerrainPosition = currentChunk.transform.Find("Down Left").position;
                SpawnChunk();*/
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y > 0) // player move up right
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up Right").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y > 0) // player move up left
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up Left").position;
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int rand = UnityEngine.Random.Range(0, terrainChunks.Count);
        lastestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
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
