using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    private float foregroundMoveSpeed = 0.05f;
    private float backgroundMoveSpeed = 0.25f;
    private float scaleFactor = 1.5f;
    private float growthDuration = 60.0f;
    private Vector3 initialScale;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSprite();
    }

    private void CheckSprite()
    {
        if (gameObject.tag == "ForegroundClouds")
        {
            transform.Translate(Vector2.right * foregroundMoveSpeed * Time.deltaTime);
        }
        else if (gameObject.tag == "BackgroundClouds")
        {
            transform.Translate(Vector2.right * backgroundMoveSpeed * Time.deltaTime);
        }
        else if (gameObject.tag == "Buildings")
        {
            timer += Time.deltaTime;

            if (timer < growthDuration)
            {
                float progress = timer / growthDuration;
                transform.localScale = Vector3.Lerp(initialScale, initialScale * scaleFactor, progress);
                transform.Translate(Vector2.up * foregroundMoveSpeed * Time.deltaTime);

            }
            else
            {
                transform.localScale = initialScale * scaleFactor;
            }
        }
    }
}
