using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LPSeasideRuins : LightPillars
{
    public Animator animator;
    public void Awake()
    {
        animator = GetComponent<Animator>();
        if (GameManager.tutorialRuinsEntranceRevealed) 
        {
            animator.SetTrigger("isLit");
        }

    }
    public override void HitByBright()
    {
        animator.SetTrigger("isLit");
        gameObject.GetComponent<Collider2D>().enabled = false;
        GameManager.tutorialRuinsEntranceRevealed = true;
    }

  }
