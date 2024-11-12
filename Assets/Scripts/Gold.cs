using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField] Player player;
    Sounds Sounds;
    // Start is called before the first frame update
    void Start()
    {
        Sounds = GameObject.FindGameObjectWithTag("Audio").GetComponent<Sounds>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && this.tag == "Gold")
        {
            Destroy(this.gameObject);
            player.IncreaseScore();
            Sounds.PlaySFX(Sounds.gold);
        }
    }
}
