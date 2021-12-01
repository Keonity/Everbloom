using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GestureController : MonoBehaviour
{
    public float swipeSensitivity = 30;

    public float dashCooldown = 1f;

    public float blastCharge = 0.5f;
    public float blastCooldown = 3f;

    public Text dashText;
    public Text blastText;



    private PlayerMovement playerMove;
    private PlayerAttack playerAttk;
    
    private List<int> touchIDs = new List<int>();

    private float dashTimer = 0f;

    private float blastChargeTimer = 0f;
    private float blastCoolTimer = 0f;
    private bool blastCheck = false;
    private bool blastReset = false;

    void SpecialMoves()
    {
        blastCheck = false;
        bool touchingUI = false;

        foreach (Touch touch in Input.touches)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = touch.position;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            foreach (RaycastResult hit in results)
            {
                if (hit.gameObject.tag == "GestureBlock")
                {
                    touchingUI = true;
                    break;
                }
            }

            if (!touchingUI)
            {
                // DASH: Gets the horizontal direction of swipes.
                if ((dashTimer <= 0f)
                    && (touch.phase == TouchPhase.Moved)
                    && !touchIDs.Contains(touch.fingerId)
                    && (System.Math.Abs(touch.deltaPosition.x) > swipeSensitivity))
                {
                    touchIDs.Add(touch.fingerId);
                    playerMove.Dash(touch.deltaPosition.x);
                    dashTimer = dashCooldown;
                }
                if (touch.phase == TouchPhase.Ended && touchIDs.Contains(touch.fingerId))
                {
                    touchIDs.Remove(touch.fingerId);
                }

                // BLAST: Checks for "tap and hold" fingers.
                bool deadzone = (touch.phase == TouchPhase.Moved)
                    && (System.Math.Abs(touch.deltaPosition.x) <= swipeSensitivity)
                    && (System.Math.Abs(touch.deltaPosition.y) <= swipeSensitivity);
                if ((touch.phase == TouchPhase.Stationary || deadzone)
                    && !touchIDs.Contains(touch.fingerId))
                {
                    //Debug.Log(touch.deltaTime);
                    blastCheck = true;
                    if (blastCoolTimer <= 0f)
                    {
                        blastChargeTimer -= Time.deltaTime;
                    }
                    if (blastChargeTimer <= 0f && !blastReset)
                    {
                        //Debug.Log(touch.position);
                        playerAttk.Blast(touch.position);
                        blastReset = true;
                        blastCoolTimer = blastCooldown;
                    }
                }
            }
        }

        if (dashTimer > 0f)
        {
            dashTimer -= Time.deltaTime;
        }

        if (blastCoolTimer > 0f)
        {
            blastCoolTimer -= Time.deltaTime;
        }    
        if (!blastCheck)
        {
            blastChargeTimer = blastCharge;
            blastReset = false;
        }
        
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (dashTimer <= 0f) dashText.color = Color.green;
        else
        {
            float cval = (dashCooldown - dashTimer) / dashCooldown;
            dashText.color = new Color(cval, cval, cval);
        }

        //Debug.Log(blastChargeTimer);
        if (blastChargeTimer != blastCharge && blastChargeTimer > 0f)
        {
            float cval = (blastCharge - blastChargeTimer) / blastCharge;
            blastText.color = new Color(cval, 0f, 0f);
        }
        else if (blastCoolTimer <= 0f) blastText.color = Color.green;
        else
        {
            float cval = (blastCooldown - blastCoolTimer) / blastCooldown;
            blastText.color = new Color(cval, cval, cval);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        playerMove = gameObject.GetComponent<PlayerMovement>();
        playerAttk = gameObject.GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        SpecialMoves();
    }
}
