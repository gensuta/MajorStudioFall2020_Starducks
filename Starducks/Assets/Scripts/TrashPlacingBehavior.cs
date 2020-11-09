using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TrashPlayingBehavior : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {



    }

    public Vector3 GetMousePos()
    {
        Vector3 myPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myPos = new Vector3(myPos.x, myPos.y, 0f);
        return myPos;
    }

}