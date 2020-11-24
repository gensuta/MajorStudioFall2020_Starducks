using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidPlacingBehavior : MonoBehaviour
{
    [SerializeField]
    Vector3 normPos, lidPos;

    bool isRightClicking;
    GameController gc;

    [SerializeField]
    float transitionTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameController.Instance;
        gc.tipMenu.TogglePopUp();
    }

    // Update is called once per frame
    void Update()
    {

        if (!gc.isPaused)
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
            if (gc.currentOrder >= gc.orders.Count)
            {
                DrinkSpawner ds = FindObjectOfType<DrinkSpawner>();
                if (Vector3.Distance(ds.lastObj.transform.position, transform.position) > 5f)
                {
                    transitionTime -= Time.deltaTime;
                    if (transitionTime < 0f)
                        gc.sc.LoadScene("EndScreen");
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) // score based on distance parent lid to cup
    {
        if (collision.GetComponent<CupBehavior>() != null)
        {
            CupBehavior b = collision.GetComponent<CupBehavior>();

            if (!b.isLidded) // to ensure we can't lid em twice
            {
                if (collision.gameObject.tag == "hot")
                {
                    if (isRightClicking)
                    {
                        b.sr.color = Color.green;
                        gc.orders[b.orderNum].scores[2].CorrectMove();
                        gc.orders[b.orderNum].scores[2].LidGameBonus(collision.transform.position.x, transform.position.x);
                    }
                    else
                    {
                        b.sr.color = Color.yellow;
                        gc.orders[b.orderNum].scores[2].IncorrectMove();
                    }
                }
                if (collision.gameObject.tag == "cold")
                {
                    if (!isRightClicking)
                    {
                        b.sr.color = Color.green;
                        gc.orders[b.orderNum].scores[2].CorrectMove();
                        gc.orders[b.orderNum].scores[2].LidGameBonus(collision.transform.position.x, transform.position.x);
                    }
                    else
                    {
                        b.sr.color = Color.yellow;
                        gc.orders[b.orderNum].scores[2].IncorrectMove();
                    }
                }

                b.isLidded = true;
            }
        }
    }
}
