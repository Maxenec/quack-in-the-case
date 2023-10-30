using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public List<GameObject> roadScript;
    public List<GameObject> obstacleScript;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("collide");
        if(collision.gameObject == player.gameObject){
            Debug.Log("Player");
            for(int i = 0;i<roadScript.Count;i++){
                roadScript[i].GetComponent<Road>().Crash();
                Debug.Log(roadScript[i]);
            }
            for(int i = 0;i<obstacleScript.Count;i++){
                obstacleScript[i].GetComponent<Obstacles>().Crash();
                Debug.Log(obstacleScript[i]);
            }
            player.GetComponent<CarControlls>().Crash();
        }
    }
}
