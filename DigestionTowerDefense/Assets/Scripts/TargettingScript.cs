using UnityEngine;
using System.Collections;

public class TargettingScript : MonoBehaviour
{
    [SerializeField] public TowerScript tower;              // Get Information from the original tower
    [SerializeField] public GameObject attackPrefab;        // Get The Attack Prefab (Projectile)
    [SerializeField] public LayerMask targetLayer = 6;      // Layer to target 

    [SerializeField] public bool isSearching = true;        // Searching is active
    [SerializeField] public float searchCooldown = 0.2f;    // Searching Cooldown
    private Transform targettedObject;                      // The Transform of the Target

    private float attackCooldown = 0f;
    
    void Update()
    {
        if (targettedObject != null)
        {
            if (attackCooldown <= 0f)
            {
                attackPrefab.GetComponent<AttackScript>().Shoot(targettedObject);
                attackCooldown = 1f / tower.attackSpeed;
            }
        }

        attackCooldown -= Time.deltaTime;
    }

    /***
     * This function will Search for targets every 0.2 seconds
     */
    private IEnumerable searchTargets()
    {
        while (isSearching)
        {
            FindNearestTarget();
            yield return new WaitForSeconds(searchCooldown);
        }
    }

    /***
     * This function searches through a given radius to find all objects within a given layer, and finds the closest one.
     */
    void FindNearestTarget()
    {
        Collider2D[] targetsInRange = Physics2D.OverlapCircleAll(transform.position, tower.range, targetLayer);

        float shortestDistance = Mathf.Infinity;
        Transform nearestTarget = null;

        foreach (Collider2D target in targetsInRange)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestTarget = target.transform;
            }
        }
        
        targettedObject = nearestTarget;
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }
}
