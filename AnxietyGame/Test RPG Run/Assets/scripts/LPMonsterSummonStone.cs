using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LPMonsterSummonStone : LightPillars
{
    public GameObject summonedSlime;
    public override void HitByBright()
    {
        summonedSlime.SetActive(true);
        summonedSlime.transform.position = new Vector3(1f, -0.4f, 0f);
        summonedSlime.GetComponent<EnemyController>().FaceTheRightWay(Vector2.right);
        base.HitByBright();
    }

}