using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Drink", menuName = "Custom/Drink", order = 1)]

public class Drink : ScriptableObject
{
    public bool canRandomizeSize; // 
    public bool isCold; // if false, it's a hot drink
    public DrinkType drinkType;
    public Flavor flavor; // we can make this an array if we want to have multiple flavors
    public Size size;

    [SerializeField]
    string specialText;

    public static Drink CreateRandomDrink()
    {
        Drink d = new Drink();

        d.flavor = (Flavor)Random.Range(1, (int)Flavor.max);
        d.drinkType = (DrinkType)Random.Range(1, (int)DrinkType.max);

        int n = Random.Range(0, 2);
        d.isCold = (n == 0);

        d.size = (Size)Random.Range(0, 3);
        return d;
    }

    public Drink()
    {

    }

    public Drink(Flavor f, Size s, DrinkType _d, bool _isCold)
    {
        Drink d = new Drink();
        d.flavor = f;
        d.size = s;
        d.drinkType = _d;
        d.isCold = _isCold;
    }

    public string getDrinkName()
    {
        string s;
        string temp = isCold ? "iced" : "hot";

        if (isCold)
        {
            if (!string.IsNullOrEmpty(specialText)) temp = specialText;
            s = size.ToString() + " " + temp + " " + ToFlavor(flavor) + " " + ToDrinkType(drinkType);
        }
        else
            s = size.ToString() + " " + ToFlavor(flavor) + " " + ToDrinkType(drinkType);



        return s;
    }

    public bool doesMatch(Drink d)
    {
        if (this.isCold == d.isCold && this.flavor == d.flavor && this.size == d.size &&
            this.drinkType == d.drinkType)
        {
            return true;
        }
        return false;
    }

    string ToFlavor(Flavor f) // placing this here because enums can't have spaces ;-;
    {
        if (f.ToString() == Flavor.SaltedCaramel.ToString())
            return "Salted Caramel";
        else if (f.ToString() == Flavor.PumpkinSpice.ToString())
            return "Pumpkin Spice";
        else
            return f.ToString();
    }

    string ToDrinkType(DrinkType d)
    {
        if (d.ToString() == DrinkType.ColdBrew.ToString())
            return "Cold Brew";
        else
            return d.ToString();
    }
}
public enum DrinkType { min, Coffee, Latte, ColdBrew, Cappuchino, max }; // we don't need these but in case we do want drink types
public enum Flavor { min, Mocha, Vanilla, SaltedCaramel, PumpkinSpice, max }; // Prolly want to solidify these flavors - Geneva
public enum Size { small, medium, large }; // enum in case we want to change it to offbrand starbucks size names
