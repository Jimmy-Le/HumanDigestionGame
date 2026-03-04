using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;     // Create a Singleton Instance to make this accessible to all game objects

    [Header("Prefabs")] 
    [SerializeField] public GameObject[] enemyPrefabs;

    public int enemyTypes;
    
    
    // Game Stats
    public int level;                           // Game Level
    public int nutrition = 200;                 // Currency to buy towers
    public int round;                           // Rounds 

    
    // List of Enemies
    public List<GameObject> enemies;            // List of enemies for the current Level
    public List<GameObject> nextEnemies;        // List of enemies for the next Level
    
    
    void Awake()
    {
        instance = this;
        enemyTypes = enemyPrefabs.Length;
        DontDestroyOnLoad(this);
    }

    /***
     * This function Initializes an Enemy based on the provided UnitLevel
     * returns the Enemy GameObject
     */
    public GameObject GenerateEnemy(int unitLevel)
    {
        GameObject enemy = Instantiate(enemyPrefabs[unitLevel]);
        enemy.SetActive(false);
        return enemy;
    }

    /***
     * This function adds / subtracts to the nutrition
     * It returns a boolean value corresponding to a valid transaction
     */
    public bool ModifyNutrition(int nutritionModifier)
    {
        tempNutrition = nutrition + nutritionModifier;
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
    
    
    
}
