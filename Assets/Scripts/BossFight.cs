using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossFight : MonoBehaviour
{

    private GameObject boss1;
    private GameObject boss2;
    private GameObject boss3;
    private GameObject boss4;
    private Queue<GameObject> bosses;
    private GameObject[] bossList = new GameObject[4];
    private int bossCount = 0;
    private bool bossStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        boss1 = GameObject.Find("Wolf (4)");
        boss2 = GameObject.Find("Wolf (5)");
        boss3 = GameObject.Find("Wolf (6)");
        boss4 = GameObject.Find("Wolf (7)");
        boss1.SetActive(false);
        boss2.SetActive(false);
        boss3.SetActive(false);
        boss4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if (bossStarted)
        {
            if (!boss1)
            {
                if (!boss2)
                {
                    if (!boss3)
                    {
                        if (!boss4)
                        {
                            GameObject.Find("End Of Game").GetComponent<Animator>().SetBool("IsOpen", true);
                        }
                    }
                }
            }
        }
    }

    IEnumerator StartFight()
    {
        GameObject.Find("Fiore").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 0;
        boss1.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        boss2.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        boss3.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        boss4.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        GameObject.Find("Fiore").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GameObject.Find("Fiore").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        Time.timeScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !bossStarted)
        {
            StartCoroutine("StartFight");
            bossStarted = true;
            //Debug.Log("Dequeued");
            //bossCount++;
        }
    }
}
