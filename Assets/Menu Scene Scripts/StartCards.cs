using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCards : MonoBehaviour
{
    private Vector3 startScale;
    private Vector3 startRotation;
    private Vector3 startPos;
    private int startOrder;


    private bool selected = false;
    private float scaleTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        //gets all staring dimensions rotations and sorting orders
        startOrder = GetComponent<SpriteRenderer>().sortingOrder;
        startScale = transform.localScale;
        startRotation = transform.eulerAngles;
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {


    }


    private void OnMouseEnter() //invoked when mouse enters the collider of the card
    {

        //starts all hover effects
        transform.localScale = new Vector3(transform.localScale.x * 1.2f, transform.localScale.y * 1.2f, transform.localScale.z);
        transform.eulerAngles = new Vector3(0, 0, 0);
        GetComponent<SpriteRenderer>().sortingOrder = 2;


    }

    private void OnMouseDown()
    {
        MenuController.instance.CardSelect(int.Parse(tag));
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


    IEnumerator Move(Vector3 target)//lerps cards position to the target
    {

        float timeSinceStart = 0;
        float waitTime = 0.5f;
        while (timeSinceStart < waitTime)
        {
            transform.position = Vector3.Lerp(transform.position, target, 0.08f);
            timeSinceStart += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
        yield return null;
    }
}
