using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRange = 9f;
    public GameObject enemyPrefab;
    public GameObject powerup;
    public int enemyCount;
    public int waveNumber = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length; //enemy type inda olanlarin uzunlugu

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(powerup, GenerateSpawnPositions(), powerup.transform.rotation);   // adds powerups
            Instantiate(enemyPrefab, GenerateSpawnPositions(), enemyPrefab.transform.rotation);
        }   
    }

    private Vector3 GenerateSpawnPositions()
    {
        float spawnPosX = Random.Range(spawnRange, -spawnRange);
        float spawnPosZ = Random.Range(spawnRange, -spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
