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
        ScoreHandler.sc.CalculateTotalScore();
        scoreTxt.text = "You earned $" + ConvertScore() + " in tips! ";
    }

    string ConvertScore()
    {
        return string.Format("{0:0.00}", ((float)ScoreHandler.sc.totalScore * 0.01));
    }

    // Update is called once per frame
    void Update()
    {
        if (endTimer < 0 && Input.GetMouseButtonDown(0))
        {
            int prevMax = GameController.Instance.maxOrders;
            GameController.Instance.orders.Clear();
            ScoreHandler.sc.totalScore = 0;
            GameController.Instance.sc.LoadScene("OrderGame");
            GameController.Instance.maxOrders = prevMax;
            GameController.Instance.currentOrder = 0;
        }
        else endTimer -= Time.deltaTime;
    }
}
