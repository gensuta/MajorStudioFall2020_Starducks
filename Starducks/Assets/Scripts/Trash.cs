using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour // don't know what we specifically need with this yet. lmk know - Geneva
{
    public TrashType myTrashtype;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (collision.gameObject.tag == "paper")
            {
                if (myTrashtype == TrashType.paper)
                    Debug.Log("Noice!");
                else
                    Debug.Log("Not cool bruv");
                Destroy(gameObject);
            }
            if (collision.gameObject.tag == "plastic")
            {
                if (myTrashtype == TrashType.plastic)
                    Debug.Log("Noice!");
                else
                    Debug.Log("Not cool bruv");
                Destroy(gameObject);
            }
            if (collision.gameObject.tag == "compost")
            {
                if (myTrashtype == TrashType.compost)
                    Debug.Log("Noice!");
                else
                    Debug.Log("Not cool bruv");
                Destroy(gameObject);
            }
        }
    }
}
public enum TrashType { plastic, compost, paper };