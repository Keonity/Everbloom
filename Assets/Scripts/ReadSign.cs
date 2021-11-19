using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReadSign : MonoBehaviour
{
    private bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                IsPointerOverUIObject();
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        if (results.Count > 0 && results[0].gameObject.name == "Interact")
        {
            if (inRange)
            {
                Debug.Log("Reading sign");
            }
        }
        //Debug.Log("Touched: " + results[0]);
        return results.Count > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sign")
        {
            inRange = collision.GetComponent<PlayerDetector>().inRange;
            Debug.Log("In range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sign")
        {
            inRange = collision.GetComponent<PlayerDetector>().inRange;
            Debug.Log("Out of range");
        }
    }
}
