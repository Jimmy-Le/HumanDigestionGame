using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;     // Create a Singleton Instance to make this accessible to all game objects

    [Header("Prefabs")] 
	[SerializeField] public GameObject[] towerPrefabs;
    [SerializeField] public GameObject[] enemyPrefabs;
    public int enemyTypes;						// Length of the enemy Prefab array

	[Header("Mobs")]
	[SerializeField] public Transform enemySpawnFolder;
	[SerializeField] public int startingEnemies = 20;

    
    
    // Game Stats
    public int level;                           // Game Level
    public int nutrition = 200;                 // Currency to buy towers
    public int round;                           // Rounds 

    
    // List of Enemies
    public List<GameObject> enemies;            // List of enemies for the current Level
    public List<GameObject> nextEnemies;        // List of enemies for the next Level
    

	// UI Tower Selection
	[SerializeField] public GameObject selectedTower;
	[SerializeField] public int selectedTowerID = 0;

    
    void Awake()
    {
        instance = this;
        enemyTypes = enemyPrefabs.Length;
		GenerateInitialEnemy();
        DontDestroyOnLoad(this);
    }

    /***
     * This function Initializes an Enemy based on the provided UnitLevel
     * returns the Enemy GameObject
     */
    public GameObject GenerateEnemy(int unitLevel)
    {
        GameObject enemy = Instantiate(enemyPrefabs[unitLevel]);
		enemy.transform.SetParent(enemySpawnFolder);
        enemy.SetActive(false);
        return enemy;
    }

    /***
     * This function adds / subtracts to the nutrition
     * It returns a boolean value corresponding to a valid transaction
     */
    public bool ModifyNutrition(int nutritionModifier)
    {
        int tempNutrition = nutrition + nutritionModifier;
        if (nutritionModifier >= 0)
        {
            nutrition = tempNutrition;
            return true;
        }
        else
        {
            return false;
        }
    }
	
	/***
	 * This function will generate random enemies at the start of Level 1
	 * and add them to the list of current enemies
	 * 
	 */
	public void GenerateInitialEnemy()
	{
		for(int i = 0; i < startingEnemies; i++)
		{
			int randomEnemy = Random.Range(0, enemyTypes);
			GameObject enemy = GenerateEnemy(randomEnemy);
			enemies.Add(enemy);
			
		}
	}
    
	
	public void ChooseTower(int towerID)
	{
		selectedTowerID = towerID;
	}
    
    
}
