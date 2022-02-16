using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpellTemplate : MonoBehaviour
{
    //The below shit is in SpellHolder
    public GameObject spellBook;
    public Animator animSpellBook;
    protected Color spellbookColor;

    public float currentCastDownTime;

    //what will this shit hit? Do we need this in template?
    public ContactFilter2D contactFilter;
    public float rayCastDistance;

    //Spells, can we work off of prefabs alone?
    public GameObject spellPrefab;
    protected GameObject spellPrefabInstantiate;
    protected Animator animPrefab;
    protected Animator animPrefabInstantiate;
    public float damageAmount;
    public float manaCost;
    public float castDownTime;
    public GameObject Hero;
    public Player player;
    public SpellHolder spellHolder;






    void Start()
    {

        spellbookColor = spellBook.GetComponent<SpriteRenderer>().color;
        spellBook.GetComponent<SpriteRenderer>().color = Color.clear;
        animSpellBook = spellBook.GetComponent<Animator>();
        //spellPrefabInstantiate = Instantiate(spellPrefab, new Vector3(0, 0, -50), Quaternion.identity);
        spellPrefabInstantiate = Instantiate(spellPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        animPrefabInstantiate = spellPrefabInstantiate.GetComponent<Animator>();

        currentCastDownTime = 0f;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void attemptSpell()
    {
        //this code is what all spells will do. (Unless overriden)

        if (player.currentMana < manaCost)
        {
            if (Input.GetMouseButtonDown(0))
            {
                spellHolder.failSpell.PlayOneShot(spellHolder.failSpellClip, 1f);
                return;
            }

        }

        else if (player.currentMana >= manaCost)
        {
            if (Input.GetMouseButtonDown(0))
            {
                player.PlayerMana(manaCost);
                castSpell();

            }
        }


    }

    public virtual void castSpell()
    {

    }

}
