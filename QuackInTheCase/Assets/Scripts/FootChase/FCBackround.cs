using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCBackround : MonoBehaviour
{
    private float speed = 12.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        if (transform.position.x >= -19)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            yield return 0;
            StartCoroutine(Move());
        }
        else
        {
            transform.position = new Vector3(19, transform.position.y, transform.position.z);
            yield return 0;
            StartCoroutine(Move());
        }
    }
}
