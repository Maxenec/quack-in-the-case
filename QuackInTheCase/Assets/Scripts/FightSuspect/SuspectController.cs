using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SuspectController : MonoBehaviour
{
    public int power = 25;
    public bool blocking = false;
    public bool attacking = false;
    public GameObject myHP;
    public GameObject player;
    public GameObject shield;
    public GameObject attack;
    public GameObject myPos;
    private float minWait = 1f;
    private float maxWait = 5f;
    private int state = 0;
    public float xCenter = 0f;

    // Start is called before the first frame update
    void Start()
    {
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
            if (blocking)
            {
                Debug.Log("blocked!");
            }
            else
            {
                myHP.GetComponent<HP>().EffectHP(-power);
            }
        }
    }

    IEnumerator AI()
    {
        while (!attacking)
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            state = Random.Range(0, 3);
            Debug.Log(state);
            if (state == 0)
            {
                Idle();
                yield return null;
            }
            else if (state == 1)
            {
                Block();
                yield return null;
            }
            else if (state == 2)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private void Idle()
    {
        blocking = false;
        attacking = false;
        shield.SetActive(false);
        attack.SetActive(false);
    }

    private void Block()
    {
        blocking = true;
        attacking = false;
        shield.SetActive(true);
        attack.SetActive(false);
    }

    IEnumerator Attack()
    {
        blocking = false;
        attacking = true;
        shield.SetActive(false);
        attack.SetActive(true);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Punch());
    }

    IEnumerator Punch()
    {
        transform.position = new Vector3(xCenter + Mathf.PingPong(Time.time * 0.25f, transform.position.x - player.transform.position.x), transform.position.y, transform.position.z);
        if(transform.position.x < myPos.transform.position.x)
        {
            yield return 0;
            StartCoroutine(Punch());
        }
        player.GetComponent<PlayerController>().Hit(power);
        attacking = false;
        attack.SetActive(false);
        state = 0;
        //Debug.Log("Ouch!");
        StartCoroutine(AI());

    }

}
