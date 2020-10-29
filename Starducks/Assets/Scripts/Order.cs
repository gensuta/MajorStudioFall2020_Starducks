using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Order 
{
    public Character myCharacter;
    public string myName; // the name the character decided to use with this order
    public Drink myDrink;

    public Order(Character c, string s, Drink d)
    {
        myCharacter = c;
        myName = s;
        myDrink = d;
    }
}
