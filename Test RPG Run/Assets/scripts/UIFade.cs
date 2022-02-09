using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    public Image fadeImage;
    //[SerializeField] float lerpTime;
    //[SerializeField] Color fadeOutColor;
    //[SerializeField] Color fadeInColor;
    public float lerpTime = 0f;
    public Color fadeOutColor;
    public Color fadeInColor;
    public bool fadingOut;
    public bool fadingIn;
    public float fadeTime;
    public SceneTransition callingTransition;




    public void Update()
    {
        if (fadingOut == true && fadingIn == false)
        {
            Debug.Log("lerp set. fading out " + fadingOut + " fading in " + fadingIn);
            fadeImage.color = Color.Lerp(fadeImage.color, fadeOutColor, lerpTime);

        }
        else if (fadingOut == false && fadingIn == true)
        {
            Debug.Log("lerp set. fading out " + fadingOut + " fading in " + fadingIn);
            fadeImage.color = Color.Lerp(fadeInColor, fadeImage.color, lerpTime);
        }

        if (lerpTime < 1)
        {
            if (fadingOut == true || fadingIn == true)
            {
                //Debug.Log("lerptime going up: " + lerpTime);
                lerpTime += Time.deltaTime / fadeTime;
            }

        }
        else if (lerpTime >= 1)
        {
            lerpTime = 1;
            Debug.Log("fading out: " + fadingOut + " fading in: " + fadingIn);
            if (fadingOut == true && fadingIn == false)
            {
                Debug.Log("fading out:" + fadingOut);
                callingTransition.fadeOutFinished();


            }
            else if (fadingIn == true && fadingOut == false)
            {
                Debug.Log("fading in:" + fadingIn);
                fadingIn = false;
                callingTransition.fadeInFinished();
                //enabled = false;

            }


        }

    }
}
