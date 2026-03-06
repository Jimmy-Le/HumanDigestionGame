using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] public float spawnDelay = 1f;
    [SerializeField] public int spawnDirection = 0;
    [SerializeField] public bool isSpawning = false;

    // void Start()
    // {
    //     SpawnWave();
    // }

    void Update()
    {
        if (GameManager.instance.spawnWave)
        {
            if (!isSpawning)
            {
                isSpawning = true;
                SpawnWave();
            }
        }
    }
    
    public void SpawnWave()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        List<GameObject> enemies = GameManager.instance.enemies;

        foreach (GameObject enemy in enemies)
        {
            enemy.transform.position = this.transform.position;
            enemy.GetComponent<EnemyMovementScript>().direction = spawnDirection;
            enemy.SetActive(true);
            yield return new WaitForSeconds(spawnDelay);
        }

        GameManager.instance.SetSpawningStatusFalse();
        isSpawning = false;
    }
    

    
}
