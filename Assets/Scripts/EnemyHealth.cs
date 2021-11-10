using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private bool canHit = true;
    public int enemyMaxHealth;
    [SerializeField]private int enemyHealth;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "playerHitBox")
        {
            //Debug.Log("Attacked " + collision.gameObject.name);
            if (canHit == true)
            {
                enemyHealth -= 2;
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