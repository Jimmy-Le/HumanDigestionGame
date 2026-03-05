using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] public float spawnDelay = 1f;

    void Start()
    {
        SpawnWave();
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
            enemy.SetActive(true);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
