using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstacles;
    private float speed = 15.0f;

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
        if (obstacleIndex == 1)
        {
            obstaclePreFab = Instantiate(obstacles[obstacleIndex], new Vector3(transform.position.x, -3.3f, transform.position.z), transform.rotation);
        }
        else if (obstacleIndex == 2)
        {
            obstaclePreFab = Instantiate(obstacles[obstacleIndex], new Vector3(transform.position.x, -1.7f, transform.rotation.z), transform.rotation);
        }
        else if (obstacleIndex == 3)
        {
            obstaclePreFab = Instantiate(obstacles[obstacleIndex], new Vector3(10.5f, -2, transform.rotation.z), transform.rotation);
        }
        else
        {
            obstaclePreFab = Instantiate(obstacles[obstacleIndex], transform.position, transform.rotation);
        }

        obstaclePreFab.GetComponent<ObstacleScript>().obstacleGenerator = this;
    }

    public float ObsSpeed()
    {
        return speed;
    }
}