using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject myHP;
    public GameObject suspect;
    public GameObject shield;
    public GameObject myPos;
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
        if (Input.GetMouseButtonDown(1))
        {
            RightClickDown();
        }
        if (Input.GetMouseButtonUp(1))
        {
            RightClickUp();
        }
    }

    public void Hit(int power)
    {
        if (myHP != null)
        {;
            if (blocking)
            {
                myHP.GetComponent<HP>().EffectHP(-power);
            }
            else
            {
                myHP.GetComponent<HP>().SetHP(0);
            }
        }
    }

    public void LeftClick()
    {
        if (myHP != null && !blocking && !attacking && canAttack)
        {
            attacking = true;
            target = suspect.transform.position;
            StartCoroutine(Punch(false));
        }
    }

    public void RightClickDown()
    {
        if (myHP != null && !blocking && !attacking)
        {
            blocking = true;
            shield.SetActive(true);
        }
    }

    public void RightClickUp()
    {
        if (myHP != null && blocking && !attacking)
        {
            blocking = false;
            shield.SetActive(false);
        }
    }

    IEnumerator Punch(bool touch){
        Debug.Log(target);
        transform.position = Vector3.MoveTowards (transform.position, target, 50 * Time.deltaTime);
        if (transform.position.x == target.x && !touch){
            target = myPos.transform.position;
            touch = true;
            suspect.GetComponent<SuspectController>().Hit(strength);
    }else if (transform.position.x == target.x && touch){
            attacking = false;
            StartCoroutine(CoolDown(coolDown));
            StopCoroutine(Punch(touch));
        }
        if(attacking){
            yield return 0;
            StartCoroutine(Punch(touch));
        }
    }

    IEnumerator CoolDown(float time){
        Debug.Log("Cool down");
        canAttack = false;
        yield return new WaitForSeconds(time);
        canAttack = true;
    }
}
