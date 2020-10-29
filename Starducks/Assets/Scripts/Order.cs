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

    public Order()
    {
        
    }

    public bool doesMatch(Order o)
    {
        // name part commented out for now bc we have no naming part
        if (this.myDrink.doesMatch(o.myDrink)) // && this.myName.Equals(o.myName)) 
        {
            return true;
        }
        return false;
    }
}
