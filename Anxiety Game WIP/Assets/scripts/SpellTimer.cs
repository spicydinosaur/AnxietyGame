using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellTimer : MonoBehaviour
{
    public float globalCastDownTime;
    //public float currentCastDownTime;
    public float castDownTime;
    public Player player;
    public SpellTemplate currentSpellScript;
    public Image spellIconMask;

    //This script tracks casting downtimes for the Hero's spells. It tracks it based on the current spell (using the script spellTemplate and child scripts.)
    //whenever the mouse wheel is used and the spell changes, the current downtime, which has been counting down if it is not expired on the parent spell object
    //(these are children of the "spells" gameobject,) some spells have a global cooldown that affects all the other spells, as well, (except Breathe, it is unaffected
    //by global cooldowns.) If the cooldown for the spell is shorter than the global cooldown, which is always and only tracked by this script, though it is defined
    //in the aforementioned spell gameobject. The Player script checks this script for the current downtime, if applicable, and determines whether a spell can be cast
    //based on tracked downtime.

    // Start is called before the first frame update
    void Start()
    {

        globalCastDownTime = 0f;
        spellIconMask.fillAmount = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        /*if (currentCastDownTime > 0f)
        {
            currentCastDownTime -= Time.deltaTime;


        }
        else if (currentCastDownTime < 0f)
        {
            currentCastDownTime = 0f;

        }*/

        if (globalCastDownTime > 0f)
        {
            globalCastDownTime -= Time.deltaTime;
        }
        else if (globalCastDownTime < 0f)
        {
            globalCastDownTime = 0f;

        }

        if (currentSpellScript.currentCastDownTime > globalCastDownTime || player.selectedSpell == 0 && currentSpellScript.currentCastDownTime > 0f)
        {

            spellIconMask.fillAmount = Mathf.Clamp01(currentSpellScript.currentCastDownTime / castDownTime);
            //Debug.Log("this is a degbug for SpellTimer firing on Update, setting spellIconMask.fillAmount to " + spellIconMask.fillAmount);

        }
        else if (globalCastDownTime > 0f && player.selectedSpell != 0)
        {

            spellIconMask.fillAmount = Mathf.Clamp01(globalCastDownTime / castDownTime);

        }
        else
        {
            spellIconMask.fillAmount = 0f;

        }
                
    }
}
