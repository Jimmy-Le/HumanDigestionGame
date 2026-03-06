using UnityEngine;

public class TowerScript : EntityScript
{
    [Header("Tower Stats")]
    [SerializeField] public float attackSpeed = 1f;         // Attacks per second
    [SerializeField] public int placability = 0;            // 0: Place on wall. 1: Place on track

    [SerializeField] public float range = 5f;               // Attack range where it will detect enemies, not very useful for melee towers, but you can prob make it work

    [SerializeField] public int refund = 10;                // The amount of Nutrition refunded when a tower dies (Functionality not implemented)

    public override void Die()
    {
        GameManager.instance.ModifyNutrition(refund);       // Refund some nutrition when a tower dies
        Destroy(this.gameObject);
    }

}
