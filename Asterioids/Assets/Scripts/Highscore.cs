using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour
{
    [SerializeField] float currentMoney;

    public static Highscore Instance;

    

    [SerializeField] float amountToAdd;

    float startCoins = 0;


    float lerpDuration = 0.75f;

    TextMeshProUGUI scoreUI;
    private void Start()
    {
        Instance = this;
        scoreUI = GetComponent<TextMeshProUGUI>();
    }
    public void IncreaseScore(float size)
    {
        StartCoroutine(ScoreCoroutine(startCoins, lerpDuration, size));

        //GetComponent<ParticleSystem>().Play();
        //GetComponent<AudioSource>().Play();
        //GetComponent<Collider>().enabled = false;
        //gameObject.GetComponent<Renderer>().enabled = false;
    }

    IEnumerator ScoreCoroutine(float startCoins, float lerpDuration, float size)
    {
        float addScore = 0;
        float moneyBefore = startCoins;
        float timeElapsed = 0;
        while (startCoins <= amountToAdd)
        {
            moneyBefore = startCoins;
            startCoins = Mathf.Lerp(startCoins, amountToAdd, timeElapsed / lerpDuration);

            addScore = Math.Abs(startCoins - moneyBefore);

            IncScore(addScore / size);

            timeElapsed += Time.deltaTime;
            yield return null;

        }
    }
    public void IncScore(float score)
    {
        currentMoney += score;
        scoreUI.text = $"Score: {Math.Round(currentMoney, 0)}";
    }
    public void ResetScore()
    {
        currentMoney = 0;
        scoreUI.text = $"Score: {Math.Round(currentMoney, 0)}";
    }
}