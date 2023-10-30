using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject roadSpawnPos;
    private float speed = 10.0f;
    private bool crashed = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Move(){
        if(transform.position.y >= -11){
            transform.position += Vector3.down * speed * Time.deltaTime;
            yield return 0;
            if(crashed == false){
                StartCoroutine(Move());
            }
        } else {
            transform.position = new Vector3(transform.position.x, roadSpawnPos.transform.position.y, transform.position.z);
            yield return 0;
            if(crashed == false){
                StartCoroutine(Move());
            }
        }
        if(crashed){
            StopCoroutine(Move());
        }
    }

    public void Crash(){
        crashed = true;
    }
}
