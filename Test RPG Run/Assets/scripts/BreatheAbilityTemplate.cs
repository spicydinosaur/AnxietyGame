using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class BreatheAbilityTemplate : SpellTemplate
{





    public override void castSpell()
    {

        gameObject.GetComponent<Animator>().SetBool("isCasting", true);
        spellHolderScript.currentCastDownTime = castDownTime;
        currentCastDownTime = castDownTime;
        player.PlayerHealth(player.maxHealthBar / (player.maxHealth * .1f));
        player.PlayerMana(player.maxManaBar / (player.maxMana * .01f));
        spellHolderScript.spellIconMask.fillAmount = Mathf.Clamp01(currentCastDownTime / castDownTime);

    }



    /*public override void castSpellGamepad()
    {
        GetComponent<Animator>().SetBool("isCasting", true);
        player.PlayerMana(player.maxMana * .1f);
        player.PlayerHealth(player.maxHealth * .1f);
        spellHolder.currentCastDownTime = castDownTime;
        currentCastDownTime = castDownTime;
    }*/
}
