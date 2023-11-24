using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCBackround : MonoBehaviour
{
    private float speed = 12.0f;
    private float resetPositionX = -19.0f;
    private float startPositionX = 18.0f;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            if (transform.position.x >= resetPositionX)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else
            {
                transform.position = new Vector3(startPositionX, transform.position.y, transform.position.z);
            }

            yield return null;
        }
    }
}
