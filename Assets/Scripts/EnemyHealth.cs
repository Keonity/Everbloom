using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private bool canHit = true;
    public int enemyMaxHealth;
    [SerializeField]private int enemyHealth;

    public void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Attack" || trig.gameObject.tag == "Blast")
        {
            //Debug.Log("Attacked " + collision.gameObject.name);
            if (canHit == true)
            {
                if(trig.gameObject.tag == "Attack") enemyHealth -= 2;
                else if (trig.gameObject.tag == "Blast") enemyHealth -= 4;
            }

            StartCoroutine(HitCD());
            Debug.Log("Current Health: " + enemyHealth);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = enemyMaxHealth;
    }

    IEnumerator HitCD()
    {
        yield return new WaitForSeconds(1.045f);
        canHit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
