using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpellTemplate : MonoBehaviour
{

    public enum spellCastTypes {raycast, projectile, other};

    public GameObject Hero;
    public GameObject spellBook;
    public Animator animSpellBook;
    public AudioSource failSpell;
    public AudioClip failSpellClip;

    public float rayCastDistance;
    public float castDownTime;
    public float currentCastDownTime;
    protected Color spellbookColor;

    //Spells
    public GameObject spellPrefab;
    protected GameObject spellPrefabInstantiate;
    protected Animator animPrefab;
    protected Animator animPrefabInstantiate;
    public float damageAmount;
    public float manaCost;
    public int selectedSpell;

    public Player player;

    public ContactFilter2D contactFilter;

    //work needed on line 77 and here to allow for switching through spells!



    // Start is called before the first frame update
    void Start()
    {
        spellbookColor = spellBook.GetComponent<SpriteRenderer>().color;
        spellBook.GetComponent<SpriteRenderer>().color = Color.clear;
        currentCastDownTime = 0f;
        animSpellBook = spellBook.GetComponent<Animator>();
        spellPrefabInstantiate = Instantiate(spellPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        animPrefabInstantiate = spellPrefabInstantiate.GetComponent<Animator>();
        selectedSpell = 0;

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
                failSpell.PlayOneShot(failSpellClip, 1f);
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
