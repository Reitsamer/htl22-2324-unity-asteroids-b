using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject highscoreMenu;
    [SerializeField] GameObject creditsScreen;

    public UnityEvent start;

    // Start is called before the first frame update
    void Start()
    {
        var rootMain = mainMenu.GetComponent<UIDocument>().rootVisualElement;

        var playButton = rootMain.Q<Button>("play");
        var highscoreButton = rootMain.Q<Button>("highscore");
        var creditsButton = rootMain.Q<Button>("credits");

        playButton.clicked += () =>
        {
            start.Invoke();
        };

        highscoreButton.clicked += () =>
        {
            highscoreMenu.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;

            mainMenu.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        };

        creditsButton.clicked += () =>
        {
            creditsScreen.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;

            mainMenu.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        };


    }

}
