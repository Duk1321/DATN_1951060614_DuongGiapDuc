using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsRandomizer : MonoBehaviour
{
    //List point to spawn prop
    public List<GameObject> propSpawnPoints;
    //List prop prefabs to spawn into point
    public List<GameObject> propPrefabs;

    void Start()
    {
        SpawmProps();   
    }

    void Update()
    {
        
    }

    void SpawmProps()
    {
        //random prop prefab into point
        foreach(GameObject sp in propSpawnPoints)
        {
            int rand = Random.Range(0, propPrefabs.Count);
            GameObject prop = Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity);
            //Spawn GameObject into parent
            prop.transform.parent = sp.transform;
        }
    }
}
