using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class BreatheAbilityTemplate : SpellTemplate
{


    public AudioSource spellSound;


    public override void castSpell()
    {
        spellSound.Play(0);
        gameObject.GetComponent<Animator>().SetBool("isCasting", true);
        spellTimer.currentCastDownTime = castDownTime;
        currentCastDownTime = castDownTime;
        player.PlayerHealth(player.maxHealthBar / (player.maxHealth * .1f));
        player.PlayerMana(player.maxManaBar / (player.maxMana * .01f));
        //player.spellIconMask.fillAmount = Mathf.Clamp01(currentCastDownTime / castDownTime);

    }



}
