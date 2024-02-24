using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
public class Countdown : MonoBehaviour
{
    public static EventHandler OnAllowMovement;

    bool countdownStarted = false;
    int countdownTimer = 3;
    [SerializeField] TextMeshProUGUI counterNum;

    private void Start()
    {
        
        counterNum = GetComponentInChildren<TextMeshProUGUI>();
        //StartCountdown();
    }

    public void StartCountdown()
    {
        if (countdownStarted)
            return;
        countdownStarted = true;

        countdownTimer = 3;
        counterNum.gameObject.SetActive(true);
        StartCoroutine(Counter());
    }

    public void StopAll()
    {
        GameObject[] objectsToStop = FindObjectsOfType<GameObject>();
        foreach (var obj in objectsToStop)
            if (obj.TryGetComponent(out IStartStop o))
                o.StopGame();
    }

    public void StartAll()
    {
        GameObject[] objectsToStop = FindObjectsOfType<GameObject>();
        foreach (var obj in objectsToStop)
            if (obj.TryGetComponent(out IStartStop o))
                o.StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    IEnumerator Counter()
    {
        while (countdownTimer >= 1)
        {
            counterNum.text = $"{countdownTimer}";
            countdownTimer--;
            yield return new WaitForSeconds(0.5f);
        }
        StartAll();

        counterNum.gameObject.SetActive(false);
        countdownStarted = false;
    }
}