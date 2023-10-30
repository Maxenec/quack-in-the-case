using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControlls : MonoBehaviour
{
    //referneces to empty game objects that indicate the lane posisions
    public GameObject leftLanePos;
    public GameObject rightLanePos;
    //what lane is the car currntly in?
    private bool leftLane = true;
    //speed the car changes lane
    private float speed = 40.0f;
    private bool crashed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //left arrow key
         if (Input.GetKeyDown("left")){
            //print("left arrow");
            StartCoroutine(Left());
        }
        //right arrow key
        if (Input.GetKeyDown("right")){
            //print("right arrow");
            StartCoroutine(Right());
        }
    }

    IEnumerator Left(){
        //check if they are in the right lane
        if(leftLane == false && crashed == false){
            //move car closer to left lane
            transform.position = Vector3.MoveTowards(transform.position, leftLanePos.transform.position, speed * Time.deltaTime);
            //if car is not in the left lane then wait one frame and start this function again
            if(transform.position.x > leftLanePos.transform.position.x && crashed == false){
                yield return 0;
                StartCoroutine(Left());
            }else if(crashed == false){
                leftLane = true;
            }else{
                StopCoroutine(Right());
                StopCoroutine(Left());
            }
        }
    }

    IEnumerator Right(){
        //check if they are in the left lane
        if(leftLane == true && crashed == false){
            //move car closer to right lane
            transform.position = Vector3.MoveTowards(transform.position, rightLanePos.transform.position, speed * Time.deltaTime);
            //if car is not in the right lane then wait one frame and start this function again
            if(transform.position.x < rightLanePos.transform.position.x && crashed == false){
                yield return 0;
                StartCoroutine(Right());
            }else if(crashed == false){
                leftLane = false;
            }else{
                StopCoroutine(Left());
                StopCoroutine(Right());
            }
        }
    }

    public void Crash(){
        crashed = true;
    }
}
