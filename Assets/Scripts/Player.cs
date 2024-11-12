using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int direction = 0;
    public int speed;
    private float JumpHeight = 2;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private int jumpCounter;
    private bool isJumping;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator.SetFloat("Move X", 0);
        _animator.SetFloat("Move Y", 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        float move = Input.GetAxis("Horizontal");
        position.x = position.x + speed * Time.deltaTime * move;
        transform.position = position;

        if (!isJumping && jumpCounter < 2 && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            Debug.Log("Jump");
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * JumpHeight)), ForceMode2D.Impulse);
        }
    }

    private void playAnimation(float moveby)
    {
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
}
