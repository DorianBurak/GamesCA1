using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverUI;
    public GameObject GameFinish;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver()
    {
        GameOverUI.SetActive(true);
    }

    public void gameFinished()
    {
        GameFinish.SetActive(true);
    }
}
