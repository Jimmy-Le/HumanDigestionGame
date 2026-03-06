using UnityEngine;
using System.Collections;

public class EnemyMovementScript : MonoBehaviour
{
    [SerializeField] public float speed = 2f;               // Movement Speed
    [SerializeField] public float baseSpeed = 2f;           // Base Movement Speed
    [SerializeField] public int direction = 0;              // 0: Right, 1: Down, 2: Left, 3: Up
    [SerializeField] public bool isMoving = true;           // If its moving, can be used for pausing
    private Coroutine changeSpeedRoutine;                   // If multiple Scripts affecting the movespeed happens, the latest one will override
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartMoving();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            switch (direction)
                {
                    case 0: // Right
                        transform.Translate(Vector3.right * speed * Time.deltaTime);
                        break;
                    case 1: // Down
                        transform.Translate(Vector3.down * speed * Time.deltaTime);
                        break;
                    case 2: // Left
                        transform.Translate(Vector3.left * speed * Time.deltaTime);
                        break;
                    case 3: // Up
                        transform.Translate(Vector3.up * speed * Time.deltaTime);
                        break;
                }
        }
        
    }

    /***
     * This function is for external towers to slow or speed up enemies for a certain duration
     * If it is already affected by a speed effect, it overrides it.
     */
    public void AffectSpeed(float strength, float duration)
    {
        if (changeSpeedRoutine != null)
        {
            StopCoroutine(changeSpeedRoutine);
            speed = baseSpeed;
        }
        StartCoroutine(ChangeSpeed(strength, duration));
        
    }
    /***
     * Change the speed for a certain duration and reset it back to the base once it is over
     */
    public IEnumerator ChangeSpeed(float strength, float duration)
    {
        ModifySpeed(strength);
        yield return new WaitForSeconds(duration);
        ResetSpeed();
    }
    

    /***
     * Adjust the speed of the enemy
     * Useful for towers that may inflict slows
     */
    public void ModifySpeed(float amount)
    {
        speed = Mathf.Max(0.1f, speed + amount);
    }

    /***
     * Reset the speed back to the original value
     */
    public void ResetSpeed()
    {
        speed = baseSpeed;
    }

    /***
     * Stop movement
     * Useful for time freeze or pause
     */
    public void StopMoving()
    {
        isMoving = false;
    }

    /***
     * Start movement
     * Resume movement
     */
    public void StartMoving()
    {
        isMoving = true;
    }

    /***
     * This function will decrement 1 from the direction, which will make it turn left
     * If the new direction is negative, manually loop it back to the max direction (3)
     */
    public void TurnLeft()
    {
        direction += -1;
        if (direction < 0)
        {
            direction = 3;
        }
    }

    /***
     * This function will increment the direction by 1, turning it right.
     * Modulus should be able to handle going out of bounds
     */
    public void TurnRight()
    {
        direction = (direction + 1) % 4;
    }
    
    /***
     * Set the direction of an enemy
     * This is used when an enemy dies and potentially spawns a new enemy
     */
    public void SetDirection(int direction)
    {
        this.direction = direction;
    }
    
    
}
