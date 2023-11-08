using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceSpellTemplate : SpellTemplate
{
    public Animator animator;
    public Collider2D spellCollider2D;
    public Animator heroAnim;
    
    public AudioSource spellSound;
    public enum LayersInMask { Enemy, Scenery, NPC, Player, Allies }
    [SerializeField]
    public List<LayersInMask> layerMasksToHit;
    [SerializeField]

    public List<string> combinedLayerString;
    public float healthAmount;
    public float manaAmount;

    public float spawnDirectionModifier;

    public void OnEnable()
    {
        spellCollider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        heroAnim = hero.GetComponent<Animator>();

        if (spawnDirectionModifier == 0)
        {
            spawnDirectionModifier = .7f;
        }

        foreach (LayersInMask value in layerMasksToHit)
        {
            combinedLayerString.Add(value.ToString());
        }


    }
    public override void castSpell()
    {
        spellSound.Play(0);
        casterTransform = hero.transform.position;
        direction = player.lookDirection;

        //is it looking left?
        if (heroAnim.GetFloat("Look X") >= .5f && heroAnim.GetFloat("Look Y") >= -.69f && heroAnim.GetFloat("Look Y") < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270);
            direction.x = spawnDirectionModifier;
        }
        //is it looking right?
        else if (heroAnim.GetFloat("Look X") <= -0.5f && heroAnim.GetFloat("Look Y") >= -.69f && heroAnim.GetFloat("Look Y") < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90);
            direction.x = -spawnDirectionModifier;
        }
        //is it looking backward?
        else if (heroAnim.GetFloat("Look Y") >= .5f && heroAnim.GetFloat("Look X") >= -.69f && heroAnim.GetFloat("Look X") < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
            direction.y = spawnDirectionModifier;
        }
        else //we are looking forward
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180);
            direction.y = -spawnDirectionModifier;
        }

        spellStartPoint = new Vector3(casterTransform.x + direction.x, casterTransform.y + direction.y, 0f);
        //angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
        transform.position = spellStartPoint;

        spellTimer.spellIconMask.fillAmount = 1f;
        spellCollider2D.enabled = true;
        //below must be negativw or player will gain mana;
        animator.SetBool("isCasting", true);
        player.PlayerMana(manaCost);
        currentCastDownTime = castDownTime;
        Debug.Log("Slice spell's castSpell function triggered!");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var collidingObject = collision.gameObject;
        string collidingObjectString = collidingObject.tag.ToString();


        foreach (string checkLayer in combinedLayerString)
        {

            if (collidingObject.CompareTag(checkLayer))
            {
                Debug.Log("collidingObject.CompareTag(checkLayer) = " + checkLayer);
                Debug.Log("collidingObjectString = " + collidingObjectString);
                if (collidingObjectString == "Enemy")
                {
                    Debug.Log("collidingObject.layer = Enemy");
                    if (healthAmount != 0f)
                    {
                        //healthAmount must be a negative in the UI for it to damage, otherwise it will heal!
                        collidingObject.GetComponent<NPCHealth>().ChangeNPCHealth(healthAmount);
                        //Below will need to be tested with a variety of enemies, as it may make the spell warp from one place as its projectile,
                        //for too much of a distance to where the spell effect is seen.
                    }
                    if (manaAmount != 0f)
                    {
                        //possibly useful later.
                    }
                }
                else if (collidingObjectString == "Player")
                {
                    if (healthAmount != 0f)
                    {
                        //healthAmount must be a negative in the UI for it to damage, otherwise it will heal!
                        collidingObject.GetComponent<Player>().PlayerHealth(healthAmount);
                    }
                    if (manaAmount != 0f)
                    {
                        //manaAmount must be a negative in the UI for it to damage, otherwise it will heal!
                        collidingObject.GetComponent<Player>().PlayerMana(manaAmount);
                    }
                }
                else if (collidingObjectString == "Allies")
                {
                    if (healthAmount != 0f)
                    {
                        //healthAmount must be a negative in the UI for it to damage, otherwise it will heal!
                        collidingObject.GetComponent<NPCHealth>().ChangeNPCHealth(healthAmount);
                    }
                    if (manaAmount != 0f)
                    {
                        //possibly useful later.
                    }
                }
                break;
            }
        }

    }

    public void OnSpellEnd()
    {
        spellCollider2D.enabled = false;
        animator.SetBool("isCasting", false);
        Debug.Log("Slice spell's OnSpellEnd function triggered!");
    }

}
