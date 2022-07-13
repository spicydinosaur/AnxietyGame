using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
  
    public Button yesButton;
    public Button noButton;

    //possible gameobjects that might be interacted with
    public DogPopUpMessage dog;


    void Start()
    {
        dog = dog.GetComponent<DogPopUpMessage>();

    }

    private void Update()
    {

        if (dog.InteractingWithDog == true)
        {
            yesButton.gameObject.SetActive(true);
            noButton.gameObject.SetActive(true);
        }

    }


}

    



