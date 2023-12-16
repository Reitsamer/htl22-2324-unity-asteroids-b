using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.SceneManagement.Scene active;

    private void Start()
    {
        active = SceneManager.GetActiveScene();
    }

    private void Load(Scene scene) => SceneManager.LoadScene(scene.name);

    private void Unload(Scene scene) => SceneManager.UnloadSceneAsync(scene);

    private void OnLevelWasLoaded(int level)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
    }
}
