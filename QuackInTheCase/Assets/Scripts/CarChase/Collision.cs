using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public GameObject player;
    public GameObject manager;
    private bool hitPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Start");
        hitPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        //Debug.Log("collide");
        if(collision.gameObject == player.gameObject){
            hitPlayer = true;
        }
    }

    public bool HitStatus()
    {
        return hitPlayer;
    }
}
