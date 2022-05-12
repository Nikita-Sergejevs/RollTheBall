using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConroller : MonoBehaviour
{
    public float speed;
    Rigidbody playerRb;
    public Text scoreText;
    private int count;

    void Start()
    {
        playerRb = this.GetComponent<Rigidbody>();
        count = 0;
        UpdateScoreText();
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
            UpdateScoreText ();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Твой счёт: " + count;
    }
}
