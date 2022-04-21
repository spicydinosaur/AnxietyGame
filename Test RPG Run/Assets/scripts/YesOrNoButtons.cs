using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class YesOrNoButtons : MonoBehaviour
{
    public ButtonController buttonController;
    public bool yesOrNo;

    private void Start()
    {
        buttonController = buttonController.GetComponent<ButtonController>();
    }

    public void ButtonWasClicked()
    {
        Debug.Log("ButtonWasClicked() fired from script YesOrNoButtons.");
        if (yesOrNo == true)
        {
            Debug.Log("ButtonWasClicked() yesOrNo value is " + yesOrNo);
            Debug.Log("Dog var InteractingWithDog is " + buttonController.dog.InteractingWithDog);
            if (buttonController.dog.InteractingWithDog == true)
            {
                Debug.Log("Dog function OnDogYesClick() should have fired.");
                buttonController.dog.OnDogYesClick();
            }
        }
        else if (yesOrNo == false)
        {
            Debug.Log("ButtonWasClicked() yesOrNo value is " + yesOrNo);
            Debug.Log("Dog var InteractingWithDog is " + buttonController.dog.InteractingWithDog);
            if (buttonController.dog.InteractingWithDog == true)
            {
                Debug.Log("Dog function OnDogNoClick() should have fired.");
                buttonController.dog.OnDogNoClick();
            }
        }

    }
}
