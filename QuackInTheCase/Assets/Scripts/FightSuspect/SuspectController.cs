using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SuspectController : MonoBehaviour
{
    public int strength = 25;
    public bool blocking = false;
    public bool attacking = false;
    public GameObject myHP;
    public GameObject player;
    public GameObject shield;
    public GameObject attack;
    public GameObject myPos;
    private Vector3 target;
    private float minWait = 1f;
    private float maxWait = 5f;
    private int state = 0;

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

    public void Hit(int power){
        Debug.Log(power);
        if (myHP != null){
            Debug.Log(blocking);
            if (!blocking)
            {
                myHP.GetComponent<HP>().EffectHP(-power);
            }
        }
    }

    IEnumerator AI(){
        while (!attacking){
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            state = Random.Range(0, 3);
            Debug.Log(state);
            if (state == 0){
                Idle();
                yield return null;
            }
            else if (state == 1){
                Block();
                yield return null;
            }
            else if (state == 2){
                StartCoroutine(Attack());
            }
        }
    }

    private void Idle(){
        blocking = false;
        attacking = false;
        shield.SetActive(false);
        attack.SetActive(false);
    }

    private void Block(){
        blocking = true;
        attacking = false;
        shield.SetActive(true);
        attack.SetActive(false);
    }

    IEnumerator Attack(){
        blocking = false;
        attacking = true;
        shield.SetActive(false);
        attack.SetActive(true);
        yield return new WaitForSeconds(1f);
        target = player.transform.position;
        attack.SetActive(false);
        StartCoroutine(Punch(false));
        StopCoroutine(Attack());
    }

    IEnumerator Punch(bool touch){
        Debug.Log(target);
        transform.position = Vector3.MoveTowards (transform.position, target, 50 * Time.deltaTime);
        if (transform.position.x == target.x && !touch){
            target = myPos.transform.position;
            touch = true;
            player.GetComponent<PlayerController>().Hit(strength);
        }else if (transform.position.x == target.x && touch){
            state = 0;
            Idle();
            StartCoroutine(AI());
            StopCoroutine(Attack());
        }
        if(attacking){
            yield return 0;
            StartCoroutine(Punch(touch));
        }
    }

}
