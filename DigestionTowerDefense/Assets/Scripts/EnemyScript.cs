using UnityEngine;

public class EnemyScript : EntityScript
{
    public bool hasEscaped = false;
    

    public override void Die()
    {
        
        // Set the level of the next enemy.
        // If the enemy died, it will appear as a smaller version (harder to digest)
        // If the enemy escaped, it will appear in the same form
        int nextEnemyLevel = hasEscaped ? unitLevel : unitLevel - 1;

        // If the enemy has been completely killed, delete and return
        if (nextEnemyLevel < 0)
        {
            Destroy(this.gameObject);
            return;
        }
        
        GameObject nextEnemy = GameManager.instance.GenerateEnemy(nextEnemyLevel); // Generate an enemy object that is 1 level lower or the same if it escaped
        
        // If the enemy died, there is a 33% chance to spawn a next tier enemy at the same location
        if (!hasEscaped)
        {
            int randomChance = Random.Range(0, 3);

            if (randomChance == 1)
            {
                int currentDirection = this.gameObject.GetComponent<EnemyMovementScript>().direction;
                nextEnemy.GetComponent<EnemyMovementScript>().SetDirection(currentDirection);
                Instantiate(nextEnemy, transform.position, transform.rotation);
            }
            
            // If it doesn't hit, it will be added to the next enemies list
            else
            {
                GameManager.instance.nextEnemies.Add(nextEnemy);
            }
            
        }
        // If it did escape, it will add an enemy of the same level to the next enemies list
        else
        {
            GameManager.instance.nextEnemies.Add(nextEnemy);
        }
        
        Destroy(this.gameObject);
    }
    
    public void SetEscaped()
    {
        hasEscaped = true;
    }
    
}
