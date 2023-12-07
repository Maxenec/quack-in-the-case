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
    public GameObject god;
    public ParticleSystem ps;
    public AudioSource pickAudio;
    public AudioSource snapAudio;
    private Color particleColor;

    // Start is called before the first frame update
    void Start()
    {
        currentPin = 0;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pickSpriteArray[0];
        isBroken = false;
        canClick = true;
        particleColor = new Color (0, 1, 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canClick && !god.GetComponent<GameManager>().IsPaused())
        {
            CheckColour();
        }
        var main = ps.main;
        main.startColor = particleColor;
    }

    private void CheckColour(){
        switch (indicator.GetComponent<PickIndicator>().GetColour()){
            case 1:
                particleColor = new Color (0, 1, 0, 0.5f);
                ps.Play();
                pickAudio.Play();
                Push();
                break;
            case 2:
                particleColor = new Color (1, 1, 0, 0.5f);
                ps.Play();
                pickAudio.Play();
                break;
            case 3:
                particleColor = new Color (1, 0.647f, 0, 0.5f);
                ps.Play();
                pickAudio.Play();
                Fall();
                break;
            case 4:
                particleColor = new Color (1, 0, 0, 0.5f);
                ps.Play();
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
        snapAudio.Play();
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
            Snap();
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