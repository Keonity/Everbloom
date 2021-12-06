using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : MonoBehaviour
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

    [Header("For Charging")]
    [SerializeField] Transform player;
    //[SerializeField] Transform groundCheck;
    //[SerializeField] Vector2 boxSize;
    private float chargeTimer;
    public float chargeCD;

    [Header("For Seeing Player")]
    [SerializeField] Vector2 lineOfSight;
    [SerializeField] LayerMask playerLayer;
    private bool canSeePlayer;

    [Header("Other")]
    private Rigidbody2D enemyRB;
    private Animator enemyAnimator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Fiore")
        {
            enemyRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            enemyRB.velocity = Vector2.zero;
            enemyRB.drag = 200000f;
            enemyRB.mass = 200000f;
            enemyRB.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Fiore")
        {
            FlipTowardsPlayer();
            enemyRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            enemyRB.velocity = new Vector2(moveSpeed * moveDirection, enemyRB.velocity.y);
            //enemyRB.velocity = new Vector2(0, 0);
            enemyRB.drag = 0f;
            enemyRB.mass = 1f;
            enemyRB.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        moveDirection = -1;
        chargeTimer = 0;
    }

    private void FixedUpdate()
    {

        checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, groundLayer);
        canSeePlayer = Physics2D.OverlapBox(transform.position, lineOfSight, 0, playerLayer);

        AnimationController();

        if (!canSeePlayer)
        {
            //Patrolling();
            FlipTowardsPlayer();
            enemyRB.constraints = RigidbodyConstraints2D.FreezePositionX;
            //enemyRB.velocity = new Vector2(moveSpeed * moveDirection, 0);
        }

        else if (canSeePlayer && chargeTimer <= 0)
        {
            enemyRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            chargeTimer = chargeCD;
            //Debug.Log("Jump attack");
            FlipTowardsPlayer();
            JumpAttack();
        }

        else if (canSeePlayer && chargeTimer > 0)
        {
            enemyRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            FlipTowardsPlayer();
            enemyRB.velocity = new Vector2(moveSpeed * moveDirection, 0);
        }

        chargeTimer -= Time.deltaTime;

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

        enemyRB.velocity = new Vector2(0, 0);
        enemyRB.velocity = new Vector2(moveSpeed * moveDirection, enemyRB.velocity.y);
    }

    void JumpAttack()
    {
        float distanceFromPlayer = player.position.x - transform.position.x;

        if (distanceFromPlayer < 5f || distanceFromPlayer > -5f)
        {
            Debug.Log("Jump Attack");
            enemyRB.velocity = new Vector2(1.5f * moveSpeed * moveDirection, enemyRB.velocity.y);
            //enemyRB.AddForce(new Vector2(distanceFromPlayer, 0), ForceMode2D.Impulse);

        }

        else if (distanceFromPlayer < 10f || distanceFromPlayer > -10f)
        {
            Debug.Log("Moving");
            enemyRB.velocity = new Vector2(moveSpeed * moveDirection, enemyRB.velocity.y);
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
        enemyRB.velocity = Vector2.zero;
        enemyRB.drag = 200000f;
        enemyRB.mass = 200000f;
        enemyRB.drag = 0f;
        enemyRB.mass = 1f;
    }

    void AnimationController()
    {
        enemyAnimator.SetBool("canSeePlayer", canSeePlayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, lineOfSight);

    }

    // Update is called once per frame
    void Update()
    {
        if (canSeePlayer)
        {
            FlipTowardsPlayer();
        }

    }
}
