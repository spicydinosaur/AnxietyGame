using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SliceSpellTemplate : SpellTemplate
{
    public List<Vector3> spawnDirectionModifier;

    public override void Awake()
    {
        spellCollider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        heroAnim = hero.GetComponent<Animator>();

        foreach (LayersInMask value in layerMasksToHit)
        {
            combinedLayerString.Add(value.ToString());
        }


    }
    public override void castSpell()
    {
        spellSound.Play(0);
        casterTransform = hero.transform.position;

        //is it looking left?
        if (heroAnim.GetFloat("Look X") >= .5f && heroAnim.GetFloat("Look Y") >= -.69f && heroAnim.GetFloat("Look Y") < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
            direction = casterTransform + spawnDirectionModifier[0];
        }
        //is it looking right?
        else if (heroAnim.GetFloat("Look X") <= -0.5f && heroAnim.GetFloat("Look Y") >= -.69f && heroAnim.GetFloat("Look Y") < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180);
            direction = casterTransform + spawnDirectionModifier[1];
        }
        //is it looking backward?
        else if (heroAnim.GetFloat("Look Y") >= .5f && heroAnim.GetFloat("Look X") >= -.69f && heroAnim.GetFloat("Look X") < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90);
            direction = casterTransform + spawnDirectionModifier[2];
        }
        else //we are looking forward
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270);
            direction = casterTransform + spawnDirectionModifier[3];
        }

     
        transform.position = direction;

        spellTimer.spellIconMask.fillAmount = 1f;
        spellCollider2D.enabled = true;
        //below must be negative or player will gain mana;
        animator.SetBool("isCasting", true);
        player.PlayerMana(manaCost);
        currentCastDownTime = castDownTime;
        Debug.Log("Slice spell's castSpell function triggered! Rotation = " + GetComponent<Transform>().rotation + ". Direction = " + direction);
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
