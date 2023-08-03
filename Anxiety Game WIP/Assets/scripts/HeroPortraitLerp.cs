using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroPortraitLerp : MonoBehaviour
{

    //this cycles through at least two images made by Loka. Only the first two in the list matter at any given time.
    //The first one is overlaid over the second one and the opacity (the w value in a Vector4, or the a value of rgba.
    //I am using the latter. The opacity is measured from 1f for full opacity to 0f for a transparent image.)
    //The primary image slowly fades overtop of the secondary image, allowing images to merge into each other.
    //From there the images move down the list. Can accomodate more than four images, but needs at least two.
    //It can cycle back the other way, as well. The images just have to be listed in reverse (except the very first one,
    //since it will already flip over to that when it starts back at the beginning of the list.

    //The goal is eventually to make the cycling happen at specific times. I just haven't worked out when I want to do this yet.
    //I'll probably add a boolean value to determine when the images cycle or not, and make an int to cap the numbers
    //in the list I use at any given time.

    public List<GameObject> imagesToLerp;
    [SerializeField]

    public float currentPrimaryImageOpacity;
    public float primaryImageOpacityModifier;

    public int currentPrimaryImageInList;
    public int currentSecondaryImageInList;

    public Image currentPrimaryImage;

    public RectTransform currentPrimaryImageRectTransform;
    [SerializeField]
    public RectTransform currentSecondaryImageRectTransform;
    [SerializeField]


    void Start()
    {
        currentPrimaryImageOpacity = 1.0f;

        if (imagesToLerp.Count > 1)
        {
            currentPrimaryImageInList = 0;
            currentSecondaryImageInList = 1;

            currentPrimaryImage = imagesToLerp[currentPrimaryImageInList].GetComponent<Image>();

            currentPrimaryImage.color = new Color(currentPrimaryImage.color.r, currentPrimaryImage.color.g, currentPrimaryImage.color.b, currentPrimaryImageOpacity);
            currentPrimaryImageRectTransform = currentPrimaryImage.GetComponent<RectTransform>();
            currentSecondaryImageRectTransform = imagesToLerp[currentSecondaryImageInList].GetComponent<RectTransform>();
            
            //the below commands first set the secondary image to the forefront, then puts the primary image in front of that.
            //The order of the other images while they are not primary or secondary is irrelevant as it sets each newly designated
            //secondary then primary image in update.

            currentSecondaryImageRectTransform.SetAsLastSibling();
            currentPrimaryImageRectTransform.SetAsLastSibling();
        }
        if (primaryImageOpacityModifier <= 0f)
        {
            primaryImageOpacityModifier = 1f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (imagesToLerp.Count > 0)
        {

            currentPrimaryImageOpacity -= Time.deltaTime / primaryImageOpacityModifier;

            if (currentPrimaryImageOpacity <= 0.0f)
            {
                currentPrimaryImageOpacity = 1f;
                currentPrimaryImage.color = new Color(currentPrimaryImage.color.r, currentPrimaryImage.color.g, currentPrimaryImage.color.b, currentPrimaryImageOpacity);

                if (currentSecondaryImageInList == imagesToLerp.Count - 1)
                {

                    currentSecondaryImageInList = 0;
                    currentPrimaryImageInList++;
                    //Debug.Log("currentSecondaryImageInList = " + currentSecondaryImageInList + ", currentPrimaryImageInList = " + currentPrimaryImageInList + ", imagesToLerp.Count = " + imagesToLerp.Count + ". first if statement within if (currentPrimaryImageOpacity <= 0.0f) in Update fired: if (currentSecondaryImageInList >= imagesToLerp.Count - 1)");
                }
                else if (currentPrimaryImageInList == imagesToLerp.Count - 1)
                {
                    currentPrimaryImageInList = 0;
                    currentSecondaryImageInList++;
                    //Debug.Log("currentSecondaryImageInList = " + currentSecondaryImageInList + ", currentPrimaryImageInList = " + currentPrimaryImageInList + ", imagesToLerp.Count = " + imagesToLerp.Count + ". second if statement within if (currentPrimaryImageOpacity <= 0.0f) in Update fired: else if (currentPrimaryImageInList >= imagesToLerp.Count - 1)");
                }
                else
                {
                    currentPrimaryImageInList++;
                    currentSecondaryImageInList++;
                    //Debug.Log("currentSecondaryImageInList = " + currentSecondaryImageInList + ", currentPrimaryImageInList = " + currentPrimaryImageInList + ", imagesToLerp.Count = " + imagesToLerp.Count + ". final else statement within if (currentPrimaryImageOpacity <= 0.0f) in Update fired.");
                }
                currentPrimaryImage = imagesToLerp[currentPrimaryImageInList].GetComponent<Image>();
                currentPrimaryImageRectTransform = currentPrimaryImage.GetComponent<RectTransform>();
                currentSecondaryImageRectTransform = imagesToLerp[currentSecondaryImageInList].GetComponent<RectTransform>();
                currentSecondaryImageRectTransform.SetAsLastSibling();
                currentPrimaryImageRectTransform.SetAsLastSibling();


            }
            else
            {
                currentPrimaryImage.color = new Color(currentPrimaryImage.color.r, currentPrimaryImage.color.g, currentPrimaryImage.color.b, currentPrimaryImageOpacity);
            }

        }
    }
}
