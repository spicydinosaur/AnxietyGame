using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCollectible : MonoBehaviour
{
    public Player target;


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (target.tombstone.activeSelf == false && collider.gameObject.CompareTag("Player"))
        {
            target.PlayerMana(1);
            Destroy(gameObject);
        }
    }

    public void cloneAndDrop(Vector3 cloneSpawnLocation)
    {
        Instantiate(gameObject, cloneSpawnLocation, Quaternion.identity);


    }
}
