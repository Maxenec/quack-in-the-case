using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPins : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] pinSpriteArray;
    private bool isUp = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pinSpriteArray[0];
        isUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            ChangeSprite();
        }
    }

    public void ChangeSprite(){
        if(!isUp){
            spriteRenderer.sprite = pinSpriteArray[1];
            isUp = true;
        }else if(isUp){
            spriteRenderer.sprite = pinSpriteArray[0];
            isUp = false;
        }
    }
}
