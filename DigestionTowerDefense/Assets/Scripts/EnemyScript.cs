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
        
        // If the level of the current enemy can be reduced by 1,
        // In this game, the smaller the level, the stronger the enemy.
        if (nextEnemyLevel >= 0)
        {
            GameObject nextEnemy = GameManager.instance.GenerateEnemy(nextEnemyLevel);
            GameManager.instance.nextEnemies.Add(nextEnemy);
        }
        
        Destroy(this.gameObject);
    }
    
    public void SetEscaped()
    {
        hasEscaped = true;
    }
    
}
