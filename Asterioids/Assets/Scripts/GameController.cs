using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, IStartStop
{
    public bool canMove;
    [SerializeField]
    public Countdown countdown;
    [SerializeField]
    public RockSpawner spawner;
    [SerializeField]
    public SpaceshipController spaceship;

    [SerializeField]
    public int CurrentRockCount;
    [SerializeField]
    public int TotalRockCount;
    
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

    private void CommonStart()
    {
        spaceship.ResetPosition();
        CurrentRockCount = 0;
        canMove = false;
        spawner.ClearRocks();
        countdown.StartCountdown();
    }

    public void Restart()
    {
        TotalRockCount = 2;
        Highscore.Instance.ResetScore();
        CommonStart();
    }

    public void NextLevel()
    {
        TotalRockCount++;
        canMove = false;
        Invoke("NextLevelAfter", 1.5f);
    }

    public void NextLevelAfter()
    {
        CommonStart();
    }

    public void Crash()
    {
        countdown.StopAll();
        Invoke("CrashAfter", 1.5f);
    }

    public void CrashAfter()
    {
        spawner.ClearRocks();
    }

    public void StartGame()
    {
        canMove = true;
    }

    public void StopGame()
    {
        canMove = false;
    }
}
