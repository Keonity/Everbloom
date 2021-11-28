using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetector : MonoBehaviour
{
    public bool inRange;
    public Button button;
    public Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
        button.enabled = false;
        button.image.color = new Color(0, 0, 0, 0);
        buttonText.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            button.enabled = true;
            button.image.color = new Color(255, 255, 255, 255);
            buttonText.color = new Color(0, 0, 0, 255);
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            button.enabled = false;
            button.image.color = new Color(0, 0, 0, 0);
            buttonText.color = new Color(0, 0, 0, 0);
            inRange = false;
        }
    }
}
