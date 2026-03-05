using UnityEngine;

public abstract class AttackScript : MonoBehaviour
{
    [SerializeField] public EntityScript attacker;
    [SerializeField] public string targetTag = "Enemy";


    // The child script must define how they control the movement 
    public abstract void Shoot(Transform destination);

    virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // If the attack touches a target (Entity), the target takes damage.
        if (collision.gameObject.CompareTags(targetTag))
        {
            EntityScript targetScript = collision.gameObject.GetComponent<EntityScript>();
            targetScript.TakeDamage(attacker.GetAttack(), attacker.isPiercing);
        }
    }
}
