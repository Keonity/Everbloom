using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeOnStart : MonoBehaviour
{
    private Image image;
    private float i = 1;
    private float stayCounter;
    public float stayTime;

    // Start is called before the first frame update
    void Start()
    {
        stayCounter = 0;
        image = this.GetComponent<Image>();
        /*for (float i = 255; i >= 0; i -= Time.deltaTime)
        {
            this.GetComponent<Image>().color = new Color(255, 255, 255, i);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (stayCounter > stayTime)
        {
            i -= Time.deltaTime;
            image.color = new Color(255, 255, 255, i);
            if (i <= 0)
            {
                image.enabled = false;
            }
        }
        stayCounter += Time.deltaTime;
    }
}
