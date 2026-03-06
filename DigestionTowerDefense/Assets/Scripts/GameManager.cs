using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;     // Create a Singleton Instance to make this accessible to all game objects

    [Header("Prefabs")] 
	[SerializeField] public GameObject[] towerPrefabs;		// List of all the tower prefabs
    [SerializeField] public GameObject[] enemyPrefabs;		// List of all the enemy prefabs
    public int enemyTypes;									// Length of the enemy Prefab array
    [SerializeField] public List<int> availableTowers;		// List of towers ID available for the current level

	[Header("Mobs")]
	[SerializeField] public Transform enemySpawnFolder;		// Location to spawn the enemies in the file hierarchy
	[SerializeField] public int startingEnemies = 20;		// How many enemies we want to start with
	
    
    
    // Game Stats
    public int level;                           // Game Level
    public string levelName;					// Level Name
    public int nutrition = 200;                 // Currency to buy towers
    public int round;                           // Rounds 
    public bool spawnWave = false;				// This decides if you want to start the wave

    
    // List of Enemies
    public List<GameObject> enemies;            // List of enemies for the current Level
    public List<GameObject> nextEnemies;        // List of enemies for the next Level
    public int enemiesLeft;						// The amount of enemies left in the current level (Used For UI)

	// UI Tower Selection
	[SerializeField] public GameObject selectedTower;			// This holds the prefab of the selected tower in the shop
	[SerializeField] public int selectedTowerID = 0;			// This holds the ID of the selected tower in the shop

    
    void Awake()
    {
        instance = this;
        enemyTypes = enemyPrefabs.Length;
		GenerateInitialEnemy();
		enemiesLeft = startingEnemies;
		
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
        if (tempNutrition >= 0)
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
    
	/***
	 * This function sets the Selected Tower's ID and prefab
	 */
	public void ChooseTower(int towerID)
	{
		selectedTowerID = towerID;
		selectedTower = towerPrefabs[towerID];
	}

	/***
	 *  This function decrements the amount of enemies left on the map
	 * This is used because if we try to get the Count of the enemies list, it will ignore the enemies spawned in from being killed
	 * There were also other reasons but i forgot
	 */
	public void DecrementEnemiesLeft()
	{
		enemiesLeft -= 1;
	}
	
	/***
	 * This function Increment the amount of enemies left
	 * Sometimes when mobs die, they instantiate the next level of itself, you would think it would remain constant, but somehow it doesnt
	 */
	public void IncrementEnemiesLeft()
	{
		enemiesLeft += 1;
	}

	/***
	 * Set the name of the level 
	 */
	public void SetLevelName(string levelName)
	{
		this.levelName = levelName;
	}

	/***
	 * This function will show the available towers in a level
	 * Each level has its own set of usable towers, this function will enable the ones included in the provided list
	 */
	public void SetAvailableTowers(List<int> availableTowerSelections)
	{
		availableTowers = availableTowerSelections;
	}
	
	/***
	 * This function loads the next scene, if it reaches the end, it will loop back to the title screen
	 * TODO: There is currently a bug where because we keep the GameManager and UI Script, when we go back to the title screen, it will also appear there.
	 */
	public void LoadNextScene()
	{
		int currentIndex = SceneManager.GetActiveScene().buildIndex;
		int nextIndex = currentIndex + 1;
        
		int totalScenes = SceneManager.sceneCountInBuildSettings;
		if (nextIndex < totalScenes)
		{
			SceneManager.LoadScene(nextIndex);
			StartNextLevel();
		}
		else
		{
			SceneManager.LoadScene(0);
			
		}
	}

	/***
	 * This function will initialize the settings when a new level has started
	 * It will put the next enemies as the current enemies and stop the wave from spawning right away
	 */
	public void StartNextLevel()
	{
		enemies.Clear();
		enemies = new List<GameObject>(nextEnemies);
		nextEnemies.Clear();
		enemiesLeft = enemies.Count;
		spawnWave = false;
	}
    
	/***
	 * This function starts the wave
	 * Theres 2 functions, because this one is called by a button
	 */
	public void SetSpawningStatus()
	{
		spawnWave = true;
	}
	/***
	 * This function stops the wave
	 */
	public void SetSpawningStatusFalse()
	{
		spawnWave = false;
	}
}
