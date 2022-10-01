using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /***
     * TODO:
     * Spawn Animals
     */
    [Header("Prefabs")]
    public GameObject chickenPrefab;
    public GameObject cowPrefab;
    public GameObject sheepPrefab;

    [Header("Spawning")]
    public Transform spawnPosXMax;
    public Transform spawnPosXMin;
    public Transform spawnPosZMax;
    public Transform spawnPosZMin;
    public Transform defaultSpawnPos;

    [Header("Tracking Animals")]
    public List<GameObject> sheepList;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomSpawnPosition;

        for (int i = 0; i < 10; i++)
        {
            randomSpawnPosition = new Vector3(defaultSpawnPos.position.x + Random.Range(-8, 8), defaultSpawnPos.position.y, defaultSpawnPos.position.z + Random.Range(-6, 6));
            SpawnAnimal(sheepPrefab, randomSpawnPosition);
        }
        
        //SpawnAnimal(sheepPrefab, defaultSpawnPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAnimal(GameObject prefab, Vector3 spawnPosition)
    {
        GameObject spawnedPrefab = Instantiate(prefab, spawnPosition, Quaternion.identity);
        sheepList.Add(spawnedPrefab);
    }


    public void SelectAnimalPhase()
    {

    }

    public void OutcomesPhase()
    {
        Produce();
        Breed();
        Feed();
    }

    public void UpdateStatsPhase()
    {

    }

    public void EventPhase()
    {

    }

    public void UpgradesPhase()
    {

    }

    #region Buildings
    public void Breed()
    {

    }

    public void Feed()
    {

    }

    public void Produce()
    {

    }

    public void Sell()
    {

    }
    #endregion


}
