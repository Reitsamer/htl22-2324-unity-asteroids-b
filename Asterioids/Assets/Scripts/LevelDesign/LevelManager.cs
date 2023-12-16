using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.SceneManagement.Scene active;
    [SerializeField] private List<UnityEngine.SceneManagement.Scene> scene;
    
   

    private int index = 0;

    private void Start()
    {
        scene = SceneManager.GetAllScenes().ToList();
        active = scene[index];
        SceneManager.SetActiveScene(active);
    }

    

    private void Load()
    {
        active = scene[index++];
        SceneManager.SetActiveScene(active);
    }

    private void Unload ()
    {
        SceneManager.UnloadSceneAsync(active);
    }

    private void OnLevelWasLoaded(int level)
    {
        // IRGENDEINE MESSAGE FÜR DEN START DES NEUEN LVLSs
    }
}

