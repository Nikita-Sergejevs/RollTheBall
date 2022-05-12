using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConroller : MonoBehaviour
{
    public float speed;
    public Text scoreText;

    private Rigidbody playerRb;
    private int count;
    private GameObject[] games;
    private Vector3 startposition;

    void Start()
    {
        playerRb = this.GetComponent<Rigidbody>();
        games = GameObject.FindGameObjectsWithTag("Spikes");
        startposition = playerRb.position;
        count = 0;
        UpdateScoreText();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveHorizontal, 0.0f, moveVertical);

        playerRb.AddForce(move * speed * Time.deltaTime);  
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Gem"))
        {
            collider.gameObject.SetActive(false);
            count++;
            UpdateScoreText();
        }
        else if(collider.gameObject.CompareTag("Win"))
            restartGameState();
        else if(collider.gameObject.CompareTag("Spikes"))
        {
            restartGameState();
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
