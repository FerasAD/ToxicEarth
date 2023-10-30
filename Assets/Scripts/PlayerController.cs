using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float JumpingSpeed = 8f;
    private float direction = 0f;
    private Rigidbody2D player;

    public Transform floorCheck;
    public float floorCheckArea;
    public LayerMask floorLayer;
    private bool isTouchingFloor;

    private Animator playerAnimation;

    private Vector3 respawnPoint;

    public Text scoreText;

    [SerializeField] private AudioSource jumpingsound;
    [SerializeField] private AudioSource collectingsound;
    [SerializeField] private AudioSource Endgamesound;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        scoreText.text = "SCORE: " + scoring.totalScore;
        respawnPoint = transform.position;
        
    }

    private void Update()
    {
        isTouchingFloor = Physics2D.OverlapCircle(floorCheck.position, floorCheckArea, floorLayer);
        direction = Input.GetAxis("Horizontal");
        Debug.Log(direction);

        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(0.2615227f, 0.2615227f);
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(-0.2615227f, 0.2615227f);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isTouchingFloor)
        {
            jumpingsound.Play();
            player.velocity = new Vector2(player.velocity.x, JumpingSpeed);
        }

        playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        playerAnimation.SetBool("OnFloor", isTouchingFloor);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            collectingsound.Play();
            scoring.totalScore += 1;
            scoreText.text = "SCORE: " + scoring.totalScore;
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (collision.tag == "PreviousLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else if (collision.tag == "EndGame")
        {
            Endgamesound.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        }
        else if(collision.tag == "Spike")
        {
            transform.position = respawnPoint;
        }
        else if(collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }
    }
}

    
