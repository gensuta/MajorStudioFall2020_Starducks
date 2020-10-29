using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drink", menuName = "Custom/Drink", order = 1)]

public class Drink : ScriptableObject
{
    
    public bool isCold; // if false, it's a hot drink
    public DrinkType drinkType;
    public Flavor flavor; // we can make this an array if we want to have multiple flavors
    public Size size;

    public static Drink CreateRandomDrink()
    {
        Drink d = new Drink();
        d.flavor = (Flavor)Random.Range(0, 3);
        d.drinkType = (DrinkType)Random.Range(0, 4);
        int n = Random.Range(0, 2);
        d.isCold = (n == 0);
        d.size = (Size)Random.Range(0, 3);
        return d;
    }

    public string getDrinkName()
    {
        string temp = isCold ? "iced" : "hot";
        string s = size.ToString() + " " + temp + " " + flavor.ToString() + " " + drinkType.ToString();

        return s;
    }

    public bool doesMatch(Drink d)
    {
        if(this.isCold == d.isCold && this.flavor == d.flavor && this.size == d.size &&
            this.drinkType == d.drinkType)
        {
            return true;
        }
        return false;
    }
}
public enum DrinkType { tea, coffee, latte, smoothie }; // we don't need these but in case we do want drink types
public enum Flavor { chocolate, lemon, cinammon }; // Prolly want to solidify these flavors - Geneva
public enum Size { small, medium, large }; // enum in case we want to change it to offbrand starbucks size names

