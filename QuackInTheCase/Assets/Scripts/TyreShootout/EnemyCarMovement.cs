using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarMovement : MonoBehaviour
{
    private float leftEdge = -4.5f;
    private float rightEdge = 4.5f;
    private float moveSpeed = 6f;
    private bool moveRight;

    private void Awake()
    {
        moveRight = Random.value > 0.5f;
        SetRandomStartPosition();
    }

    void SetRandomStartPosition()
    {
        // Set the initial position of the square to a random x-value within the defined edges
        float randomX = Random.Range(leftEdge, rightEdge);
        Vector3 startPosition = new Vector3(randomX, transform.position.y, transform.position.z);
        transform.position = startPosition;
    }

    private void Update()
    {
        MoveCar();
        CheckEdges();
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
        //Check if the square reaches the edges, then change direction.
        if (transform.position.x >= rightEdge)
        {
            moveRight = false;
        }
        else if (transform.position.x <= leftEdge)
        {
            moveRight = true;
        }
    }
}
