using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConroller : MonoBehaviour
{
    public float speed;
    Rigidbody playerRb;

    void Start()
    {
        playerRb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        playerRb.AddForce (move * speed * Time.deltaTime);  
    }
}
