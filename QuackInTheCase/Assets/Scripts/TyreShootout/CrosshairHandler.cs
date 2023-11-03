using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairHandler : MonoBehaviour
{
    private Color normalColor = Color.white;
    private Color lightColor = Color.red;
    private float pulseSpeed = 5.0f;
    public SpriteRenderer spriteRenderer;

    private bool isDragged = false;
    private Vector3 offset;
    private float shake = 0.1f;
    public GameObject enemyCar;
    public GameObject instruction;
    public GameObject god;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Pulse());
    }

    private void OnMouseDown()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!isDragged)
        {
            isDragged = true;
            spriteRenderer.color = normalColor;
        }
        if (instruction != null)
        {
            instruction.SetActive(false);
        }
        
    }

    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

        // Add a slight random shake effect to the position
        float offsetX = Random.Range(-shake, shake);
        float offsetY = Random.Range(-shake, shake);
        Vector3 shakeOffset = new Vector3(offsetX, offsetY, 0);

        transform.position = curPosition + shakeOffset;
    }

    private void OnMouseUp()
    {
        CheckCollision();
    }

    private void CheckCollision()
    {
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Tyres"))
            {
                god.GetComponent<LevelManager>().CarCollided();
                Debug.Log("Collision with Tyres detected.");
            }
        }
        else
        {
            Debug.Log("Collision unsuccessful with Tyres.");
            god.GetComponent<LevelManager>().FailGame();
        }
        Destroy(gameObject);
    }

    IEnumerator Pulse()
    {
        while (!isDragged)
        {
            float t = Mathf.PingPong(Time.time * pulseSpeed, 1.0f);
            Color lerpedColor = Color.Lerp(normalColor, lightColor, t);
            spriteRenderer.color = lerpedColor;
            yield return null;
        }
    }
}
