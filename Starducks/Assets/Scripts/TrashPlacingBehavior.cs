using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TrashPlacingBehavior : MonoBehaviour // player controls this!
{

    TrashManager tm;

    GameObject heldObj; // the object we're about to toss out
    Trash trash;

    // Start is called before the first frame update
    void Start()
    {
        tm = FindObjectOfType<TrashManager>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GetMousePos(); // this is a lil invisible gameObject that's handling collisions bc I'm lazy and it works -Geneva

        if(heldObj != null)
            heldObj.transform.position = GetMousePos();

        if (Input.GetMouseButtonUp(0))
        {
            heldObj = null;
        }
    }

    public Vector3 GetMousePos()
    {
        Vector3 myPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myPos = new Vector3(myPos.x, myPos.y, 0f);
        return myPos;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //For future Gen, please utilize the Trash class and make a script for the trash prefabs.don't be lazy!!
        if(collision.gameObject.GetComponent<Trash>() != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
               heldObj = collision.gameObject;
                trash = heldObj.GetComponent<Trash>();
            }
        }


    }

}