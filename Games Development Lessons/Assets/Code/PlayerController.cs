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

    [SerializeField] private Vector2 Scale;

    // Controller Inputs

    private float direction;
    private bool jump;


    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");
        jump = Input.GetButtonDown("Jump");
    }

    /// <summary>
    /// Frame-rate independent MonoBehaviour.FixedUpdate message for physics calculations.
    /// </summary>
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.BoxCast(gameObject.transform.position, Scale, 0f, Vector2.down, 1.5f, Mask);
        isGrounded = (hit.collider != null);

        //Vector2 boxCenter = transform.position;
        //boxCenter.y -= SpriteRenderer.bounds.size.y / 2;

        //isGrounded = Physics2D.OverlapBox(boxCenter, Scale, 0f, Mask);
        //DebugCollision(boxCenter, Scale, isGrounded);

        DebugCollision(gameObject.transform.position, gameObject.transform.localScale, isGrounded);

        if (direction != 0)
        {
            Player.transform.position += Vector3.right * direction * Time.deltaTime * Speed;
            Animator.SetBool("IsWalking", true);

            if (direction > 0)
            {
                transform.localScale = RightFacing;
            }
            else if (direction < 0)
            {
                transform.localScale = LeftFacing;
            }
        }
        else
        {
            Animator.SetBool("IsWalking", false);
        }

        if (jump && isGrounded)
        {
            Player.AddForce(JumpForce, ForceMode2D.Impulse);
            Animator.SetBool("IsJumping", true);
        }

        if (gameObject.transform.position.y < -5)
        {
            RespawnPlayer();
        }

        Debug.Log(direction);
        Animator.SetBool("IsJumping", !isGrounded);
    }

    // called when the cube hits the floor
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        
        if(collision.gameObject.layer == 9)
        {
            Animator.SetBool("IsDieing", true);
           
        }
    }

    public void RespawnPlayer()
    { 
        gameObject.transform.position = spawmpoint.transform.position;
        Animator.SetBool("IsDieing", false);
    }

    private void DebugCollision(Vector2 boxCenter, Vector2 boxSize, bool successful)
    {
        Vector2 t_tL = boxCenter + new Vector2(-boxSize.x * 0.5f, boxSize.y * 0.5f);
        Vector2 t_tR = boxCenter + new Vector2(boxSize.x * 0.5f, boxSize.y * 0.5f);
        Vector2 t_bL = boxCenter + new Vector2(-boxSize.x * 0.5f, -boxSize.y * 0.5f);
        Vector2 t_bR = boxCenter + new Vector2(boxSize.x * 0.5f, -boxSize.y * 0.5f);
        Color t_lineColor = (successful) ? Color.green : Color.red;

        Debug.DrawLine(t_tL, t_tR, t_lineColor);
        Debug.DrawLine(t_bL, t_bR, t_lineColor);
        Debug.DrawLine(t_tL, t_bL, t_lineColor);
        Debug.DrawLine(t_tR, t_bR, t_lineColor);
    }
}
