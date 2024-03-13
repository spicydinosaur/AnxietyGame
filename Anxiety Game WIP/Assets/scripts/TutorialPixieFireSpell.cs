using PixelCrushers.DialogueSystem.UnityGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPixieFireSpell : MonoBehaviour
{
    public GameObject hero;
    public Player player;
    public AudioSource audioSource;
    public Animator animator;
    public float healthModifier;
    public Animator pixieAnim;
    public Transform pixieTransform;
    public List<Vector3> spawnDirectionModifier;
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.Find("Hero");
        player = hero.GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

    }

    public void SpellStarting()
    {
        audioSource.Play(0);
        Debug.Log("TutorialPixieFireSpell script: SpellStarting() firing.");
        //is it looking left?
        if (pixieAnim.GetFloat("Look X") >= .5f && pixieAnim.GetFloat("Look Y") >= -.69f && pixieAnim.GetFloat("Look Y") < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
            direction = pixieTransform.position + spawnDirectionModifier[0];
        }
        //is it looking right?
        else if (pixieAnim.GetFloat("Look X") <= -0.5f && pixieAnim.GetFloat("Look Y") >= -.69f && pixieAnim.GetFloat("Look Y") < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180);
            direction = pixieTransform.position + spawnDirectionModifier[1];
        }
        //is it looking backward?
        else if (pixieAnim.GetFloat("Look Y") >= .5f && pixieAnim.GetFloat("Look X") >= -.69f && pixieAnim.GetFloat("Look X") < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270);
            direction = pixieTransform.position + spawnDirectionModifier[2];
        }
        else //we are looking forward
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90);
            direction = pixieTransform.position + spawnDirectionModifier[3];
        }
        
        transform.position = direction;
        animator.SetTrigger("isCasting");
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TutorialPixieFireSpell script: collision for ontriggerenter() = " + collision + ". healthModifier = " + healthModifier + " Note that the variable is adjusted by default with a - inside the script.");
        if (collision == hero)
        {
            player.PlayerHealth(-healthModifier);
            Debug.Log("TutorialPixieFireSpell script: collision == " + collision);
        }
    }

    public void SpellEnding()
    {
        //what is disabling the animator at this point for the spell???
        Debug.Log("TutorialPixieFireSpell script: SpellEnding() firing.");
        audioSource.Stop();
        gameObject.SetActive(false);

    }
    // Update is called once per frame

}
