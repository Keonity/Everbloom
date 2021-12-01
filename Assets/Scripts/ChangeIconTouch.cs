using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeIconTouch : MonoBehaviour
{

    public Sprite regSprite;
    public Sprite downSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = touch.position;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            //Debug.Log(results.Count);
            foreach (RaycastResult hit in results)
            {
                Debug.Log(hit.gameObject.name + " / " + this.gameObject.name);
                if (hit.gameObject.Equals(this.gameObject))
                {
                    Debug.Log(hit.gameObject.name);
                    this.GetComponent<Image>().sprite = downSprite;
                    break;
                }
                else
                {
                    this.GetComponent<Image>().sprite = regSprite;
                }
            }
            /*
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {
                this.GetComponent<Image>().sprite = downSprite;
            }
            else
            {
                this.GetComponent<Image>().sprite = regSprite;
            }
            */
        }
    }

    
}
