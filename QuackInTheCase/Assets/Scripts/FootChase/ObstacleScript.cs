using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public ObstacleGenerator obstacleGenerator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * obstacleGenerator.ObsSpeed() *  Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Regenerator"))
        {
            obstacleGenerator.GenerateRandomObstacles();
        }
        else if (collision.gameObject.CompareTag("Edges"))
        {
            Destroy(gameObject);
        }
    }
}
