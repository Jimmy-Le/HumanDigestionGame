using UnityEngine;
using System.Collections;

public class MeleeAttackScript : AttackScript
{
    [SerializeField] private Transform towerBody;
    [SerializeField] private SpriteRenderer indicatorSprite;
    [SerializeField] private Transform upperRange;             // Move towards
    [SerializeField] private Transform lowerRange;             // Move towards after reaching top 

    [SerializeField] private Transform startPosition;          // Starting position, if you want to start somewhere in the middle

    [SerializeField] public bool allTimeActive = false;         // If you want the hitbox to be active the entire time, otherwise it will only be active when it is at the lower range
    [SerializeField] public float upDuration = 0.5f;                   // How long it takes to move up
    [SerializeField] public float downDuration = 0.5f;                 // How long it takes to move down
    [SerializeField] public float attackSpeed = 1f;                     // How many attacks in a second
    [SerializeField] public bool matchAttackSpeed = true;              // This will make upDuration and downDuration to be 50% of attack speed
    [SerializeField] public bool moveBody = true;               // Move the tower as well
    [SerializeField] public bool isMoving = false;              // For pausing / starting
    
    
    private Vector3 direction;

    void Start()
    {
        if (matchAttackSpeed)
        {
            upDuration = attackSpeed/ 2f;
            downDuration = attackSpeed / 2f;
        }
        Shoot(startPosition.transform);
    }
    
    public override void Shoot(Transform destination)
    {
        isMoving = true;
        StartCoroutine(MeleeAttack());

    }

    void Update()
    {
        if (!attackActive)
        {
            indicatorSprite.enabled = false;
        }
        else
        {
            indicatorSprite.enabled = true;
        }
    }

    private IEnumerator MeleeAttack()
    {
        while (isMoving)
        {
            Debug.Log($"Moving from {startPosition}");
            Debug.Log($"UpperRange world: {upperRange.position}");

            attackActive = false;

            // Phase 1: Move Upwards
            float timer = 0f;
            while (timer < upDuration)
            {
                timer += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(timer / upDuration);

                transform.position = Vector3.Lerp(startPosition.position, upperRange.position, normalizedTime);
                if (moveBody)
                {
                    towerBody.position = Vector3.Lerp(startPosition.position, upperRange.position, normalizedTime);
                }
                
                yield return null;
            }

            // Snap to the upper position
            transform.position = upperRange.position;
            if (moveBody)
            {
                towerBody.position = upperRange.position;
            }

            // Phase 2: Go down
            timer = 0f;
            while (timer < downDuration)
            {
                timer += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(timer / downDuration);

                transform.position = Vector3.Lerp(upperRange.position, lowerRange.position,
                    normalizedTime);
                
                if (moveBody)
                {
                    towerBody.position = Vector3.Lerp(upperRange.position, lowerRange.position,
                        normalizedTime);
                }
                
                yield return null;
            }

            // Snap to bottom position
            transform.position = lowerRange.position;
            
            if (moveBody)
            {
                towerBody.position = lowerRange.position;
            }
            if (!allTimeActive)
            {
                attackActive = true;
            }
        }
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }
    
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        // If the attack touches a target (Entity), the target takes damage.
        if (collision.gameObject.CompareTag(targetTag) && attackActive)
        {
            EntityScript targetScript = collision.gameObject.GetComponent<EntityScript>();
            targetScript?.TakeDamage(attacker.GetAttack(), attacker.isPiercing);
        }
        
    }
    
    
    
    


}
