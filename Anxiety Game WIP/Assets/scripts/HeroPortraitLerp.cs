using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HeroPortraitLerp : MonoBehaviour
{

    //this cycles through four total images made by Loka. Only the first two in the list matter at any given time.
    //The first one is overlaid over the second one and the opacity (the w value in a Vector4, or the a value of rgba.
    //I am using the latter. The opacity is measured from 1f for full opacity to 0f for a transparent image.)
    //The primary image slowly fades overtop of the secondary image, allowing images to merge into each other.
    //From there the images move down the list. Can accomodate more than four images, but needs at least two.

    //Recttransform values are not setting, and the fade isn't happening though the lerp itself is working.

    public List<GameObject> ImagesToLerp;
    [SerializeField]

    public float currentPrimaryImageOpacity;
    public float primaryImageOpacityModifier;

    public int currentPrimaryImageInList;
    public int currentSecondaryImageInList;

    public Image currentPrimaryImage;

    public RectTransform currentPrimaryImageRectTransform;
    public RectTransform currentSecondaryImageRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        currentPrimaryImageOpacity = 1.0f;

        if (ImagesToLerp.Count > 0)
        {
            currentPrimaryImageInList = 0;
            currentSecondaryImageInList = 1;

            ImagesToLerp[currentSecondaryImageInList].SetActive(true);
            ImagesToLerp[currentPrimaryImageInList].SetActive(true);

            currentPrimaryImage = ImagesToLerp[currentPrimaryImageInList].GetComponent<Image>();

            currentPrimaryImage.tintColor = new Color(currentPrimaryImage.tintColor.r, currentPrimaryImage.tintColor.g, currentPrimaryImage.tintColor.b, currentPrimaryImageOpacity);
            currentPrimaryImageRectTransform = ImagesToLerp[currentPrimaryImageInList].GetComponent<RectTransform>();
            currentSecondaryImageRectTransform = ImagesToLerp[currentSecondaryImageInList].GetComponent<RectTransform>();
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
        if (ImagesToLerp.Count > 0)
        {

            currentPrimaryImageOpacity -= Time.deltaTime / primaryImageOpacityModifier;

            if (currentPrimaryImageOpacity <= 0.0f)
            {
                currentPrimaryImageOpacity = 1f;
                currentPrimaryImage.tintColor = new Color(currentPrimaryImage.tintColor.r, currentPrimaryImage.tintColor.g, currentPrimaryImage.tintColor.b, currentPrimaryImageOpacity);
                ImagesToLerp[currentPrimaryImageInList].SetActive(false);

                currentPrimaryImageInList++;
                currentSecondaryImageInList = currentPrimaryImageInList + 1;

                if (currentSecondaryImageInList > ImagesToLerp.Count)
                {
                    currentSecondaryImageInList = 0;
                }
                else if (currentPrimaryImageInList > ImagesToLerp.Count)
                {
                    currentPrimaryImageInList = 0;
                }

                ImagesToLerp[currentSecondaryImageInList].SetActive(true);

                currentPrimaryImageRectTransform = ImagesToLerp[currentPrimaryImageInList].GetComponent<RectTransform>();
                currentSecondaryImageRectTransform = ImagesToLerp[currentSecondaryImageInList].GetComponent<RectTransform>();
                currentSecondaryImageRectTransform.SetAsLastSibling();
                currentPrimaryImageRectTransform.SetAsLastSibling();

            }
            else
            {
                currentPrimaryImage.tintColor = new Color(currentPrimaryImage.tintColor.r, currentPrimaryImage.tintColor.g, currentPrimaryImage.tintColor.b, currentPrimaryImageOpacity);
            }

        }
    }
}
