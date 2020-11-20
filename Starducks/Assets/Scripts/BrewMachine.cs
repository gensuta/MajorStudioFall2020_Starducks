using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BrewMachine : MonoBehaviour
{
    [SerializeField]
    bool isOn;
    [SerializeField]
    Drink rightDrink;

    [SerializeField]
    Slider drinkAmount; // this is what goes up when the brewMachine is on
    [SerializeField]
    Image sliderImg,stopLine;

    public int currentFlavor = 1;
    [SerializeField]
    float[] stopPoints,stopPositions;// where to stop for the brew machine depending on the size of the drink. prolly needs a better name

    [SerializeField]
    TextMeshProUGUI flavorText;

    BrewManager bm;

    DraggableCup currentCup;

    bool isCold;


    void Start()
    {
        bm = FindObjectOfType<BrewManager>();
        flavorText.text = ToFlavor(currentFlavor);
        drinkAmount.maxValue = stopPoints[stopPoints.Length-1] + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn) drinkAmount.value += Time.deltaTime;
    }

    public void PlaceCup(DraggableCup c) // when mouse lets go of a cup over a brew machine
    {
        currentCup = c;
        rightDrink = c.myDrink;
        sliderImg.color = Color.gray;
        c.enabled = false;
    }

    string ToFlavor(int flavorNum) // placing this here because enums can't have spaces ;-;
    {
        Flavor f = (Flavor)flavorNum;
        if (f.ToString() == Flavor.SaltedCaramel.ToString())
            return "Salted Caramel";
        else if (f.ToString() == Flavor.PumpkinSpice.ToString())
            return "Pumpkin Spice";
        else
            return f.ToString();
    }

    public void IcedButtonPressed()
    {
        isCold = true;
    }
    public void HotButtonPressed()
    {
        isCold = false;
    }
    public void NextFlavor()
    {
        if(currentFlavor < (int)Flavor.max -1) currentFlavor++;
        flavorText.text = ToFlavor(currentFlavor);
    }
    public void PrevFlavor()
    {
        if (currentFlavor > (int)Flavor.min + 1) currentFlavor--;
        flavorText.text = ToFlavor(currentFlavor);
    }

    public void Brew()
    {
        if (!isOn)
        {
            isOn = true;
            stopLine.gameObject.SetActive(true);
            PlaceStopLine();
        }
        else
        {
            isOn = false;
            stopLine.gameObject.SetActive(false);

        }
    }

    void PlaceStopLine()
    {
        switch(rightDrink.size)
        {
            case (Size.small):
                stopLine.rectTransform.anchoredPosition = GetNewAnchoredPos(Size.small);
                break;
            case (Size.medium):
                stopLine.rectTransform.anchoredPosition = GetNewAnchoredPos(Size.medium);
                break;
            case (Size.large):
                stopLine.rectTransform.anchoredPosition = GetNewAnchoredPos(Size.large);
                break;
        }
    }

    Vector3 GetNewAnchoredPos(Size s)
    {
        Vector3 newPos;
        newPos = stopLine.rectTransform.anchoredPosition;

        float x = stopPositions[(int)s];

        Debug.Log(x);

        newPos = new Vector3(x, newPos.y, newPos.z);
        return newPos;
    }

    public void Serve()
    {
        Debug.Log(Mathf.Abs(drinkAmount.value - stopPoints[(int)rightDrink.size]));

        Drink newDrink = new Drink();
        newDrink.flavor = (Flavor)currentFlavor;
        newDrink.size = rightDrink.size;
        newDrink.drinkType = rightDrink.drinkType;
        newDrink.isCold = isCold;

        Debug.Log(newDrink.getDrinkName());

        if (rightDrink.doesMatch(newDrink) && (Mathf.Abs(drinkAmount.value - stopPoints[(int)rightDrink.size]) < 0.7f))
        {
            GameController.Instance.correct++;
            Debug.Log("nice");
        }
        else
        {
            GameController.Instance.incorrect++;
            Debug.Log("not nice");
        }

        drinkAmount.value = 0;
        Destroy(bm.currentDrinks[currentCup.drinkNum]);
        bm.numSpawned--;

        sliderImg.color = Color.white;
        bm.ordersComplete++;
    }

    public void Trash()
    {
        drinkAmount.value = 0;
        sliderImg.color = Color.white;
        Destroy(bm.currentDrinks[currentCup.drinkNum]);
    }
}
