using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerRunController : MonoBehaviour
{
    private float jumpForce = 26f;
    private bool isGrounded = false;
    private bool hitObstacle = false;

    private Rigidbody2D rb;
    public Animator animator;
    private BoxCollider2D boxCollider;
    private Vector2 normalSize;
    private Vector2 duckingSize;

    private void Awake()
    {
        InitializeColliders();
    }

    void Update()
    {
        if (isGrounded)
        {
            CheckInputForAction();
        }
    }

    private void InitializeColliders()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", true);
        boxCollider = GetComponent<BoxCollider2D>();
        normalSize = boxCollider.size;
        duckingSize = new Vector2(normalSize.y, 1);
    }

    private void CheckInputForAction()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Duck();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Stand();
        }
    }

    private void Jump()
    {
        if (rb != null)
        {
            Stand();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
        else
        {
            Debug.Log("No rigid body found.");
        }
    }

    private void Duck()
    {
        AdjustCollider(new Vector2(0, -0.505f), duckingSize, true);
    }

    private void Stand()
    {
        AdjustCollider(new Vector2(0, -0.23f), normalSize, false);
    }

    private void AdjustCollider(Vector2 offset, Vector2 size, bool isDucking)
    {
        boxCollider.offset = offset;
        boxCollider.size = size;
        animator.SetBool("isDucking", isDucking);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isGrounded == false)
        {
            isGrounded = true;
            animator.SetBool("isRunning", true);
        } 
        else if (collision.gameObject.CompareTag("Obstacle"))
        {

            Debug.Log("Obstacle hit.");
            hitObstacle = true;
        }
    }

    public bool HitStatus()
    {
        return hitObstacle;
    }
}
