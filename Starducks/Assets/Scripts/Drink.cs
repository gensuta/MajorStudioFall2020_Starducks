using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Drink", menuName = "Custom/Drink", order = 1)]

public class Drink : ScriptableObject
{
    
    public bool isCold; // if false, it's a hot drink
    public DrinkType drinkType;
    public Flavor flavor; // we can make this an array if we want to have multiple flavors
    public Size size;

    [SerializeField]
    string specialText;

    public static Drink CreateRandomDrink()
    {
        Drink d = new Drink();

        d.flavor = (Flavor)Random.Range(1, Flavor.max-Flavor.min +1);
        d.drinkType = (DrinkType)Random.Range(1, DrinkType.max-DrinkType.min+1);

        int n = Random.Range(0, 2);
        d.isCold = (n == 0);

        d.size = (Size)Random.Range(0, 3);
        return d;
    }

    public string getDrinkName()
    {
        string s;
        string temp = isCold ? "iced" : "hot";

        if (isCold)
        {
            if (!string.IsNullOrEmpty(specialText)) temp = specialText;
            s = size.ToString() + " " + temp + " " + flavor.ToString() + " " + drinkType.ToString();
        }
        else
            s = size.ToString() + flavor.ToString() + " " + drinkType.ToString();



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
public enum DrinkType { min,tea, coffee, latte, smoothie,max}; // we don't need these but in case we do want drink types
public enum Flavor { min,chocolate, lemon, cinammon,max }; // Prolly want to solidify these flavors - Geneva
public enum Size { small, medium, large }; // enum in case we want to change it to offbrand starbucks size names

