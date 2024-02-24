using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class CreditEntryController
{
    Label Credit;
    Label Names;

   public void SetVisualElement(VisualElement visualElement)
    {
        Credit = visualElement.Q<Label>("credit");
        Names = visualElement.Q<Label>("names");
    }

    public void SetCreditData(Credit credit)
    {
        Credit.text = credit.task;

        Names.text = string.Join(" ", credit.names);

        
    }
}
