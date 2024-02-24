using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Creditsmanager : MonoBehaviour
{
    [SerializeField] private TextAsset creditsFile;
    [SerializeField] GameObject mainMenu;

    [SerializeField]
    VisualTreeAsset ListEntryTemplate;

    ListView creditlist;

    // Start is called before the first frame update
    void Start()
    {
        var uiDoc = GetComponent<UIDocument>();

        var root = uiDoc.rootVisualElement;

        creditlist = root.Q<ListView>("creditsList");

        Credits credits = JsonUtility.FromJson<Credits>(creditsFile.text);



        var creditListController = new CreditListController();

        creditListController.InitializeCreditList(root, ListEntryTemplate, credits.credits.ToList());


        creditlist.Q<ScrollView>().verticalScrollerVisibility = ScrollerVisibility.Hidden;



        root.style.display = DisplayStyle.None;

        var backButtonCredit = root.Q<Button>("back");

        backButtonCredit.clicked += () =>
        {
            root.style.display = DisplayStyle.None;

            

            mainMenu.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;

        };
    }

    private void Update()
    {
        creditlist.Q<ScrollView>().scrollOffset = creditlist.Q<ScrollView>().scrollOffset + new Vector2(0, 0.04f);
    }

}
