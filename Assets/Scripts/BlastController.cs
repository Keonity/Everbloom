using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastController : MonoBehaviour
{
    public float aliveTime = 2f;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Interactive")
        {
            DestroyBlast();
        }
    }

    public void DestroyBlast()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        aliveTime -= Time.deltaTime;
        if (aliveTime <= 0f) DestroyBlast();
    }
}
