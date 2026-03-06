using UnityEngine;
using System.Collections;

public class TowerMovementScript : MonoBehaviour
{
    [SerializeField] public Transform upperLimit;           // Move Up towards this point
    [SerializeField] public Transform bottomLimit;          // Move Down towards this point, this is the time attacks will happen if set up

    [SerializeField] public float moveSpeed = 2f;           // Movement speed

    [SerializeField] private bool isMoving = true;          // is currently moving

    [SerializeField] public bool isDown = false;            // A period of time, if true then the attack will be active 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(BounceLoop());
    }

    /***
     * This function makes the tower move up and down towards a given point
     */
    private IEnumerator BounceLoop()
    {
        while (isMoving)
        {
            isDown = false;
            yield return StartCoroutine(MoveTo(upperLimit.position, moveSpeed));
            isDown = true;
            yield return StartCoroutine(MoveTo(bottomLimit.position, moveSpeed));
        }
    }

    /***
     * This function makes the tower move to a certain point.
     */
    private IEnumerator MoveTo(Vector3 targetPosition, float speed)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

    /***
     * This function determines when isDown period
     * This is not used right now, but it can be useful if you only want the attack to be active at a certain time during the movement
     */
    public bool GetIsDown()
    {
        return isDown;
    }
}
