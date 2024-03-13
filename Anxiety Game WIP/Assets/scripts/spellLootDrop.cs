using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellLootDrop : MonoBehaviour
{

    public GameObject hero;
    public Player player;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.Find("Hero");
        player= hero.GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("pickedUp");
        }
    }

    public void WhenDropped()
    {
        audioSource.Play(0);
    }
    public void whenPickedUp()
    {
        player.selectedSpellMax = 3;
        GameManager.tutorialFlameSpellObtained = true;
        Destroy(gameObject);
    }

}
