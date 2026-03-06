using UnityEngine;
using System.Collections;

public class TowerMovementScript : MonoBehaviour
{
    [SerializeField] public Transform upperLimit;
    [SerializeField] public Transform bottomLimit;

    [SerializeField] public float moveSpeed = 2f;

    [SerializeField] private bool isMoving = true;

    [SerializeField] public bool isDown = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(BounceLoop());
    }

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

    private IEnumerator MoveTo(Vector3 targetPosition, float speed)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

    public bool GetIsDown()
    {
        return isDown;
    }
}
