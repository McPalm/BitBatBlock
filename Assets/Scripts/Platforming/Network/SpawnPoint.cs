using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnPoint : MonoBehaviour
{
    public int playerNumber = 0;

    IEnumerator Start()
    {
        Player2[] players = null;
        while(players == null || players.Length == 0)
        {
            yield return null;
            players = FindObjectsOfType<Player2>();
        }
        foreach (var player in players)
        {

            if(player.playerNumber == playerNumber)
            {
                if(player.GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    player.transform.position = (Vector2)transform.position;
                    break;
                }
            }
        }
    }
}
