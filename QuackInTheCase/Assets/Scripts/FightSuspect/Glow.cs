using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    public Color normalColor = Color.white;
    public Color lightColor;
    public float pulseSpeed = 5.0f;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Pulse());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Pulse()
    {
        while (gameObject.activeSelf)
        {
            float t = Mathf.PingPong(Time.time * pulseSpeed, 1.0f);
            Color lerpedColor = Color.Lerp(normalColor, lightColor, t);
            spriteRenderer.color = lerpedColor;
            yield return null;
        }
    }
}
