using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;



public class ScoreBoardManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;

    private List<int> scores;
    private List<Label> scoreLabels;

    private static ScoreBoardManager _scoreBoardManager;


    public static ScoreBoardManager Instance
    {
        get 
        { 
            return _scoreBoardManager;
        }
}


    // Start is called before the first frame update
    void Start()
    {
        _scoreBoardManager = this;

        scoreLabels = new List<Label>();
        scores = new List<int>();

        var uiDoc = GetComponent<UIDocument>();

        var root = uiDoc.rootVisualElement;

        root.style.display = DisplayStyle.None;


        var backButtonCredit = root.Q<Button>("back");

        backButtonCredit.clicked += () =>
        {
            root.style.display = DisplayStyle.None;

            mainMenu.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;

        };

        
        for (int i = 1; i <= 5; i++)
        {
            scoreLabels.Add(root.Q<Label>($"score{i}")); ;
        }

        LoadScores();

        AddScore(0);
        AddScore(0);
        AddScore(0);
        AddScore(0);
        AddScore(0);
         
        UpdateScore();

    }

    public UnityEngine.TextAsset scoreCsv;

    private void LoadScores()
    {
        var parts = scoreCsv.text.Split(',');

        foreach (var part in parts)
        {
            scores.Add(int.Parse(part));
        }
    }

    private void SaveScore()
    {
        File.WriteAllText(AssetDatabase.GetAssetPath(scoreCsv), string.Join(',', scoreCsv));
    }

    private void UpdateScore()
    {
        foreach (var item in scoreLabels)
        {
            item.text = "";
        }

        for (int i = 0; i < 5; i++)
        {
            //nicht gut, gar nicht gut
            try
            {
                scoreLabels[i].text = scores[i].ToString();

            }
            catch (Exception e) { }
        }
    }
    
    public void AddScore(int score)
    {
        scores.Add(score);

        scores.Sort((a, b) => b.CompareTo(a));


        UpdateScore();

        SaveScore();
    }
}
