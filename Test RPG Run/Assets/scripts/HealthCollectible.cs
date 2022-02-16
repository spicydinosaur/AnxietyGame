using UnityEngine;

/// <summary>
/// Will handle giving health to the character when they enter the trigger.
/// </summary>
public class HealthCollectible : MonoBehaviour, IDroppable
{

    public Player target;

  
   

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (target.tombstone.activeSelf == false && collider.gameObject.CompareTag("Player"))
        {
            target.PlayerHealth(1);
            Destroy(gameObject);
        }
    }

    public void cloneAndDrop(Vector3 cloneSpawnLocation)
    {
        Instantiate(gameObject, cloneSpawnLocation, Quaternion.identity);


    }

}
