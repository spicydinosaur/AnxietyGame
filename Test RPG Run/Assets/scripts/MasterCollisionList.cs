using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCollisionList : PersistentSingleton<MasterCollisionList>
{
    public List<NPCSortingLayerSwap> collidingTransforms = new List<NPCSortingLayerSwap>(10);
    public bool doRecalculations = false;
    public int RecalculateOnFrame = 5;
    public int CurrentFrame = 0;
    protected int totalNulls = 0;

    public int AddNewCollision(Transform newCollision)
    {
        if (collidingTransforms.Count + 1 >= collidingTransforms.Capacity)
        {
            collidingTransforms.Capacity += 5;
        }
        collidingTransforms.Add(newCollision.GetComponent<NPCSortingLayerSwap>());
        RecalculatePositions();
        doRecalculations = true;
        return collidingTransforms.Count - 1;
    }

    public bool RemoveCollision(int removeCollisionAtPosition)
    {
        collidingTransforms[removeCollisionAtPosition] = null;
        totalNulls++;
        if (totalNulls > 10 && totalNulls >= (int)(collidingTransforms.Count * 0.1))
        {
            for (int i = collidingTransforms.Count -1; i > -1; i--)
            {

                if (collidingTransforms[i] == null) collidingTransforms.RemoveAt(i);


            }

            totalNulls = 0;

            collidingTransforms.TrimExcess();
            for (int i = 0; i < collidingTransforms.Count; i++)
            {
                collidingTransforms[i].mclPosition = i;
            }
        }
        if (collidingTransforms.Count == 0) doRecalculations = false;
        return true;
    }
    private void RecalculatePositions()
    {
        for (int i = 0; i < collidingTransforms.Count; i++)
        {
            if (collidingTransforms[i] == null) continue;

            collidingTransforms[i].RecalculatePosition();
        }
    }

    public void Update()
    {
        if (doRecalculations)
        {
            CurrentFrame++;
            if (CurrentFrame >= RecalculateOnFrame)
                RecalculatePositions();
            CurrentFrame = 0;
        }
    }
}
