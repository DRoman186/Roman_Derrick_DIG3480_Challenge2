using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    public Text winText;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    private bool grounded;

    private int scoreValue = 0;
    private int dir = 1;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
    }
    private void Update()
    {
        if (rd2d.velocity.magnitude > 0.5f)
        {
            anim.SetInteger("State", 1);
        }

        else
        {
            anim.SetInteger("State", 0);
        }

        if (!grounded)
            anim.SetInteger("State", 2);

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (rd2d.velocity.x < -0.5f)
            dir = -1;
        if (rd2d.velocity.x > 0.5f)
            dir = 1;
        transform.localScale = new Vector3(dir * 0.3f, 0.3f, 0.3f);
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (scoreValue == 4 && collision.collider.tag =="Coin")
        { 
            winText.text = "Congratulations for winning! Game made by Derrick Roman.";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            grounded = true;
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                grounded = false;
            }
        }
        if (collision.collider.tag == "Walls")
        {
            grounded = false;
        }
    }
}