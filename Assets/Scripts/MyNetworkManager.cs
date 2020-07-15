using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public event System.Action<NetworkConnection> E_OnServerReady;

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);
        E_OnServerReady?.Invoke(conn);
    }
}
