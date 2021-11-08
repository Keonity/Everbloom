using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GestureController : MonoBehaviour
{
    public PlayerMovement player;
    public float swipeSensitivity = 30;

    private List<int> touchIDs = new List<int>();

    void SpecialMoves()
    {
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
                    player.Dash(touch.deltaPosition.x);
                }
                if (touch.phase == TouchPhase.Ended && touchIDs.Contains(touch.fingerId))
                {
                    touchIDs.Remove(touch.fingerId);
                }
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpecialMoves();
    }
}
