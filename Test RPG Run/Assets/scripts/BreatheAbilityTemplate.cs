using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreatheAbilityTemplate : SpellTemplate
{



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame


    public override void attemptSpell()
    {
        base.attemptSpell();
    }

    public override void castSpell()
    {

        spellPrefabInstantiate.GetComponent<Animator>().SetBool("isCasting", true);
        spellPrefabInstantiate.GetComponent<Animator>().SetInteger("currentSpell", 0);
        player.PlayerMana(.1f);

    }
}
