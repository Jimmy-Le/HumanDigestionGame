using UnityEngine;

public class MissileAttackScript : AttackScript
{
    [Header("Missile Settings")]
    [SerializeField] public float speed = 3f;           // Missile Speed

    [SerializeField] public float duration = 4f;        // Missile duration before destroying


    [SerializeField] public bool followOn = false;      // Toggle On if you want the projectile to follow the target
    [SerializeField] public bool isMoving = false;
    [SerializeField] public float turnSpeed = 180f;     // Used for following targets
    
    [SerializeField] public Transform target;
    private Vector3 direction;
    
    // Set the initial direction and let it move
    public override void Shoot(Transform destination)
    {
        target = destination;

        direction = (target.position - transform.position).normalized;

        isMoving = true;

    }

    void Update()
    {
        
        if (isMoving)
        {
            // If the duration expired or if the follow on is up and the target died, destroy this projectile
            if (duration <= 0f || followOn && target == null)
            {
                Destroy(this.gameObject);
            }

            // If the follow is on, keep changing the direction every update as well as the rotation to make it smooth
            if (followOn && target != null)
            {
                direction = (target.position - transform.position).normalized;

                Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
                transform.rotation =
                    Quaternion.RotateTowards(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
            }
            
            // Move towards the direction
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            
            // Decrement the duration
            duration -= Time.deltaTime;

        }
        
    }
    
    
}
