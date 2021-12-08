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
    public AudioSource chargeSoundSource;

    [Header("For Seeing Player")]
    [SerializeField] Vector2 lineOfSight;
    [SerializeField] LayerMask playerLayer;
    private bool canSeePlayer;

    [Header("Other")]
    private Rigidbody2D enemyRB;
    private Animator enemyAnimator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Fiore - Placeholder Sprite")
        {
            enemyRB.drag = 200f;
            enemyRB.mass = 200f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Fiore - Placeholder Sprite")
        {
            enemyRB.velocity = new Vector2(0, 0);
            enemyRB.drag = 0f;
            enemyRB.mass = 1f;
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
            Patrolling();
        }

        else if (canSeePlayer && chargeTimer <= 0)
        {
            chargeTimer = chargeCD;
            //Debug.Log("Jump attack");
            FlipTowardsPlayer();
            JumpAttack();
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

        //enemyRB.velocity = new Vector2(moveSpeed * moveDirection, enemyRB.velocity.y);
    }

    void JumpAttack()
    {
        float distanceFromPlayer = player.position.x - transform.position.x;

        if (distanceFromPlayer < 5f || distanceFromPlayer > -5f)
        {
            Debug.Log("Jump Attack");
            enemyRB.AddForce(new Vector2(distanceFromPlayer + 3f, 0), ForceMode2D.Impulse);
            chargeSoundSource.Play();

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

    }
}
