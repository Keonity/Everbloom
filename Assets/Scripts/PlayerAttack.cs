using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour
{
    public float blastSpeed = 10f;
    
    public GameObject hitBox;

    public Animator animator;
    public SoundControl soundcontroller;



    public Rigidbody2D blastPrefab;

    IEnumerator waitAttack()
    {
        yield return new WaitForSeconds(1);
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        if (results.Count > 0 && results[0].gameObject.name == "Attack")
        {
            animator.SetTrigger("Attack");
            waitAttack();
            animator.SetTrigger("Attack");
        }
        //Debug.Log("Touched: " + results[0]);
        return results.Count > 0;
    }

    public void Blast(Vector2 pos)
    {
        Vector3 playerPix = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 dir = (pos - new Vector2(playerPix.x, playerPix.y)).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Debug.Log(string.Format("{0} {1}", pos.ToString(), playerPix.ToString()));
        //Debug.DrawLine(transform.position, pos + dir * 10, Color.red, Mathf.Infinity);
        Rigidbody2D blast = GameObject.Instantiate(blastPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        blast.GetComponent<Rigidbody2D>().velocity = dir * blastSpeed;


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    /*private void FixedUpdate()
    {
        IsPointerOverUIObject();
    }*/

    // Update is called once per frame
    void Update()
    {

        /*if (!IsPointerOverUIObject())
        {
            //
        }*/

        // Code for OnMouseDown in the iPhone. Unquote to test.
        /*RaycastHit hit = new RaycastHit();
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                if (Physics.Raycast(ray, out hit))
                {
                    hit.transform.gameObject.SendMessage("OnMouseDown");
                    Debug.Log("Hit!");
                }
            }
        }*/

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                IsPointerOverUIObject();
                /*foreach (Transform child in this.transform)
                {
                    if (child.gameObject.name == "playerHitBox")
                    {
                        child.gameObject.SetActive(true);
                    }
                }*/
                waitAttack();
                /*foreach (Transform child in this.transform)
                {
                    if (child.gameObject.name == "playerHitBox")
                    {
                        child.gameObject.SetActive(false);
                    }
                }*/
            }
        }
    }
}
