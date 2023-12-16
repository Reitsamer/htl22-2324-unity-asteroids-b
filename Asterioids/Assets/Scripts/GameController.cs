using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool canMove;
    [SerializeField]
    public Countdown countdown;
    
    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        Restart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Restart();
        }
    }

    public void Restart()
    {
        canMove = false;
        countdown.StartCountdown();
    }

    public void Crash()
    {
        countdown.StopAll();
    }
}
