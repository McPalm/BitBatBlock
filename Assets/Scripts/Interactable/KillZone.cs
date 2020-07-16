using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KillZone : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<PlatformingCharacter>();
        if(player)
        {
            StartCoroutine(Trigger(player));
        }
    }

    IEnumerator Trigger(PlatformingCharacter player)
    {
        FindObjectOfType<Checkpoint>().Respawn(player);
        var camera = FindObjectOfType<CameraFollow>();
        var renderer = player.GetComponentInChildren<SpriteRenderer>();
        camera.enabled = false;
        player.enabled = false;
        renderer.enabled = false;
        yield return new WaitForSeconds(.5f);
        camera.enabled = true;
        renderer.enabled = true;
        player.enabled = true;
    }
}
