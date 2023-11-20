using System.Collections;
using System.Collections.Generic;
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

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateRandomObstacles()
    {
        int obstacleIndex = Random.Range(0, obstacles.Length);

        GameObject obstaclePreFab;

        //Spawn different obstacles based on the obstacle index
        switch (obstacleIndex)
        {
            case 1:
                obstaclePreFab = Instantiate(obstacles[obstacleIndex], new Vector3(transform.position.x, -3.3f, transform.position.z), transform.rotation);
                break;
            case 2:
                obstaclePreFab = Instantiate(obstacles[obstacleIndex], new Vector3(transform.position.x, -1.7f, transform.rotation.z), transform.rotation);
                break;
            case 3:
                obstaclePreFab = Instantiate(obstacles[obstacleIndex], new Vector3(transform.position.x, -1.3f, 0.1f), transform.rotation);
                break;
            default:
                obstaclePreFab = Instantiate(obstacles[obstacleIndex], transform.position, transform.rotation);
                break;
        }

        obstaclePreFab.GetComponent<ObstacleScript>().obstacleGenerator = this;
    }

    public float ObstacleSpeed()
    {
        return speed;
    }
}