using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SuspectController : MonoBehaviour
{
    private int strength = 25;
    private bool blocking = false;
    private bool attacking = false;
    public GameObject myHP;
    public GameObject player;
    public GameObject shield;
    public GameObject attack;
    public GameObject myPos;
    private Vector3 target;
    private float minWait = 1f;
    private float maxWait = 3f;
    private int state = 0;
    private int newState = 1;

    // Start is called before the first frame update
    void Start()
    {
        target = myPos.transform.position;
        StartCoroutine(AI());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(int power){//when this is hit, if it is not blocking then reduce it's HP by the power of the hit
        //Debug.Log(power);
        if (myHP != null){
            //Debug.Log(blocking);
            if (!blocking)
            {
                myHP.GetComponent<HP>().EffectHP(-power);
            }
        }
    }

    IEnumerator AI(){
        while (!attacking){
            //while it is not attacking, wait a random amounnt of seconds
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            //picks either idle, block, or attack
            newState = Random.Range(0, 3);
            //Debug.Log(newState);
            //Debug.Log(state);
            if(newState == state && state != 2){//if it tries to either idel twise in a row of block twise in a row, it will attack instead
                state = 2;
            }else if (newState == state){ //if it tries to attack twise in a rowm it will idle instead
                state = 0;
            }else{ //if it isn't the same in a row, then the state will become what it picks
                state = newState;
            }
            if (state == 0){//if it picks the idle state then it runs the idle function and starts this function again
                Idle();
                ;
            }
            else if (state == 1){//if it picks the block state then it runs the block function and starts this function again
                Block();
                yield return null;
            }
            else if (state == 2){ //if it picks the attack state then it runs the attack function
                StartCoroutine(Attack());
            }
        }
    }

    private void Idle(){ //sets blocking and attacking to false
        blocking = false;
        attacking = false;
        shield.SetActive(false);
        attack.SetActive(false);
    }

    private void Block(){ //sets blocking to true and attacking to false
        blocking = true;
        attacking = false;
        shield.SetActive(true);
        attack.SetActive(false);
    }

    IEnumerator Attack(){ //sets blocing to false and attacking to true 
        blocking = false;
        attacking = true;
        shield.SetActive(false);
        attack.SetActive(true);
        yield return new WaitForSeconds(1f); //gives a warning for one second
        target = player.transform.position; //sets the teaget to the player's position
        attack.SetActive(false);
        StartCoroutine(Punch(false)); //starts the punch function
        StopCoroutine(Attack()); //stops this function
    }

    IEnumerator Punch(bool touch){
        //Debug.Log(target);
        transform.position = Vector3.MoveTowards (transform.position, target, 50 * Time.deltaTime);//move towards the target
        if (transform.position.x == target.x && !touch){ //if it reaches the target and has not touched it befor then, chatge the target to the origional position and activate the player's hit function
            target = myPos.transform.position;
            touch = true;
            player.GetComponent<PlayerController>().Hit(strength);
        }else if (transform.position.x == target.x && touch){ //if it reaches the target for the second time start the idle function and the ai function and stop this function
            Idle();
            StartCoroutine(AI());
            StopCoroutine(Attack());
        }
        if(attacking){//if it is still attacking then wait one frame and repeteat this function
            yield return 0;
            StartCoroutine(Punch(touch));
        }
    }
}
