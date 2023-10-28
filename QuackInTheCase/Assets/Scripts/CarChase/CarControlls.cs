using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControlls : MonoBehaviour
{
    public GameObject leftLanePos;
    public GameObject rightLanePos;
    private bool leftLane = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown("left")){
            print("left arrow");
            Left();
        }
        if (Input.GetKeyDown("right")){
            print("right arrow");
            Right();
        }
    }
    private void Left(){
        if(leftLane == false){
            leftLane = true;
            transform.position = leftLanePos.transform.position;
        }
    }
    private void Right(){
        if(leftLane == true){
            leftLane = false;
            transform.position = rightLanePos.transform.position;
        }
    }
}
