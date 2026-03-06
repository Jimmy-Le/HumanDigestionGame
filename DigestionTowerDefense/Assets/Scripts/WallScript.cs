using UnityEngine;

public class WallScript : MonoBehaviour
{
    public bool turnRight = true;           // If its false, turn left

    // If an enemy hits the wall, it will turn either right or left
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (turnRight)
            {
                collision.gameObject.GetComponent<EnemyMovementScript>().TurnRight();
            }
            else
            {
                collision.gameObject.GetComponent<EnemyMovementScript>().TurnLeft();
            }
        }
    }
}
