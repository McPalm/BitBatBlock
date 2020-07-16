using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Checkpoint : MonoBehaviour
{
    float next = 0f;

    public SpriteRenderer SpriteRenderer;

    public void Respawn(PlatformingCharacter player)
    {
        if (player.GetComponent<NetworkIdentity>() == null)
            Reset(player);
        else if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            Reset(player);
    }

    void Reset(PlatformingCharacter player)
    {
        if (next > Time.timeSinceLevelLoad)
            return;
        FindObjectOfType<PartyStreamer>().Pop(player.transform.position);
        player.transform.position = transform.position;
        next = Time.timeSinceLevelLoad + .1f;
    }
}
