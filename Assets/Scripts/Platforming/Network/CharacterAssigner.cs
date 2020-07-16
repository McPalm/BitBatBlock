using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterAssigner : NetworkBehaviour
{
    public CharacterData Player1;
    public CharacterData Player2;

    Player2 player1object;
    Player2 player2object;

    public void Assign(Player2 target)
    {
        if(isServer)
        {
            if(player1object == null)
            {
                RpcAssign(target.GetComponent<NetworkIdentity>(), 0);
                player1object = target;
            }
            else
            {
                RpcAssign(target.GetComponent<NetworkIdentity>(), 1);
                player2object = target;
            }
        }
    }

    [ClientRpc( channel = Channels.DefaultReliable)] void RpcAssign(NetworkIdentity target, int playerNumber)
    {
        CharacterData data = playerNumber == 0 ? Player1 : Player2;
        target.GetComponent<Player2>().Assign(playerNumber, data);
    }
}
