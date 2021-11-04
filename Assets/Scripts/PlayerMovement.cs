using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public SoundControl soundmanager;
    public float runSpeed = 40f;


    private float horizontalMove = 0f;
    private bool jump = false;
    //bool Moving = false;
    private float dash = 0f;
    private List<int> touchIDs = new List<int>();


    void SpecialMoves()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        if(results.Count == 0)
        {
            foreach (Touch touch in Input.touches)
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

        if (SimpleInput.GetAxisRaw("Vertical") > 0f && jump == false)
        {
            jump = true;
        }

        horizontalMove = SimpleInput.GetAxisRaw("Horizontal"); // + Touches();
        if (horizontalMove < -1) horizontalMove = -1;
        else if (horizontalMove > 1) horizontalMove = 1;
        horizontalMove *= runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));


        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump, dash);
        //Camera.main.transform.position = new Vector3(controller.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        jump = false;
        dash = 0f;
    }

    void FixedUpdate()
    {


    }
}
