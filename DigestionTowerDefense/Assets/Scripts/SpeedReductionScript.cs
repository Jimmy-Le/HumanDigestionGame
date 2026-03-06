using UnityEngine;

public class SpeedReductionScript : AttackScript
{
   [SerializeField] public float speedReduction = -1f;
   [SerializeField] public float duration = 1f;
   
   /***
    * This function overrides the damage taking to instead slow the movement speed of an enemy
    */
   public override void OnTriggerEnter2D(Collider2D collision)
   {
      // If the attack touches a target (Entity), the target takes gets slowed.
      if (collision.gameObject.CompareTag(targetTag) && attackActive)
      {
         EnemyMovementScript targetScript = collision.gameObject.GetComponent<EnemyMovementScript>();
         targetScript?.AffectSpeed(speedReduction, duration);
      }																                                            
   }
}
