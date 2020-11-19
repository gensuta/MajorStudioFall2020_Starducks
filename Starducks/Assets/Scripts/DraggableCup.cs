using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableCup : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler // in the brew game. The cup you drag onto the brew machine
{
    public Drink myDrink;
    public int drinkNum;

    bool outOfPanel;

    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta;
        if (!outOfPanel)
        {
            transform.parent = transform.parent.parent;
            outOfPanel = true;
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(myDrink.getDrinkName());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (collision.GetComponent<BrewMachine>() != null)
            {
                collision.GetComponent<BrewMachine>().PlaceCup(this);
            }

        }
    }

    public Vector3 GetMousePos()
    {
        Vector3 myPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myPos = new Vector3(myPos.x, myPos.y, 0f);
        return myPos;
    }
}
