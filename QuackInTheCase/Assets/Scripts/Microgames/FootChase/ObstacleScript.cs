using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public ObstacleGenerator obstacleGenerator;

    void Update()
    {
        MoveObstacle();
    }

    private void MoveObstacle()
    {
        transform.Translate(Vector2.left * obstacleGenerator.ObstacleSpeed() * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision);
    }

    private void HandleCollision(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Regenerator":
                GenerateRandomObstacles();
                break;
            case "Edges":
                DestroyObstacle();
                break;
            default:
                break;
        }
    }

    private void GenerateRandomObstacles()
    {
        if (obstacleGenerator != null)
        {
            obstacleGenerator.GenerateRandomObstacles();
        }
    }

    private void DestroyObstacle()
    {
        Destroy(gameObject);
    }
}
