using UnityEngine;

public class EnemyScript : EntityScript
{
    public bool hasEscaped = false;     // If an enemy has escaped (did not get killed)
    

    public override void Die()
    {
        
        // Set the level of the next enemy.
        // If the enemy died, it will appear as a higher level version (harder to digest)
        // If the enemy escaped, it will appear in the same form
        int nextEnemyLevel = hasEscaped ? unitLevel : unitLevel + 1;

        // If there is no more levels
        if (nextEnemyLevel >= GameManager.instance.enemyTypes)
        {
            GameManager.instance.ModifyNutrition(nutrition);
            GameManager.instance.DecrementEnemiesLeft();
            Destroy(this.gameObject);
            return;
        }
        
        GameObject nextEnemy = GameManager.instance.GenerateEnemy(nextEnemyLevel); // Generate an enemy object that is 1 level lower or the same if it escaped
        
        // If the enemy died, there is a 33% chance to spawn a next tier enemy at the same location
        if (!hasEscaped)
        {
            int randomChance = Random.Range(0, 3);
			
			GameManager.instance.ModifyNutrition(nutrition);

            if (randomChance <= 1)
            {
                // Spawn a new enemy at the same location
                int currentDirection = this.gameObject.GetComponent<EnemyMovementScript>().direction;
                nextEnemy.GetComponent<EnemyMovementScript>().SetDirection(currentDirection);
                
                // Increment the amount of enemies left
                GameManager.instance.IncrementEnemiesLeft();
                GameObject newEnemy = Instantiate(nextEnemy, transform.position, transform.rotation);
                newEnemy.SetActive(true);

            }
            
            // If it doesn't hit, it will be added to the next enemies list
            else
            {
                GameManager.instance.nextEnemies.Add(nextEnemy);
            }
            
        }
        // If it did escape, it will add an enemy of the same level, but with the same HP to the next enemies list
        else
        {
			nextEnemy.GetComponent<EntityScript>().SetMaxHealth(this.health);
            GameManager.instance.nextEnemies.Add(nextEnemy);
            
        }
        
        // Reduce the amount of enemies displayed
        GameManager.instance.DecrementEnemiesLeft();
        Destroy(this.gameObject);
    }
    
	/***
	* If an enemy escapes, it will appear in the next stage in the same form.
	*/
    public void SetEscaped(bool hasEscaped)
    {
        this.hasEscaped = hasEscaped;
    }
    
}
