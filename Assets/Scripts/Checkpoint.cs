using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private GameMaster gm;

    public Animator animator;

    public Sprite LitLamp;
    public Sprite UnlitLamp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.GetComponent<Animator>().enabled = true;
            gm.lastCheckpointPos = transform.position;
            //this.GetComponent<SpriteRenderer>().sprite = LitLamp;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
