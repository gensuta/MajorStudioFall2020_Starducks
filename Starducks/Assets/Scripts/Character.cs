﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drink", menuName = "Custom/Character", order = 0)]
public class Character : ScriptableObject
{
    public List<Drink> preferredDrinks;
    public List<string> names; // more than one name bc sometimes ppl like to be funny at starbucks or ykno...trans tingz




    public string getRandName() // returns one of the names they like to use when ordering
    {
        return names[Random.Range(0, names.Count)];
    }

    public Drink getRandDrink() // returns one of their random preferred drinks
    {
        return preferredDrinks[Random.Range(0, preferredDrinks.Count)];
    }

    public void CreateRandomCharacter() // for if we want to create a random character instead of fine tuning one
    {
        preferredDrinks = new List<Drink>();
        names = new List<string>();

        int nameNum = Random.Range(1, 4);
        int drinkNum = Random.Range(1, 4);

        for (int i = 0; i < nameNum; i++)
            names.Add(GameController.Instance.GrabRandomName());

        for (int o = 0; o < nameNum; o++)
        {
            preferredDrinks.Add(Drink.CreateRandomDrink());
        }
    }

    public Order getOrder()
    {
        // making sure a drink is actually there first
        for (int i = 0; i < preferredDrinks.Count; i++)
        {
            if (preferredDrinks[i] == null)
            {
                preferredDrinks[i] = Drink.CreateRandomDrink();
            }
        }

        if (preferredDrinks.Count == 0) preferredDrinks.Add(Drink.CreateRandomDrink());

        if (names.Count == 0) names.Add(GameController.Instance.GrabRandomName());

        Order o = new Order(this,getRandName(),getRandDrink());
        return o;
    }
}
