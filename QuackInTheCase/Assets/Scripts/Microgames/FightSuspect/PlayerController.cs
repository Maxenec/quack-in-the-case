using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject myHP;
    public GameObject suspect;
    public GameObject shield;
    public GameObject myPos;
    public GameObject god;
    private Vector3 target;
    public int strength = 25;
    public float coolDown = 2f;
    public bool blocking = false;
    public bool attacking = false;
    public bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            LeftClick();
        }
        if (Input.GetMouseButtonDown(1)){
            RightClickDown();
        }
        if (Input.GetMouseButtonUp(1)){
            RightClickUp();
        }
    }

    public void Hit(int power){ //when this is hit, if it is blocking reduce it's HP by the power of the hit else reduce the HP to 0
        if (myHP != null){
            if (blocking){
                myHP.GetComponent<HP>().EffectHP(-power);
            }
            else{
                myHP.GetComponent<HP>().SetHP(0);
            }
        }
    }

    public void LeftClick(){ //when the left mouse button is clicked, if it is not blocking or attacking and it can attack the it attacks the suspect
        if (myHP != null && !blocking && !attacking && canAttack && !god.GetComponent<GameManager>().IsPaused()){
            attacking = true;
            target = suspect.transform.position;
            StartCoroutine(Punch(false));
        }
    }

    public void RightClickDown(){ //when the right mouse button is pressed down, if it is not blocking or attacking, it blocks
        if (myHP != null && !blocking && !attacking && !god.GetComponent<GameManager>().IsPaused()){
            blocking = true;
            shield.SetActive(true);
        }
    }

    public void RightClickUp(){ //when the right button is relesed it stops blocking
        if (myHP != null && blocking && !attacking && !god.GetComponent<GameManager>().IsPaused()){
            blocking = false;
            shield.SetActive(false);
        }
    }

    IEnumerator Punch(bool touch){
        //Debug.Log(target);
        transform.position = Vector3.MoveTowards (transform.position, target, 50 * Time.deltaTime);//move towards the target
        if (transform.position.x == target.x && !touch){//if it reaches the target and has not touched it befor then, chatge the target to the origional position and activate the suspect's hit function
            target = myPos.transform.position;
            touch = true;
            suspect.GetComponent<SuspectController>().Hit(strength);
    }else if (transform.position.x == target.x && touch){//if it reaches the target for the second time start the cool down function and stop this function
            attacking = false;
            StartCoroutine(CoolDown(coolDown));
            StopCoroutine(Punch(touch));
        }
        if(attacking){//if it is still attacking then wait one frame and repeteat this function
            yield return 0;
            StartCoroutine(Punch(touch));
        }
    }

    IEnumerator CoolDown(float time){//this can not attack until a set amount of time
        //Debug.Log("Cool down");
        canAttack = false;
        yield return new WaitForSeconds(time);
        canAttack = true;
    }
}
