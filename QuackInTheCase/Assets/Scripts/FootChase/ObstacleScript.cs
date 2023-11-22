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
        transform.Translate(Vector2.left * obstacleGenerator.ObstacleSpeed() *  Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Regenerator":
                if (obstacleGenerator != null)
                {
                    obstacleGenerator.GenerateRandomObstacles();
                }
                break;
            case "Edges":
                Destroy(gameObject);
                break;
            default: 
                break;
        }
    }
}
