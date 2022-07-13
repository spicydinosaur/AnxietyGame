using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LPLightTotem : LightPillars
{
    // Start is called before the first frame update

    public GameObject pixieBoss;

    public override void HitByBright()
    {

        gameObject.GetComponentInChildren<Light2D>().enabled = true;
        gameObject.GetComponent<Animator>().SetBool("pillarActivated", true);

    }

    public void PixieSpawn()
    {
        pixieBoss.transform.position = new Vector3(-63f, 12.76f, 0f);
        gameObject.GetComponent<Light2D>().enabled = false;
        gameObject.GetComponentInParent<GameObject>().SetActive(false);
    }

}
