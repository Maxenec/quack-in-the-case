using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairHandler : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private float shake = 0.1f;

    private void OnMouseDown()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

            // Add a slight random shake effect to the position
            float offsetX = Random.Range(-shake, shake);
            float offsetY = Random.Range(-shake, shake);
            Vector3 shakeOffset = new Vector3(offsetX, offsetY, 0);

            transform.position = curPosition + shakeOffset;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
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
                Debug.Log("Collision with Tyre detected.");
            }
        }
        else
        {
            Debug.Log("Tyres not hit.");
        }
    }
}
