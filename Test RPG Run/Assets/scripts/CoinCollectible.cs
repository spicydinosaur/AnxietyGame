using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectible : MonoBehaviour, IDroppable
{

    public Player target;




    void OnTriggerEnter2D(Collider2D collider)
    {
        if (target.gameObject.activeSelf && collider.gameObject.CompareTag("Player"))
        {
            target.PlayerCoins(1);
            Destroy(gameObject);
        }
    }

    public void cloneAndDrop(Vector3 cloneSpawnLocation)
    {
        Instantiate(gameObject, cloneSpawnLocation, Quaternion.identity);


    }

}
