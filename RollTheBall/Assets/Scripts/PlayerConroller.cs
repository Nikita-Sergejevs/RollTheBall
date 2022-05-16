using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConroller : MonoBehaviour
{
    public Text scoreText;
    public Text StarText;
    public Text WinText;
    public Button playAgainButton;

    private Rigidbody playerRb;

    public GameObject UiObject;
    public GameObject Finish;
    private GameObject[] gems;
    private GameObject[] stars;
    private GameObject[] bonuses;
    private GameObject[] debufs;

    private Vector3 startposition;

    private float boostTimer;
    private float speed;
    private float slowdownTimer;

    private bool boosting;
    private bool slowdown;
    private bool gameStarted;

    private int count;
    private int countstars;

    void Start()
    {
        playerRb = this.GetComponent<Rigidbody>();
        gems = GameObject.FindGameObjectsWithTag("Gem");
        stars = GameObject.FindGameObjectsWithTag("Star");
        debufs = GameObject.FindGameObjectsWithTag("Debuf");
        bonuses = GameObject.FindGameObjectsWithTag("Bonuse");
        startposition = playerRb.position;
        playAgainButton.onClick.AddListener(playAgainButtonAction);
        gameStarted = true;
        count = 0;
        countstars = 0;
        UpdateScoreText();
        UpdateStarText();
        UiObject.SetActive(false);
        Finish.SetActive(false);
        WinText.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        boostTimer = 0;
        boosting = false;
        speed = 1000;
    }

    void Update()
    {
        if(gameStarted)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(moveHorizontal, 0.0f, moveVertical);

            playerRb.AddForce(move * speed * Time.deltaTime);

            if(boosting)
            {
                boostTimer += Time.deltaTime;
                if(boostTimer >= 5)
                {
                    speed = 1000;
                    boostTimer = 0;
                    boosting = false;
                }
                if(slowdown == true)
                {
                    speed = 1000;
                    slowdown = false;
                    slowdownTimer = 0;
                }
            }

            if(slowdown)
            {
                slowdownTimer += Time.deltaTime;
                if(slowdownTimer >= 5)
                {
                    speed = 1000;
                    slowdownTimer = 0;
                    slowdown = false;
                }
                if (slowdown == false)
                {
                    speed = 1000;
                    boostTimer = 0;
                    boosting = false;
                }
                
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
        else if(collider.gameObject.CompareTag("Win"))
        {
            gameStarted = false;
            restartGameState();
            WinText.text = "Ты выйграл!\n Твой счет: " + count;
            WinText.gameObject.SetActive(true);
            playAgainButton.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(false);
            StarText.gameObject.SetActive(false);

        }
        else if(collider.gameObject.CompareTag("Bonuse"))
        { 
            boosting = true;
            speed = 2000;
            collider.gameObject.SetActive(false);
        }
        else if(collider.gameObject.CompareTag("Debuf"))
        {
            slowdown = true;
            speed = 300;
            collider.gameObject.SetActive(false);
        }
        
        if(collider.gameObject.CompareTag("Star"))
        {
            collider.gameObject.SetActive(false);
            countstars++;
            UpdateStarText();
        }
        else if(countstars == 3)
        {
            UiObject.SetActive(true);
            Finish.SetActive(true);
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Твой счет: " + count;
    }
    void UpdateStarText()
    {
        StarText.text = "Звезд: " + countstars;
    }

    void playAgainButtonAction()
    {
        count = 0;
        countstars = 0;
        UpdateScoreText();
        UpdateStarText();
        UiObject.SetActive(false);
        Finish.SetActive(false);
        scoreText.gameObject.SetActive(true);
        WinText.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        gameStarted = true;
    }

    void restartGameState()
    { 
        for(int i = 0; i < gems.Length; i++)
        {
            gems[i].gameObject.SetActive(true);
        }

        for(int i = 0;i < stars.Length; i++)
        {
            stars[i].gameObject.SetActive(true);
        }

        for(int i = 0; i < bonuses.Length; i++)
        {
            bonuses[i].gameObject.SetActive(true);
        }

        for(int i = 0; i < debufs.Length; i++)
        {
            debufs[i].gameObject.SetActive(true);
        }

        speed = 1000;
        Finish.SetActive(false);
        playerRb.position = startposition;
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
    }
}
