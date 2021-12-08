using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float damageTime;
    public float damageIndicatorCD;
    private bool damageIndicator;
    private bool damageIndicatorOn;

    private bool canHit = true;
    public int enemyMaxHealth;
    public float enemyDeathAnimTime = 0f;
    [SerializeField]private int enemyHealth;

    public void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Attack" || trig.gameObject.tag == "Blast")
        {
            if (damageIndicator)
            {
                spriteRenderer.color = new Color(100, 0, 0);
                damageIndicatorOn = true;
            }

            //Debug.Log("Attacked " + collision.gameObject.name);
            if (canHit == true)
            {
                if (trig.gameObject.tag == "Attack") enemyHealth -= 2;
                else if (trig.gameObject.tag == "Blast")
                {
                    enemyHealth -= 4;
                    trig.gameObject.GetComponent<BlastController>().DestroyBlast();
                }
            }

            StartCoroutine(HitCD());
            Debug.Log("Current Health: " + enemyHealth);
            damageTime = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        enemyHealth = enemyMaxHealth;
    }

    IEnumerator HitCD()
    {
        yield return new WaitForSeconds(1.045f);
        canHit = true;
    }

    IEnumerator Die()
    {
        yield return new WaitForSecondsRealtime(enemyDeathAnimTime);
        this.gameObject.GetComponent<Animator>().SetBool("dead", true);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        damageTime += Time.deltaTime;


        if (damageTime >= damageIndicatorCD)
        {
            damageIndicator = true;
        }

        if (damageIndicatorOn && damageTime >= damageIndicatorCD)
        {
            spriteRenderer.color = new Color(255, 255, 255);
        }

        if (enemyHealth <= 0)
        {
            //Die();
            this.gameObject.GetComponent<Animator>().SetBool("dead", true);
            HitCD();
            Destroy(this.gameObject, enemyDeathAnimTime);
        }
    }
}
