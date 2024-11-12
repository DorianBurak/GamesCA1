using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int direction = 0;
    public int speed;
    private float JumpHeight = 2;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private int jumpCounter;
    private bool isJumping;
    [SerializeField] Image[] images;
    private int lifes = 3;
    private bool isDead = false;
    private int score = 0;
    [SerializeField] TMP_Text scoreText;
    private const String BORDER = "Border";
    private const String FINAL = "Final";
    public GameOver GameManager;
    Sounds Sounds;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator.SetFloat("Move X", 0);
        _animator.SetFloat("Move Y", 0);
        Sounds = GameObject.FindGameObjectWithTag("Audio").GetComponent<Sounds>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        float moveby = Input.GetAxis("Horizontal");
        Vector2 position = transform.position;
        float move = Input.GetAxis("Horizontal");
        position.x = position.x + speed * Time.deltaTime * move;
        transform.position = position;

        if (!isJumping && jumpCounter < 2 && Input.GetKeyDown(KeyCode.Space))
        {
            Sounds.PlaySFX(Sounds.jump);
            isJumping = true;
            Debug.Log("Jump");
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * JumpHeight)), ForceMode2D.Impulse);
        }

        playAnimation(moveby);
    }

    private void playAnimation(float moveby)
    {
        _animator.SetBool("isJumping", isJumping);
        if (moveby == 0)
        {
            _animator.SetFloat("Move X", 0);
        }
        else if (moveby < 0)
        {
            _animator.SetFloat("Move X", -1);
            _animator.SetFloat("Move Y", -1);
        }
        else
        {
            _animator.SetFloat("Move X", 1);
            _animator.SetFloat("Move Y", 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (isJumping)
        {
            isJumping = false;
            jumpCounter = 0;
            Debug.Log("Test");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(BORDER))
        {
            Die();
        }
        if (collision.CompareTag(FINAL))
        {
            Finish();
        }
    }

    private void updateLivesUI()
    {
        switch(lifes){
            case 0:
                images[0].enabled = false;
                images[1].enabled = false;
                images[2].enabled = false;
                break;
            case 1:
                images[0].enabled = true;
                images[1].enabled = false;
                images[2].enabled = false;
                break;
            case 2:
                images[0].enabled = true;
                images[1].enabled = true;
                images[2].enabled = false;
                break;
            case 3:
                images[0].enabled = true;
                images[1].enabled = true;
                images[2].enabled = true;
                break;
        }
    }

    public void RemoveLife()
    {
        lifes--;
        updateLivesUI();
        Sounds.PlaySFX(Sounds.hit);
        if(lifes == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        _animator.SetTrigger("Die");
        GameManager.gameOver();
        gameObject.SetActive(false);
        Debug.Log("Player has died");
        Sounds.PlaySFX(Sounds.death);
    }

    private void Finish()
    {
        GameManager.gameFinished();
        Debug.Log("Player has finsihed");
        Sounds.PlaySFX(Sounds.finish);
    }

    public void IncreaseScore()
    {
        score ++;
        if (scoreText != null)
        {
            scoreText.text = ":" + score;
        }
    }

    public void RemoveScore()
    {
        score--;
        if (scoreText != null)
        {
            scoreText.text = ":" + score;
        }
    }

    public void IncreaseHealth()
    {
        if (lifes < 3)
        {
            lifes++;
            updateLivesUI ();
        }
    }

}
