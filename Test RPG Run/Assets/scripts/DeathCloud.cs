using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCloud : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator deathCloudAnim;

    public void Start()
    {
        deathCloudAnim = GetComponent<Animator>();
    }

    public void OnAnimationEnd()
    {
        deathCloudAnim.SetBool("isDead", false);
        GetComponentInParent<GameObject>().SetActive(false);

    }
}
