using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class CardSelect : MonoBehaviour
{
    private Vector3 startScale;
    private Vector3 startRotation;
    private int startOrder;
    private int scaling = 0;
    public float scaleAmount;
    private bool selected = false;
    float scaleTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        //gets all staring dimensions rotations and sorting orders
        startOrder = GetComponent<SpriteRenderer>().sortingOrder;
        startScale = transform.localScale;
        startRotation = transform.eulerAngles;
       
    }

    // Update is called once per frame
    void Update()
    {
       

    }


    private void OnMouseEnter() //invoked when mouse enters the collider of the card
    {
        if (selected == false)
        {
            //starts all hover effects
            transform.localScale = new Vector3(transform.localScale.x * 1.2f, transform.localScale.y * 1.2f, transform.localScale.z);
            transform.eulerAngles = new Vector3(0, 0, 0);
            GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        
    }

    private void OnMouseDown()
    {
        selected = true;
        Select();
    }
    private void OnMouseExit()//invoked when mouse exits the collider of the card
    {
        if (selected == false)
        {
            //ends all hover effects
            transform.localScale = startScale;
            GetComponent<SpriteRenderer>().sortingOrder = startOrder;
            transform.eulerAngles = startRotation;
        }
    }

    private void Select()//runs when a card is clicked on and therefore chosen
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()//lerps cards position to the center
    {
        Vector3 target = new Vector3(0, -1.5f, 0);
        float timeSinceStart = 0;
        float waitTime = 3f;
        while (timeSinceStart < waitTime)
        {
            transform.position = Vector3.Lerp(transform.position, target,0.08f);
            timeSinceStart += Time.deltaTime;
            yield return null;
        }
        transform.position =target;
        yield return null;
    }




}
