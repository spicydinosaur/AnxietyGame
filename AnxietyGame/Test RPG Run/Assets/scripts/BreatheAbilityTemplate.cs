using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class BreatheAbilityTemplate : SpellTemplate
{





    public override void castSpell()
    {
        spellSound.Play();
        gameObject.GetComponent<Animator>().SetBool("isCasting", true);
        player.currentCastDownTime = castDownTime;
        currentCastDownTime = castDownTime;
        player.PlayerHealth(player.maxHealthBar / (player.maxHealth * .1f));
        player.PlayerMana(player.maxManaBar / (player.maxMana * .01f));
        player.spellIconMask.fillAmount = Mathf.Clamp01(currentCastDownTime / castDownTime);

    }



}
