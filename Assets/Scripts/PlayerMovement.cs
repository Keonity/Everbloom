using System.Collections;
using System.Collections.Generic;
//using System.Math;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public SoundControl soundmanager;
    public float runSpeed = 40f;
    public float dashCooldown = 1f;


    private float horizontalMove = 0f;
    private bool jump = false;
    //bool Moving = false;
    private float dash = 0f;
    private float dashTimer = 0f;



    public void Dash(float dir)
    {
        if (dash == 0f && dashTimer <= 0f)
        {
            dash = dir / System.Math.Abs(dir);
            dashTimer = dashCooldown;
        }

    }

    // Update is called once per frame
    void Update()
    {
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

        if (SimpleInput.GetAxisRaw("Vertical") > 0f && jump == false)
        {
            jump = true;
        }

        horizontalMove = SimpleInput.GetAxisRaw("Horizontal"); // + Touches();
        if (horizontalMove < -1) horizontalMove = -1;
        else if (horizontalMove > 1) horizontalMove = 1;
        horizontalMove *= runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));


        //Camera.main.transform.position = new Vector3(controller.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump, dash);
        jump = false;
        dash = 0f;
        if (dashTimer != 0f) dashTimer -= Time.fixedDeltaTime;
    }
}
