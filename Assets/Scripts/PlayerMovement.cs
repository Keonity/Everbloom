using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    bool jump = false;

    int Touches()
    {
        int touchAxis = 0;
        foreach(Touch touch in Input.touches)
        {
            Vector2 touchPos = touch.position - new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
            float ratio = touchPos.x / (float)Camera.main.scaledPixelWidth;
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
            {
                if (ratio <= 0.33)
                {
                    touchAxis--;
                }
                else if (ratio >= 0.66)
                {
                    touchAxis++;
                }
                else if (touch.phase == TouchPhase.Began)
                {
                    jump = true;
                }

            }
        }

        return touchAxis;
    }

    // Update is called once per frame
    void Update()
    {
        Touches();

        horizontalMove = Input.GetAxisRaw("Horizontal") + Touches();
        if (horizontalMove < -1) horizontalMove = -1;
        else if (horizontalMove > 1) horizontalMove = 1;
        horizontalMove *= runSpeed;


        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        Camera.main.transform.position = new Vector3(controller.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        jump = false;
    }
}
