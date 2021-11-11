using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GestureController : MonoBehaviour
{
    public float swipeSensitivity = 30;
    public float blastCharge = 5f;

    private PlayerMovement playerMove;
    private PlayerAttack playerAttk;
    private List<int> touchIDs = new List<int>();
    private float blastTimer = 0f;
    private bool blastCheck = false;
    private bool blastReset = false;

    void SpecialMoves()
    {
        blastCheck = false;

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        if (results.Count == 0)
        {
            foreach (Touch touch in Input.touches)
            {
                // DASH: Gets the horizontal direction of swipes.
                if ((touch.phase == TouchPhase.Moved) && !touchIDs.Contains(touch.fingerId) && (System.Math.Abs(touch.deltaPosition.x) > swipeSensitivity))
                {
                    touchIDs.Add(touch.fingerId);
                    playerMove.Dash(touch.deltaPosition.x);
                }
                if (touch.phase == TouchPhase.Ended && touchIDs.Contains(touch.fingerId))
                {
                    touchIDs.Remove(touch.fingerId);
                }

                // BLAST: Checks for "tap and hold" fingers.
                bool deadzone = (touch.phase == TouchPhase.Moved)
                    && (System.Math.Abs(touch.deltaPosition.x) <= swipeSensitivity)
                    && (System.Math.Abs(touch.deltaPosition.y) <= swipeSensitivity);
                if ((touch.phase == TouchPhase.Stationary || deadzone) && !touchIDs.Contains(touch.fingerId))
                {
                    //Debug.Log(touch.deltaTime);
                    blastCheck = true;
                    blastTimer -= Time.fixedDeltaTime;
                    if (blastTimer <= 0f && !blastReset)
                    {
                        playerAttk.Blast(touch.position);
                        blastReset = true;
                        //blastCheck = false;
                    }
                }
            }
        }

        if (!blastCheck)
        {
            blastTimer = blastCharge;
            blastReset = false;
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
