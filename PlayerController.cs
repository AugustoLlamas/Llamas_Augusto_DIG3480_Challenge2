using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    private Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;
    public Text countText;
    public Text winText;
    public Text livesText;
    public Text loseText;
    public Animator myAnimator;
    public AudioClip musicClipOne;
    public AudioSource musicSource;

    private int count;
    private int lives;
    private bool facingRight;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        facingRight = true;
        rb2d = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        count = 0;
        lives = 3;
        winText.text = "";
        loseText.text = "";

        SetAllText();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey("escape"))
            Application.Quit();
    }
    private void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);

        myAnimator.SetFloat("State", Mathf.Abs(moveHorizontal));

        Flip(moveHorizontal);

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        {
            myAnimator.SetBool("IsJumping", false);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

                myAnimator.SetBool("IsJumping", true);
            }
         

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetAllText();

        }
        if (other.gameObject.CompareTag("Foe"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetAllText();
        }
        if (count == 4)
        {
            transform.position = new Vector3(24.68f, 2.65f, gameObject.transform.position.z);
        }
    }
    void SetAllText()
    {
        countText.text = "Count: " + count.ToString();
        livesText.text = "Lives: " + lives.ToString();
        if (count >= 8)
        {
            winText.text = "YOU WIN!";
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }
        else if (lives == 0)
        {
            Destroy(gameObject);
            loseText.text = "GAME OVER";
        }
        
    }
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
  
}
