using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathCloudDeactivate : MonoBehaviour
{
    // Start is called before the first frame update

    private NPCController npcController;

    public void OnEnable()
    {
        npcController = GetComponentInParent<NPCController>();
    }
    public void Deactivate()
    {

        gameObject.GetComponent<Animator>().SetBool("isDead", false);
        gameObject.GetComponentInParent<GameObject>().SetActive(false);


    }

}
