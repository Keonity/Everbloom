using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;


    private float horizontalMove = 0f;
    private bool jump = false;
    //bool Moving = false;
    private float dash = 0f;
    private List<int> touchIDs = new List<int>();


    void SpecialMoves()
    {
        foreach(Touch touch in Input.touches)
        {
            // DASH: Gets the horizontal direction of swipes.
            if (touch.phase == TouchPhase.Moved && !touchIDs.Contains(touch.fingerId) && dash == 0f)
            {
                touchIDs.Add(touch.fingerId);
                dash = (touch.deltaPosition.x > 0) ? 1f : -1f;
            }
            if (touch.phase == TouchPhase.Ended && touchIDs.Contains(touch.fingerId))
            {
                touchIDs.Remove(touch.fingerId);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        SpecialMoves();
        
        if (SimpleInput.GetAxisRaw("Horizontal") != 0f)
        {
            //Moving = true;
            animator.SetBool("Moving", true);
        }
        else
        {
            //Moving = false;
            animator.SetBool("Moving", false);
        }

        horizontalMove = SimpleInput.GetAxisRaw("Horizontal"); // + Touches();
        if (horizontalMove < -1) horizontalMove = -1;
        else if (horizontalMove > 1) horizontalMove = 1;
        horizontalMove *= runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

    }

    void FixedUpdate()
    {

        if (SimpleInput.GetAxisRaw("Vertical") > 0f && jump == false)
        {
            jump = true;
        }

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump, dash);
        //Camera.main.transform.position = new Vector3(controller.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        jump = false;
        dash = 0f;
    }
}
