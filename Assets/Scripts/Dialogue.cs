using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue
{

    public string name; // The name of who/what we're talking to

    [TextArea(3, 10)]
    public string[] sentences; // The sentences of who/what we're talking to

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
