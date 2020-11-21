using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrewManager : MonoBehaviour // perhaps for the player?
{
    public BrewMachine[] brewMachines;


    GameController gc;

    [SerializeField]
    GameObject drinkPrefab, drinkHolder, orderPrefab,orderHolder;
    public int numSpawned;

    public List<GameObject> currentDrinks, currentOrders;// the ones that are displayed!

    [SerializeField]
    Slider timeSlider;

    [SerializeField]
    float baseTime, smallTime,mediumTime,largeTime, maxTime, currentTime;

    public int ordersComplete;
    void Start()
    {
        gc = GameController.Instance;
        gc.currentOrder = 0;
        maxTime = CalculateTime();
        currentTime = maxTime;
        timeSlider.maxValue = maxTime;

        currentOrders = new List<GameObject>();

        for (int i = 0; i < gc.orders.Count; i++)
        {
            currentOrders.Add(Instantiate(orderPrefab, Vector3.zero, Quaternion.identity, orderHolder.transform));
            currentOrders[i].GetComponentInChildren<TextMeshProUGUI>().text = gc.orders[i].myDrink.getDrinkName();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gc.isPaused)
        {
            currentTime -= Time.deltaTime;
            timeSlider.value = currentTime;

            if (currentTime <= 0f || ordersComplete == gc.maxOrders) GameController.Instance.sc.LoadScene("LidGame");


        }

      /*  if (gc.currentOrder < gc.orders.Count && numSpawned < 4)
        {
            SpawnDrink();
        }*/
    }

    float CalculateTime()
    {
        int smallCups = 0, mediumCups = 0, largeCups = 0;

        foreach(Order o in gc.orders)
        {
            if (o.myDrink.size == Size.small) smallCups++;
            if (o.myDrink.size == Size.medium) mediumCups++;
            if (o.myDrink.size == Size.large) largeCups++;
        }

        return baseTime + (smallCups * smallTime) +(mediumCups * mediumTime) + (largeCups * largeTime);
    }


    void SpawnDrink()
    {
        GameObject g = Instantiate(drinkPrefab, Vector3.zero, Quaternion.identity, drinkHolder.transform);
        currentDrinks.Add(g);
        DraggableCup d = g.GetComponent<DraggableCup>();
        d.myDrink = gc.orders[gc.currentOrder].myDrink;
        d.drinkNum = gc.currentOrder;
        gc.currentOrder++;
        numSpawned++;
    }
}
