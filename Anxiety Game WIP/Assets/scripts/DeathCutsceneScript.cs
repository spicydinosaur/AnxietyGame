using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCutsceneScript : MonoBehaviour
{
    public List<GameObject> eyesThatBlink;
    public GameObject hero;
    public int eyeBlinkCountdown;
    public int randomValue;
    public int valueToBlinkAt;
    // Start is called before the first frame update
    void Start()
    {
        eyeBlinkCountdown = 0;
        if (valueToBlinkAt == 0)
        {
            valueToBlinkAt = 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        eyeBlinkCountdown++;
        if (eyeBlinkCountdown == valueToBlinkAt)
        {
            eyeBlinkCountdown = 0;
            foreach (GameObject eyes in eyesThatBlink)
            {
                randomValue =  Random.Range(1,21);
                if (eyes.GetComponent<SpriteRenderer>().color.a == 0f)
                {
                    eyes.GetComponent<SpriteRenderer>().color = new Color(eyes.GetComponent<SpriteRenderer>().color.r, eyes.GetComponent<SpriteRenderer>().color.g, eyes.GetComponent<SpriteRenderer>().color.b, 1f);
                }

                else if (randomValue == 1)
                {
                    eyes.GetComponent<SpriteRenderer>().color = new Color(eyes.GetComponent<SpriteRenderer>().color.r, eyes.GetComponent<SpriteRenderer>().color.g, eyes.GetComponent<SpriteRenderer>().color.b, 0f);
                }

            }

        }
    }
}
