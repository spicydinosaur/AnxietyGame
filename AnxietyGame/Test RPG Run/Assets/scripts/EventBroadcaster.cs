using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBroadcaster : MonoBehaviour
{

    //public delegate void HeroDeath();
    public static UnityEvent HeroDeath = new UnityEvent();

    public static void onHeroDeath()
    {
        if (HeroDeath != null)
        {
            HeroDeath.Invoke();
        }
    }

}
