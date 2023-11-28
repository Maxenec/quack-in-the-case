using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPick : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] pickSpriteArray;
    private bool isBroken = false;
    public GameObject indicator;
    private int currentPin = 0;
    public List<GameObject> lockPins;
    private bool canClick = true;
    private Vector3 target;
    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        currentPin = 0;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pickSpriteArray[0];
        isBroken = false;
        canClick = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canClick){
            CheckColour();
        }
    }

    private void CheckColour(){
        switch(indicator.GetComponent<PickIndicator>().GetColour()){
            case 1:
                Push();
                break;
            case 2:
                break;
            case 3:
                Fall();
                break;
            case 4:
                Snap();
                break;
            case 0:
                Debug.Log("error");
                break;
            default:
                Debug.Log("error");
                break;
        }
    }

    private void Snap(){
        ChangeSprite();
    }

    private void Push(){
        //Debug.Log("push");
        lockPins[currentPin].GetComponent<LockPins>().ChangeSprite();
        if(currentPin != lockPins.Count - 1){
            currentPin++;
            canClick = false;
            StartCoroutine(MoveTo(new Vector3(lockPins[currentPin].transform.position.x - 6f, transform.position.y, transform.position.z)));
        }
    }

    private void Fall(){
        //Debug.Log("fall");
        if(currentPin != 0){
            currentPin--;
            lockPins[currentPin].GetComponent<LockPins>().ChangeSprite();
            canClick = false;
            StartCoroutine(MoveTo(new Vector3(lockPins[currentPin].transform.position.x - 6f, transform.position.y, transform.position.z)));
        }else{
            ChangeSprite();
        }
    }

    IEnumerator MoveTo(Vector3 target){
        while(!canClick){
            transform.position = Vector3.MoveTowards(transform.position, target, 3f * Time.deltaTime);
            //Debug.Log(transform.position.x - target.x);
            if (transform.position.x == target.x){
                canClick = true;
                StopCoroutine(MoveTo(target));
            }
            yield return null;
        }
    }

    public void ChangeSprite(){
        if(!isBroken){
            spriteRenderer.sprite = pickSpriteArray[1];
            isBroken = true;
        }else if(isBroken){
            spriteRenderer.sprite = pickSpriteArray[0];
            isBroken = false;
        }
    }

    public bool CheckStatus(){
        return isBroken;
    }
}