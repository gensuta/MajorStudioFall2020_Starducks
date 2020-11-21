using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkSpawner : MonoBehaviour
{
    float timer, maxTime;
    public GameObject hotCup, coldCup;
    GameController gc;

    // in case you don't know, serializefields are for when you want to edit an object in the inspector
    // but you don't necessarily want it to be a public game object that other scripts can accidentally edit
    [SerializeField]
    Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameController.Instance;
        gc.currentOrder = 0; // setting back to 0 everytime we start a new minigame!!
    }

    // Update is called once per frame
    void Update()
    {
        if (!gc.isPaused)
        {
            if (gc.currentOrder < gc.orders.Count)
            {
                if (timer >= maxTime)
                {
                    if (gc.orders[gc.currentOrder].myDrink.isCold)
                    {
                        GameObject g = Instantiate(coldCup, spawnPos, Quaternion.identity);
                        CupBehavior b = g.GetComponent<CupBehavior>();
                        b.orderNum = gc.currentOrder;
                    }
                    else
                    {
                        GameObject g = Instantiate(hotCup, spawnPos, Quaternion.identity);
                        CupBehavior b = g.GetComponent<CupBehavior>();
                        b.orderNum = gc.currentOrder;
                    }
                    maxTime = Random.Range(0.75f, 2f);
                    gc.currentOrder++;
                    timer = 0;
                }
                timer += Time.deltaTime;
            }
        }
    }
}
