using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControlls : MonoBehaviour
{
    //referneces to empty game objects that indicate the lane posisions
    public GameObject leftLanePos;
    public GameObject rightLanePos;
    public GameObject god;
    //what lane is the car currntly in?
    private bool leftLane = true;
    //speed the car changes lane
    private float speed = 40.0f;

    //collision check
    private bool hitOb = false;

    public GameObject Explosion;
    private bool playedSound;

    // Start is called before the first frame update
    void Start()
    {
        hitOb = false;
        Explosion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //left arrow key
        if (Input.GetKeyDown("left") || Input.GetKeyDown("a") && !god.GetComponent<GameManager>().IsPaused() & !hitOb){
            //print("left arrow");
            playedSound = false;
            StartCoroutine(Left());
        }
        //right arrow key
        if (Input.GetKeyDown("right") || Input.GetKeyDown("d") && !god.GetComponent<GameManager>().IsPaused() & !hitOb){
            //print("right arrow");
            playedSound = false;
            StartCoroutine(Right());
        }
    }

    IEnumerator Left(){
        //check if they are in the right lane
        if(!leftLane){
            if(!playedSound){
            gameObject.GetComponent<AudioSource>().Play();
            playedSound = true;
            }
            //move car closer to left lane
            transform.position = Vector3.MoveTowards(transform.position, leftLanePos.transform.position, speed * Time.deltaTime);
            //if car is not in the left lane then wait one frame and start this function again
            if(transform.position.x > leftLanePos.transform.position.x){
                yield return 0;
                StartCoroutine(Left());
            }else{
                leftLane = true;
            }
        }
    }

    IEnumerator Right(){
        //check if they are in the left lane
        if(leftLane){
            if(!playedSound){
            gameObject.GetComponent<AudioSource>().Play();
            playedSound = true;
            }
            //move car closer to right lane
            transform.position = Vector3.MoveTowards(transform.position, rightLanePos.transform.position, speed * Time.deltaTime);
            //if car is not in the right lane then wait one frame and start this function again
            if(transform.position.x < rightLanePos.transform.position.x){
                yield return 0;
                StartCoroutine(Right());
            }else{
                leftLane = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        //Debug.Log("collide");
        if(collision.gameObject.tag == "Obstacle"){
            //Debug.Log("hit");
            hitOb = true;
            Explosion.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public bool HitStatus()
    {
        return hitOb;
    }
}
