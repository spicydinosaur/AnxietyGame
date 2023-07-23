using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour

{

    public GameObject[] itemsInDropTable;
    public float[] itemDropChance;
    public float randomDropResult;




    public void rollOnTable()
    {
        randomDropResult = Random.Range(0, 100);
        Debug.Log("Random Drop Result: " + randomDropResult);
        for (int i = 0; i < itemsInDropTable.Length; i++)
        {
            //check if randomDropResult is in between two values. If this is true, clone the item in itemsInDropTable, then exit out of for loop!
            if (i == 0)
            {
                if (randomDropResult <= itemDropChance[0])
                {
                    deliverResult(itemsInDropTable[0]);
                    return;
                }

            }

            else if (randomDropResult > itemDropChance[i - 1] && randomDropResult <= itemDropChance[i])
            {
                Debug.Log("successful drop result: " + i.ToString());
                deliverResult(itemsInDropTable[i]);
                return;
            }


        }
    }
     
    protected void deliverResult(GameObject rolledDropTableItem)
    {

        if (rolledDropTableItem != null)
        {

            
            //check if item is IDroppable, if so call cloneAndDrop(). Otherwise, we figure that out later!
            if (rolledDropTableItem.GetComponent<IDroppable>() != null)
            {
            
                rolledDropTableItem.GetComponent<IDroppable>().cloneAndDrop(GetComponent<Transform>().transform.position);
            
            }

        }

    }



}
