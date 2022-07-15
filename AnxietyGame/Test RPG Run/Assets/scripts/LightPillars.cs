using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class LightPillars : MonoBehaviour
{
	//Child scripts all start with the initials "LP," including LPMonsterSummonStone and LPSeasideRuins, and perhaps others if need be.
	public virtual void HitByBright()
	{

		gameObject.GetComponent<Light2D>().enabled = true;
		if (gameObject.GetComponent<PuzzleKey>().isActiveAndEnabled)
        {
			gameObject.GetComponent<PuzzleKey>().ChangeLockState(PuzzleKey.LockCheck.unlocked);
        }

	}

}