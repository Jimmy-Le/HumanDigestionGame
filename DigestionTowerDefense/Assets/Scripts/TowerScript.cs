using UnityEngine;

public class TowerScript : EntityScript
{
    [Header("Tower Stats")]
    [SerializeField] public float attackSpeed = 1f;         // Delay between attacks
    [SerializeField] public bool isPiercing = false;        // Ignores enemy armor
    [SerializeField] public int placability = 0;            // 0: Place on wall. 1: Place on track

    [SerializeField] public int refund = 10;                // The amount of Nutrition refunded when a tower dies

    public override void Die()
    {
        GameManager.instance.ModifyNutrition(refund);       // Refund some nutrition when a tower dies
        Destroy(this.gameObject);
    }

}
