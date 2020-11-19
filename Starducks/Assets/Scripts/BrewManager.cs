﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrewManager : MonoBehaviour // perhaps for the player?
{
    public BrewMachine[] brewMachines;


    GameController gc;

    [SerializeField]
    GameObject drinkPrefab, drinkHolder;
    public int numSpawned;

    public List<GameObject> currentDrinks;// the ones that are displayed!

    [SerializeField]
    Slider timeSlider;

    [SerializeField]
    float baseTime, smallTime,mediumTime,largeTime, maxTime, currentTime;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        gc.currentOrder = 0;
        maxTime = CalculateTime();
        currentTime = maxTime;
        timeSlider.maxValue = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        timeSlider.value = currentTime;
        
        if(gc.currentOrder < gc.orders.Count)
        {
            if(numSpawned < 4)
            {
                SpawnDrink();
            }
        }
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
        numSpawned++;
        gc.currentOrder++;
    }
}
