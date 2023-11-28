using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickIndicator : MonoBehaviour
{
    private Vector3 target;
    public GameObject leftEdge;
    public GameObject rightEdge;
    private bool canMove;
    private int currentColour;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = leftEdge.transform.position;
        target = rightEdge.transform.position;
        canMove = true;
        StartCoroutine(Move(false));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Move(bool left){
        transform.position = Vector3.MoveTowards (transform.position, target, 2f * Time.deltaTime); //default speed = 8, testing speed = 2
        if (transform.position.x == target.x && !left){
            target = leftEdge.transform.position;
            left = true;
        }else if (transform.position.x == target.x && left){
            target = rightEdge.transform.position;
            left = false;
        }
        if (canMove){
            yield return 0;
            StartCoroutine(Move(left));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.name){
            case "Green":
                //Debug.Log("Green");
                currentColour = 1;
                break;
            case "Yellow":
                //Debug.Log("Yellow");
                currentColour = 2;
                break;
            case "Orange":
                //Debug.Log("Orange");
                currentColour = 3;
                break;
            case "Red":
                //Debug.Log("Red");
                currentColour = 4;
                break;
            default:
                //Debug.Log("No Colour");
                currentColour = 0;
                break;
        }
    }

    public int GetColour(){
        return currentColour;
    }
}