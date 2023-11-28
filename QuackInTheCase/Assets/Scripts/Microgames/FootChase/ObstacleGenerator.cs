using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstacles;
    private float speed = 12.0f;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomObstacles();
    }

    public void GenerateRandomObstacles()
    {
        int obstacleIndex = Random.Range(0, obstacles.Length);

        Vector3 spawnPosition = GetSpawnPosition(obstacleIndex);

        GameObject obstaclePrefab = Instantiate(obstacles[obstacleIndex], spawnPosition, Quaternion.identity);

        obstaclePrefab.GetComponent<ObstacleScript>().obstacleGenerator = this;
    }

    private Vector3 GetSpawnPosition(int obstacleIndex)
    {
        switch (obstacleIndex)
        {
            case 1:
                return new Vector3(transform.position.x, -3.3f, transform.position.z);
            case 2:
                return new Vector3(transform.position.x, -1.7f, transform.position.z);
            case 3:
                return new Vector3(transform.position.x, -1.3f, 0.1f);
            default:
                return transform.position;
        }
    }

    public float ObstacleSpeed()
    {
        return speed;
    }
}