using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Player;
    [SerializeField] private Animator Animator;

    [SerializeField] private float Speed;
    [SerializeField] private Vector2 JumpForce;

    [SerializeField] private GameObject spawmpoint;


    [SerializeField] private LayerMask Mask;
    private bool isGrounded = false;

    private Vector3 RightFacing = new Vector3(1, 1, 1);
    private Vector3 LeftFacing = new Vector3(-1, 1, 1);

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 1.5f, Mask);
        isGrounded = (hit.collider != null);
        float direction = Input.GetAxis("Horizontal");

        if (direction != 0)
        {
            Player.transform.position += Vector3.right * direction * Time.deltaTime * Speed;
            Animator.SetBool("IsWalking", true);

            if (direction>0)
            {
                transform.localScale = RightFacing;
            }
            else if(direction<0)
            {
                transform.localScale = LeftFacing;
            }
        }   
        else
        {
            Animator.SetBool("IsWalking", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Player.AddForce(JumpForce, ForceMode2D.Impulse);
            Animator.SetBool("IsJumping", true);
        }



        Animator.SetBool("IsJumping", !isGrounded);

        if (gameObject.transform.position.y<-5)
        {
            RespawnPlayer();
        }
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
        
        if(col.gameObject.layer == 9)
        {
            Animator.SetBool("IsDieing", true);
           
        }
    }

    public void RespawnPlayer()
    { 
        gameObject.transform.position = spawmpoint.transform.position;
        Animator.SetBool("IsDieing", false);
    }
}
