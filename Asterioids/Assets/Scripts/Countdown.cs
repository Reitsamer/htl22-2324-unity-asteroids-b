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

    int countdownTimer = 3;
    [SerializeField] GameObject[] objectsToStop;
    [SerializeField] TextMeshProUGUI counterNum;

    private void Start()
    {
        objectsToStop = FindObjectsOfType<GameObject>();
        counterNum = GetComponentInChildren<TextMeshProUGUI>();
        StartCountdown();
    }

    void StartCountdown()
    {
        StartCoroutine(Counter());
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
            yield return new WaitForSeconds(1);
        }
        foreach (var obj in objectsToStop)
        {
            if (obj.TryGetComponent(out IShouldBeStopped o))
            {
                o.StartGame();
            }
        }
        Destroy(gameObject);
    }
}