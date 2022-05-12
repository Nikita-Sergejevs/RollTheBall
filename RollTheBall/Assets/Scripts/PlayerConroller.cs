using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConroller : MonoBehaviour
{
    public Text scoreText;
    private float speed;
    private Rigidbody playerRb;
    private int count;
    private GameObject[] games;
    private Vector3 startposition;
    private float boostTimer;
    private bool boosting;

    void Start()
    {
        playerRb = this.GetComponent<Rigidbody>();
        games = GameObject.FindGameObjectsWithTag("Spikes");
        startposition = playerRb.position;
        count = 0;
        UpdateScoreText();
        boostTimer = 0;
        boosting = false;
        speed = 1000;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveHorizontal, 0.0f, moveVertical);

        playerRb.AddForce(move * speed * Time.deltaTime);  

        if(boosting)
        {
            boostTimer += Time.deltaTime;
            if (boostTimer >= 5)
            {
                speed = 1000;
                boostTimer = 0;
                boosting = false;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Gem"))
        {
            collider.gameObject.SetActive(false);
            count++;
            UpdateScoreText();
        }
        else if(collider.gameObject.CompareTag("Spikes"))
        {
            restartGameState();
        }
        else if(collider.gameObject.CompareTag("Bonuse"))
        { 
            boosting = true;
            speed = 2000;
            collider.gameObject.SetActive(false);
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Твой счёт: " + count;
    }

    void restartGameState()
    {
        count = 0;
        UpdateScoreText();

        for(int i = 0; i < games.Length; i++)
        {
            games[i].gameObject.SetActive(true);
        }

        playerRb.position = startposition;
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
    }
}
