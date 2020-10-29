using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidPlacingBehavior : MonoBehaviour
{
    [SerializeField]
    Vector3 normPos, lidPos;

    bool isRightClicking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            isRightClicking = false;
            transform.position = lidPos;
        }
        else if (Input.GetMouseButton(1))
        {
            isRightClicking = true; // rightclick = hot cup lid
            transform.position = lidPos;
        }
        else
        {
            isRightClicking = false;
            transform.position = normPos;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<CupBehavior>() != null)
        {
            CupBehavior b = collision.GetComponent<CupBehavior>();

            if (!b.isLidded) // to ensure we can't lid em twice
            {
                if (collision.gameObject.tag == "hot")
                {
                    if(isRightClicking) b.sr.color = Color.green;
                    else b.sr.color = Color.yellow;
                }
                if (collision.gameObject.tag == "cold")
                {
                    if (!isRightClicking) b.sr.color = Color.green;
                    else b.sr.color = Color.yellow;
                }
                
                b.isLidded = true;
            }
        }
    }
}
