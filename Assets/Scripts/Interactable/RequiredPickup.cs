using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TouchTrigger))]
public class RequiredPickup : MonoBehaviour
{
    void Start()
    {
        var pickup = GetComponent<TouchTrigger>();
        if(pickup.isServer)
        {
            pickup.OnServerPickup += Pickup_OnServerPickup;
            FindObjectOfType<HeartCounter>().Max++;
        }
    }

    private void Pickup_OnServerPickup()
    {
        FindObjectOfType<HeartCounter>().Collected++;
    }
}
