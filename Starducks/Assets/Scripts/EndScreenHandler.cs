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
        scoreTxt.text = "Your score is " + ScoreHandler.sc.GetScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (endTimer < 0 && Input.GetKeyDown(KeyCode.Space))
        {
            GameController.Instance.orders.Clear();
            ScoreHandler.sc.correct = 0;
            ScoreHandler.sc.incorrect = 0;
            GameController.Instance.sc.LoadScene("StartScreen");
        }
        else endTimer -= Time.deltaTime;
    }
}
