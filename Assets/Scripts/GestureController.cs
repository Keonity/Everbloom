using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureController : MonoBehaviour
{
    private List<Touch> touch;





    List<Touch> Touches()
    {
        List<Touch> tList = new List<Touch>();
        foreach(Touch touch in Input.touches)
        {
            tList.Add(touch);
            /*
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
            */
        }
        return tList;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        touch = Touches();
        if(touch.Count != 0)
        {
            string msg = "";
            foreach(Touch t in touch)
            {
                if(t.deltaPosition != new Vector2(0, 0))
                {
                    msg += t.deltaPosition.ToString() + " ";
                }
            }
            
            
            if(msg != "") Debug.Log(msg);
        }
    }
}
