using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private bool canHit;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit: " + collision.gameObject.name);

        if (collision.gameObject.layer == 8)
        {
            if (canHit == true)
            {
                //collision.gameObject.damage();
                canHit = false;
                StartCoroutine(HitCD());
            }
        }
    }

    IEnumerator HitCD()
    {
        yield return new WaitForSeconds(1.045f);
        canHit = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
