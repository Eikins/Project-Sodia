using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [Header("Physics Properties")]
    public float moveSpeed = 1.0f;
    public float jumpForce = 5.0f;

    private float inputMovement;
    private bool inputAttack;
    private bool inputJump;
    
    private bool facingRight;
    private bool isGrounded;

    private bool canMove;

    private Animator animator;
    private new Rigidbody2D rigidbody;

    private Transform groundCheck;
    
    void Start() {
        facingRight = true;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        canMove = true;
    }

    void Update() {
        Inputs();
        
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, LayerMask.GetMask("Ground"));
        animator.SetBool("OnGround", isGrounded);

        if(inputAttack) PerformAttack(); else if(inputJump && isGrounded && canMove) PerformJump();

        if(canMove) {
            SetMoveSpeed(inputMovement * moveSpeed);
            if(facingRight && inputMovement < -0.1f) {
                Flip();
            } else if(!facingRight && inputMovement > 0.1f) {
                Flip();
            }
        }

    }

    void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawLine(transform.position, transform.Find("GroundCheck").position);
    }

    private void SetMoveSpeed(float velocity) {
        rigidbody.velocity = new Vector2(velocity, rigidbody.velocity.y);
        animator.SetFloat("MoveSpeed", Mathf.Abs(velocity));
    }

    private void Inputs() {
        inputAttack = Input.GetButtonDown("Fire1");
        inputMovement = Input.GetAxis("Horizontal");
        inputJump = Input.GetButtonDown("Jump");
    }

    private void PerformAttack() {
        SetCanMove(false);
        animator.SetTrigger("Attack");
    }

    private void OnAttackFinished() {
        SetCanMove(true);
    }

    private void SetCanMove(bool value) {
        if(value) {
            canMove = true;
        } else {
            canMove = false;
            SetMoveSpeed(0f);
        }
    }

    private void PerformJump() {
        rigidbody.AddForce(Vector2.up * jumpForce);
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }


}
