using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenHandler : MonoBehaviour
{
    [SerializeField]
    float endTimer;

    [SerializeField]
    TextMeshProUGUI scoreTxt;

    // Start is called before the first frame update
    void Start()
    {
        scoreTxt.text = "Your score is " + GameController.Instance.GetScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (endTimer < 0 && Input.GetKeyDown(KeyCode.Space))
        {
            GameController.Instance.orders.Clear();
            GameController.Instance.correct = 0;
            GameController.Instance.incorrect = 0;
            GameController.Instance.sc.LoadScene("StartScreen");
        }
        else endTimer -= Time.deltaTime;
    }
}
