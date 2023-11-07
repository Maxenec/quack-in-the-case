using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerRunController : MonoBehaviour
{
    public float jumpForce = 25f;
    private bool isGrounded = false;
    private bool hitObstacle = false;

    private Rigidbody2D rb;
    public Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetTrigger("Jump");
            Jump();
        }
        else if (isGrounded && Input.GetKeyDown(KeyCode.DownArrow))
        {
            Duck();
        }
    }

    private void Jump()
    {
        if (rb != null)
        {
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
