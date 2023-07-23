using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellcastFinished : MonoBehaviour
{
    // Start is called before the first frame update

    public void OnSpellEnd()
    {
        Destroy(gameObject);
    }

}
