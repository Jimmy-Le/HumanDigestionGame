using UnityEngine;

public abstract class AttackScript : MonoBehaviour
{
    [SerializeField] public EntityScript attacker;
    [SerializeField] public string targetTag = "Enemy";
	[SerializeField] public bool attackActive = true;


    // The child script should define how they control the movement of the attack
	// For melee attacks, this is not necessary
    public virtual void Shoot(Transform destination)
	{
    	Debug.Log("Shoot");
	}

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // If the attack touches a target (Entity), the target takes damage.
        if (collision.gameObject.CompareTag(targetTag) && attackActive)
        {
            EntityScript targetScript = collision.gameObject.GetComponent<EntityScript>();
            targetScript?.TakeDamage(attacker.GetAttack(), attacker.isPiercing);
        }

        Destroy(this.gameObject);
    }

	public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // If the attack touches a target (Entity), the target takes damage.
        if (collision.gameObject.CompareTag(targetTag) && attackActive)
        {
            EntityScript targetScript = collision.gameObject.GetComponent<EntityScript>();
            targetScript?.TakeDamage(attacker.GetAttack(), attacker.isPiercing);
        }																                                            
    }
}
