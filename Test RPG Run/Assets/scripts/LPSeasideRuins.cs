using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LPSeasideRuins : LightPillars
{

    public void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }
    public override void HitByBright()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(198f, 198f, 198f, 1f);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

  }
