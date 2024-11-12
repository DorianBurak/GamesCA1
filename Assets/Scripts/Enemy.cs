using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public bool flip;
    public float detection;
    public float fieldOfAngle = 85f;
    private bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer < detection)
        {
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.right * (flip ? -1 : 1), directionToPlayer);
            if (angleToPlayer <= fieldOfAngle || inRange)
            {
                inRange = true;
                Vector3 scale = transform.localScale;

                if (player.transform.position.x > transform.position.x)
                {
                    scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }
                else
                {
                    scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
                    transform.Translate(speed * Time.deltaTime * -1, 0, 0);
                }

                transform.localScale = scale;
            }
        }
        else
        {
            inRange = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.RemoveScore();
                Destroy(this.gameObject);
                playerScript.RemoveLife();
            }
        }
    }
}
