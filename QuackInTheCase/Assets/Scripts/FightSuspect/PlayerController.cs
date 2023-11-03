using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject myHP;
    public GameObject Suspect;
    public int strength = 25;
    public bool blocking = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
        if (myHP != null)
        {
            Suspect.GetComponent<SuspectController>().Hit(strength);
        }
    }

    public void RightClickDown()
    {
        if (!blocking)
        {
            blocking = true;
        }
    }

    public void RightClickUp()
    {
        if (blocking)
        {
            blocking = false;
        }
    }
}
