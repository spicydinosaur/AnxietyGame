using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTimer : MonoBehaviour
{
    public float globalCastDownTime;
    public float currentCastDownTime;
    //public float castDownTime;
    public Player player;



    // Start is called before the first frame update
    void Start()
    {
        currentCastDownTime = 0f;
        globalCastDownTime = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentCastDownTime > 0f)
        {
            currentCastDownTime -= Time.deltaTime;


        }
        else if (currentCastDownTime < 0f)
        {
            currentCastDownTime = 0f;

        }

        if (globalCastDownTime > 0f)
        {
            globalCastDownTime -= Time.deltaTime;
        }
        else if (globalCastDownTime < 0f)
        {
            globalCastDownTime = 0f;
        }

        if (currentCastDownTime > globalCastDownTime)
        {

            player.spellIconMask.fillAmount = Mathf.Clamp01(currentCastDownTime / player.currentSpellTemplate.castDownTime);

        }
        else if (globalCastDownTime > 0f)
        {

            player.spellIconMask.fillAmount = Mathf.Clamp01(globalCastDownTime / player.currentSpellTemplate.castDownTime);

        }
        else
        {
            if (player.spellIconMask.fillAmount != 0)
            {
                player.spellIconMask.fillAmount = 0f;
            }
        }
                
    }
}
