using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class BreatheAbilityTemplate : SpellTemplate
{

    //This ability/spell (Breathe) costs no mana and restores some mana and mental health. It has a long down time to prevent
    //making things too easy, and does not return much, it's currently timed at around 5 mins downtime. It creates no global
    //downtime and isn't affected by a global downtime. If the spell is selected then the mouse clicked anywhere on the screen
    //it gets cast. It doesn't have anything related to range, as things like that are irrelevant. Currently it only works on the Hero.



    public override void castSpell()
    {
        spellSound.Play(0);
        gameObject.GetComponent<Animator>().SetBool("isCasting", true);
        currentCastDownTime = castDownTime;
        spellTimer.globalCastDownTime = globalCastDownTime;
        player.PlayerHealth(player.maxHealthBar / (player.maxHealth * .1f));
        player.PlayerMana(player.maxManaBar / (player.maxMana * .1f));
        //player.spellIconMask.fillAmount = Mathf.Clamp01(currentCastDownTime / castDownTime);

    }



}
