using UnityEngine;

public class ExitTileScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            collision.gameObject.GetComponent<EnemyScript>().SetEscaped(true);
            collision.gameObject.GetComponent<EnemyScript>().Die();

        }
    }
}
