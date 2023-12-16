using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject highscoreMenu;


    // Start is called before the first frame update
    void Start()
    {
        var rootScore = highscoreMenu.GetComponent<UIDocument>().rootVisualElement;

        var backButton = rootScore.Q<Button>("back");

        backButton.clicked += () =>
        {
            mainMenu.SetActive(true); ;

            highscoreMenu.SetActive(false);
        };

        highscoreMenu.SetActive(false);

        var rootMain = mainMenu.GetComponent<UIDocument>().rootVisualElement;

        var playButton = rootMain.Q<Button>("play");
        var highscoreButton = rootMain.Q<Button>("highscore");
        var creditsButton = rootMain.Q<Button>("credits");

        playButton.clicked += () =>
        {
            //todo game scene loader
        };

        highscoreButton.clicked += () => 
        {
            highscoreMenu.SetActive(true);

            mainMenu.SetActive(false);
        };

        creditsButton.clicked += () => 
        {
            //todo
        };


    }

}
