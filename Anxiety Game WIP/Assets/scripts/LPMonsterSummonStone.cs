using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LPMonsterSummonStone : LightPillars
{
    public GameObject summonedSlime;
    public RaycastHit2D checkForPlayer;
    public EnemyController enemyController;



    public override void HitByBright()
    {
        gameObject.GetComponent<Light2D>().enabled = true;
        summonedSlime.SetActive(true);

        /*int layerMask = LayerMask.GetMask("Player");

        checkForPlayer = Physics2D.Raycast(summonedSlime.transform.position, Vector3.right, 5f, layerMask);
        if (checkForPlayer.collider != null)
        {
            summonedSlime.GetComponent<Animator>().SetFloat("Look X", .5f);
            summonedSlime.GetComponent<Animator>().SetFloat("Look Y", 0f);
            return;
        }

        checkForPlayer = Physics2D.Raycast(summonedSlime.transform.position, Vector3.left, 5f, layerMask);
        if (checkForPlayer.collider != null)
        {
            summonedSlime.GetComponent<Animator>().SetFloat("Look X", -.5f);
            summonedSlime.GetComponent<Animator>().SetFloat("Look Y", 0f);
            return;
        }

        checkForPlayer = Physics2D.Raycast(summonedSlime.transform.position, Vector3.up, 5f, layerMask);
        if (checkForPlayer.collider != null)
        {
            summonedSlime.GetComponent<Animator>().SetFloat("Look X", 0f);
            summonedSlime.GetComponent<Animator>().SetFloat("Look Y", .5f);
            return;
        }

        checkForPlayer = Physics2D.Raycast(summonedSlime.transform.position, Vector3.down, 5f, layerMask);
        if (checkForPlayer.collider != null)
        {
            summonedSlime.GetComponent<Animator>().SetFloat("Look X", 0f);
            summonedSlime.GetComponent<Animator>().SetFloat("Look Y", -.5f);
            return;
        }
        */
    }

}