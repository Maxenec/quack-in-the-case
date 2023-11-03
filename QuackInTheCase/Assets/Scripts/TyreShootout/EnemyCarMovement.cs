using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarMovement : MonoBehaviour
{
    private float leftEdge = -4.5f;
    private float rightEdge = 4.5f;
    private float moveSpeed = 6f;
    private bool moveRight;
    private bool escaped = false;

    private Vector3 initialScale;
    private float speed = 2f;
    private float scaleFactor = 5f;
    private float shrinkDuration = 4;
    private float timer = 0f;

    public GameObject smoke;

    private void Awake()
    {
        moveRight = Random.value > 0.5f;
        SetRandomStartPosition();
        smoke.SetActive(false);
    }

    private void Start()
    {
        initialScale = transform.localScale;
    }

    void SetRandomStartPosition()
    {
        //Set the initial position of the square to a random x-value within the defined edges.
        float randomX = Random.Range(leftEdge, rightEdge);
        Vector3 startPosition = new Vector3(randomX, transform.position.y, transform.position.z);
        transform.position = startPosition;
    }

    private void Update()
    {
        if (!escaped)
        {
            MoveCar();
            CheckEdges();
        }
        else
        {
            ZoomAway();
        }
    }

    private void MoveCar()
    {
        //Move the car left and right within the specified edges.
        if (moveRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }

    private void CheckEdges()
    {
        //Check if the car reaches the edges, then change direction.
        if (transform.position.x >= rightEdge)
        {
            moveRight = false;
        }
        else if (transform.position.x <= leftEdge)
        {
            moveRight = true;
        }
    }

    public void ShotMissed()
    {
        escaped = true;
        CarSmoke();
    }

    private void CarSmoke()
    {
        smoke.SetActive(true);
    }

    private void ZoomAway()
    {
        timer += Time.deltaTime;

        if (timer < shrinkDuration)
        {
            float progress = timer / shrinkDuration;
            transform.localScale = Vector3.Lerp(initialScale, initialScale / scaleFactor, progress);
            transform.Translate(Vector2.down * speed * Time.deltaTime);

        }
        else
        {
            transform.localScale = initialScale * scaleFactor;

            Destroy(gameObject);
        }
    }
}
