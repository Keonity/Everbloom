using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [Header("For Patrolling")]
    [SerializeField] float moveSpeed;
    private float moveDirection;
    private bool facingRight = true;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] float circleRadius;
    [SerializeField] LayerMask groundLayer;
    private bool checkingGround;
    private bool checkingWall;

    [Header("For Jump Attacking")]
    [SerializeField] float jumpHeight;
    [SerializeField] Transform player;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 boxSize;
    private bool isGrounded;
    private float jumpTimer;
    public float jumpCD;

    [Header("For Seeing Player")]
    [SerializeField] Vector2 lineOfSight;
    [SerializeField] LayerMask playerLayer;
    private bool canSeePlayer;

    [Header("Other")]
    private Rigidbody2D enemyRB;
    private Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        moveDirection = -1;
        jumpTimer = 0;
    }

    private void FixedUpdate()
    {

        checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, groundLayer);
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer);
        canSeePlayer = Physics2D.OverlapBox(transform.position, lineOfSight, 0, playerLayer);

        AnimationController();

        if (!canSeePlayer && isGrounded)
        {
            Patrolling();
        }
        else if (canSeePlayer && jumpTimer <= 0)
        {
            jumpTimer = jumpCD;
            Debug.Log("Jump attack");
            FlipTowardsPlayer();
            JumpAttack();
        }

        jumpTimer -= Time.deltaTime;

        //Patrolling();

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpAttack();
        }*/

    }

    void Patrolling()
    {
        if (!checkingGround || checkingWall)
        {
            if (facingRight)
            {
                //Debug.Log("Flipping");
                Flip();
            }

            else if (!facingRight)
            {
                //Debug.Log("Flipping");
                Flip();
            }
        }

        enemyRB.velocity = new Vector2(moveSpeed * moveDirection, enemyRB.velocity.y);
    }

    void JumpAttack()
    {
        float distanceFromPlayer = player.position.x - transform.position.x;

        if (isGrounded)
        {
            enemyRB.velocity = new Vector2(0, 0);
            enemyRB.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
        }

    }

    void FlipTowardsPlayer()
    {
        float distanceFromPlayer = player.position.x - transform.position.x;

        if (distanceFromPlayer > 0 && facingRight)
        {
            Flip();
        }
        else if (distanceFromPlayer < 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    void AnimationController()
    {
        enemyAnimator.SetBool("canSeePlayer", canSeePlayer);
        enemyAnimator.SetBool("isGrounded", isGrounded);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, boxSize);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, lineOfSight);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
