using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPick : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] pickSpriteArray;
    private bool isBroken = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pickSpriteArray[0];
        isBroken = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            ChangeSprite();
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
}
