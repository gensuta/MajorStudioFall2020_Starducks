using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupBehavior : MonoBehaviour
{
    public bool isLidded;
    public SpriteRenderer sr;
    public int orderNum;

    [SerializeField]
    float moveSpeed, minSpeed,maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
       // moveSpeed = Random.Range(minSpeed, maxSpeed); // uncomment this if you want randomness!
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.Instance.isPaused) transform.position += (Vector3.right * moveSpeed);
    }
}
