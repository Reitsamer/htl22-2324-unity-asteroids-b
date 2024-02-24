using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditListController 
{
    VisualTreeAsset ListEntryTemplate;

    ListView CreditsListView;

    public void InitializeCreditList(VisualElement root, VisualTreeAsset listElementTemplate, List<Credit> dataList)
    {
        AllCredits = dataList;


        ListEntryTemplate = listElementTemplate;

        CreditsListView = root.Q<ListView>("creditsList");

        FillCreditsList();

    }

    public List<Credit> AllCredits;



    void FillCreditsList()
    {
        // Set up a make item function for a list entry
        CreditsListView.makeItem = () =>
        {
            // Instantiate the UXML template for the entry
            var newListEntry = ListEntryTemplate.Instantiate();

            // Instantiate a controller for the data
            var newListEntryLogic = new CreditEntryController();
            
            // Assign the controller script to the visual element
            newListEntry.userData = newListEntryLogic;

            // Initialize the controller script
            newListEntryLogic.SetVisualElement(newListEntry);

            // Return the root of the instantiated visual tree
            return newListEntry;
        };

        // Set up bind function for a specific list entry
        CreditsListView.bindItem = (item, index) =>
        {
            (item.userData as CreditEntryController).SetCreditData(AllCredits[index]);
        };

        // Set a fixed item height
        CreditsListView.fixedItemHeight = 200;

        // Set the actual item's source list/array
        CreditsListView.itemsSource = AllCredits;
    }
}
