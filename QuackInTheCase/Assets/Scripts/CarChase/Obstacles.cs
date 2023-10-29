using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    //references to obstacle spawn positions
    public GameObject leftObsPos;
    public GameObject rightObsPos;
    //the speed the obstacles ravle down the road i.e. the speed the road is going
    private float speed = 10.0f;
    //what lane it is in?
    private bool left = true;
    //reference list of active obstacles
    public List<GameObject> obs;
    //reference to the current obstacle
    private GameObject currentObs;

    // Start is called before the first frame update
    void Start()
    {
        Spawner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawner(){
        //random bool (true or false)/(left or right)
        if(Random.Range(0.0f, 1.0f)>0.5){
            left = true;
        }else{
            left = false;
        }
        //select random obstacle
        currentObs = obs[Random.Range(0,obs.Count)];

        //moving obstacles to spawn locations
        if(left == true){
            currentObs.transform.position = leftObsPos.transform.position;
        }else{
            currentObs.transform.position = rightObsPos.transform.position;
        }

        StartCoroutine(Move());
    }

    IEnumerator Move(){
        //if the obstacle is higher than the y level -10, then move it down then repeate this functin after one frame
        if(currentObs.transform.position.y > -10){
            currentObs.transform.position += Vector3.down * speed * Time.deltaTime;
            yield return 0;
            StartCoroutine(Move());
        } else {
            //if the obstacle reaches y level -10 then repeat this script
            Spawner();
        }
    }
}
