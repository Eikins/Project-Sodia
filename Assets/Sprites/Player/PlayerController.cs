using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [Header("Physics Properties")]
    public float moveSpeed = 1.0f;

    private float inputMovement;
    private bool inputAttack;
    private bool inputJump;
    
    private bool facingRight;

    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        if(inputAttack) PerformAttack(); else if(inputJump) PerformJump();

        animator.SetFloat("MoveSpeed", Mathf.Abs(inputMovement * moveSpeed));

        if(facingRight && inputMovement < -0.1f) {
            Flip();
        } else if(!facingRight && inputMovement > 0.1f) {
            Flip();
        }
    }

    private void Inputs() {
        inputAttack = Input.GetButtonDown("Fire1");
        inputMovement = Input.GetAxis("Horizontal");
        inputJump = Input.GetButtonDown("Jump");
    }

    private void PerformAttack() {
        animator.SetTrigger("Attack");
    }

    private void PerformJump() {
        animator.SetTrigger("Jump");
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }


}
