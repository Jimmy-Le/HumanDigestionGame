using UnityEngine;

public abstract class EntityScript : MonoBehaviour
{
    // Attributes 
    [Header("Entity Stats")]
    [SerializeField] public string entityName;              // Name of the entity
    [SerializeField] public int maxHealth;                  // Maximum Health of an entity (Possibly used for displaying it)
    [SerializeField] public int health;                     // Hit Points of the entity
    [SerializeField] public int attack;                     // The amount of damage the entity does to another entity
    [SerializeField] public int armor;                      // The amount of damage an entity can block
    [SerializeField] public int nutrition;                  // The value of an entity. Enemies: Currency dropped on death. Towers: Cost 
    [SerializeField] public int factionID;                  // 0: Towers, 1: Enemies
    [SerializeField] public int unitLevel;                  // Enemies: The type of enemy. Towers: Upgrades (This won't be implemented in this version)
    [SerializeField] public bool isPiercing;                // If an entity has Piercing, they can ignore armor when dealing dmg
    
    protected virtual void Awake()
    {
        health = maxHealth;                                 // Set the current Health to be the Max Health
    }

    /***
     * This function will decrease the health of the entity and if it runs out of HP, it will call the Die() function
     */
    public virtual void TakeDamage(int damage, bool pierce)
    {
        // Calculate health to determine if the enemy will die first
        health = health - Mathf.Max(0, (pierce ? damage : damage - armor));     // If the attack can pierce, it will ignore the armor. The attack cannot do negative damage.

        if (health <= 0)
        {
            Die();
        }
    }

    /***
     * This function will return the attack value of the entity
     * If there are any attack multipliers or bonuses, you can override it
     */
    public virtual int GetAttack()
    {
        return attack;
    }

    /***
     * This function will trigger all the effects that occur when an entity dies
     * This will be useful to override to add Enemies to the next section, animations, or Nutrition refund when destroying towers.
     */
    public virtual void Die()
    {
        Destroy(this.gameObject);
    }

	public virtual void SetMaxHealth(int newMaxHealth)
	{
		maxHealth = newMaxHealth;
	}
    
    
}
