using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject prefabEnemy;
    private GameObject goPlayer;
    // private float spawnDelay = 1;
    private float minRate = 0.2f;
    private float maxRate = 2.0f;
    private float minSpawnPos = 10;
    private float maxSpawnPos = 25;

    private int WaveSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        goPlayer = GameObject.Find("Player");
        // StartCoroutine(recursiveSpawnInSecond(5));
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0){
            WaveSize += 2;
            spawnNewWave();
        }
    }

    private IEnumerator recursiveSpawnInSecond(float delay){

            yield return new WaitForSeconds(delay);
            
           spawnEnemy();

            float nextSpawnDelay = Random.Range(minRate, maxRate);
            Debug.Log("next spawn in " + nextSpawnDelay);
            StartCoroutine(recursiveSpawnInSecond(nextSpawnDelay));
    }

    private void spawnNewWave(){
        for(int i = 0; i < WaveSize; ++i){
            spawnEnemy();
        }
    }

    private void spawnEnemy(){

            float spawnPosZ = goPlayer.transform.position.z + Random.Range(minSpawnPos, maxSpawnPos) * (Random.Range(1, 9) % 2 == 0? 1 : -1);
            float spawnPosX = goPlayer.transform.position.x + Random.Range(minSpawnPos, maxSpawnPos) * (Random.Range(1, 9) % 2 == 0? 1 : -1);
            Vector3 spawnPos = new Vector3(spawnPosX, goPlayer.transform.position.y, spawnPosZ);

            Instantiate(prefabEnemy, spawnPos , prefabEnemy.transform.rotation);
    }
}
