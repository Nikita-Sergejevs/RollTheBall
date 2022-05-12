using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConroller : MonoBehaviour
{
    public float speed;
    Rigidbody playerRb;
    private int count;

    void Start()
    {
        playerRb = this.GetComponent<Rigidbody>();
        count = 0;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        playerRb.AddForce (move * speed * Time.deltaTime);  
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Gem"))
        {
            collider.gameObject.SetActive(false);
            count++;
            Debug.Log("Gem count is:" + count);
        }
    }
}
