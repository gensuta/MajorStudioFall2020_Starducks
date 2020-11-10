using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupBehavior : MonoBehaviour
{
    public bool isLidded;
    public SpriteRenderer sr;

    [SerializeField]
    float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = Random.Range(0.07f, 0.1f);
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3.right * moveSpeed);
    }
}
