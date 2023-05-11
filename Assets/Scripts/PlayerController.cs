using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    private Rigidbody2D playerRb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isOnGround;
    private bool doubleJumpUsed;
    private Vector2 moveVec;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        playerRb.velocity = new Vector2(moveVec.x * moveSpeed * Time.fixedDeltaTime, playerRb.velocity.y);
    }

    public void Move(InputAction.CallbackContext cbct)
    {
        moveVec = cbct.ReadValue<Vector2>();

        if(moveVec.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(moveVec.x < 0)
        {
            spriteRenderer.flipX = false;
        }

        if(moveVec  == Vector2.zero)
        {
            animator.SetTrigger("StopWalk");
        }
        else
        {
            animator.SetTrigger("StartWalk");
        }
        
    }

    public void Jump(InputAction.CallbackContext cbct)
    {
        if (isOnGround)
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else if(cbct.performed && !doubleJumpUsed)
        {
            playerRb.AddForce(Vector2.up * jumpForce * gravity, ForceMode2D.Impulse);
            doubleJumpUsed = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            doubleJumpUsed = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}
