using UnityEngine;

public class ArmorReductionScript : AttackScript
{
    [SerializeField] public int armorReduction = -1;

   
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        // If the attack touches a target (Entity), the target takes damage.
        if (collision.gameObject.CompareTag(targetTag) && attackActive)
        {
            EnemyScript targetScript = collision.gameObject.GetComponent<EnemyScript>();
            targetScript?.ModifyArmor(armorReduction);
        }																                                            
    }
}
