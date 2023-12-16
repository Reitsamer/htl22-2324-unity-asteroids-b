using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Scene active;                             // represents the active Scene 
    [SerializeField] private Scene load;                               // loadingscreen can be added   

    
    public event EventHandler<Scene> EndLevel;                // add the logic that should be done if a level is finished
    private LinkedList<Scene> sceneList;                           // holds all  levelscenes 

    private void Start()                                                    // all Levels are added to the LList 
    {
        for (int i = 0; i < 10; i++)
        {
            sceneList.AddLast(SceneManager.GetSceneByName($"Level{i}"));
        }

        active = SceneManager.GetActiveScene();
    }

    private void NextScene()                                           // sets active to the next scene
    {
        sceneList.RemoveFirst();
        active = sceneList.First();
    }
    
    private void Update()                                                 // Checks if Level is Complete
    {
        if (GameObject.FindGameObjectWithTag("Rock") == null)
            EndLevel?.Invoke(this, active);
    }

    private void OnLevelEnd(Scene sender, EventArgs e) // Is Called by EndLevel
    {
        NextScene();
        SceneManager.LoadSceneAsync(active.buildIndex);
        SceneManager.LoadScene(load.buildIndex);
    }
}
