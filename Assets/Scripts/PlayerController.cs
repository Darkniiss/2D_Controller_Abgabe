using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D playerRb;
    private bool isOnGround;
    private Vector2 moveVec;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        playerRb.velocity = new Vector2(moveVec.x * moveSpeed * Time.fixedDeltaTime, playerRb.velocity.y);
    }

    public void Move(InputAction.CallbackContext cbct)
    {
        moveVec = cbct.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext cbct)
    {
        if (isOnGround)
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
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
