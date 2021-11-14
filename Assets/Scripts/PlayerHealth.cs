using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int health;
    public int numHearts;
    public float invincibilityCD;
    private float invincibilityTime;
    private bool canBeDamaged;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private SpriteRenderer spriteRenderer;
    public float damageIndicatorCD;
    private float damageTime;
    private bool damageIndicator;
    private bool damageIndicatorOn;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 && canBeDamaged)
        {
            if (damageIndicator)
            {
                spriteRenderer.color = new Color(100, 0, 0);
                damageIndicatorOn = true;
            }

            health--;
            damageTime = 0;
            invincibilityTime = 0;
            canBeDamaged = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        invincibilityTime += Time.deltaTime;
        damageTime += Time.deltaTime;

        if (damageTime >= damageIndicatorCD)
        {
            damageIndicator = true;
        }

        if (damageIndicatorOn && damageTime >= damageIndicatorCD)
        {
            spriteRenderer.color = new Color(255, 255, 255);
        }

        if (invincibilityTime >= invincibilityCD)
        {
            canBeDamaged = true;
        }


        if (health > numHearts)
        {
            health = numHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
