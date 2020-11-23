using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TipsMenu : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    [TextArea(2, 5)]
    [SerializeField]
    string[] tipsText; // instructions

    [SerializeField]
    TextMeshProUGUI displayText;

    bool poppedUp;
    float lilTimer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        lilTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (lilTimer > 0) lilTimer -= Time.deltaTime;


        if (GameController.Instance.sc.IsSceneCalled("OrderGame"))
        {
            displayText.text = tipsText[0];
        }
        else if (GameController.Instance.sc.IsSceneCalled("BrewGame"))
        {
            displayText.text = tipsText[1];
        }
        else if (GameController.Instance.sc.IsSceneCalled("LidGame"))
        {
            displayText.text = tipsText[2];
        }
        else
        {
            displayText.text = " Helpful Instructions will be here :)";
        }
    }

    public void TogglePopUp()
    {
        if (lilTimer <= 0f)
        {
            if (poppedUp) GoBack();
            else
                PopUp();

            lilTimer = 1f;
        }
    }
    void PopUp()
    {
        anim.Play("PopUp", 0);
        poppedUp = true;
        GameController.Instance.isPaused = true;

    }

    void GoBack()
    {
        anim.Play("GoBack", 0);
        poppedUp = false;
        GameController.Instance.isPaused = false;
    }
}
