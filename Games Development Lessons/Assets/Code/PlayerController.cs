using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Player;
    [SerializeField] private Animator Animator;
    [SerializeField] private SpriteRenderer SpriteRenderer;

    [SerializeField] private float Speed;
    [SerializeField] private Vector2 JumpForce;

    [SerializeField] private GameObject spawmpoint;


    [SerializeField] private LayerMask Mask;
    private bool isGrounded = false;

    private Vector3 RightFacing = new Vector3(1, 1, 1);
    private Vector3 LeftFacing = new Vector3(-1, 1, 1);

    [SerializeField] private Vector2 Scale;

    private bool Playerhascontrol = true;

    public float UpwardsGravityModifier = 1;
    public float DownwardsGravityModifier = 1;
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
        if (Playerhascontrol == true)
        {
            isGrounded = IsGrounded();

            Run();
            Jump();
            adjustGravityModifier();

            if (gameObject.transform.position.y < -5)
            {
                RespawnPlayer();
                Playerhascontrol = false;
            }

            Animator.SetBool("IsJumping", !isGrounded);
        }
    }

    private void adjustGravityModifier()
    {
        if (Player.velocity.y > 0)
        {
            Player.gravityScale = UpwardsGravityModifier;
        }
        else if (Player.velocity.y<0)
        {
            Player.gravityScale = DownwardsGravityModifier;
        }
    }

    private void Run()
    {
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
    }

    private void Jump()
    {

        if (jump && isGrounded)
        {
            Player.AddForce(JumpForce, ForceMode2D.Impulse);
            Animator.SetBool("IsJumping", true);
        }

    }

    private bool IsGrounded()
    {
        Vector2 boxCenter = transform.position;
        boxCenter.y -= SpriteRenderer.bounds.size.y / 2;

        bool grounded = Physics2D.OverlapBox(boxCenter, Scale, 0f, Mask);
        DebugCollision(boxCenter, Scale, grounded);

        return grounded;
    }


    // called when the cube hits the floor
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        
        if(collision.gameObject.layer == 9)
        {
            Animator.SetBool("IsDieing", true);
            Playerhascontrol = false;
        }
    }

    public void RespawnPlayer()
    {
        Playerhascontrol = true;
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
