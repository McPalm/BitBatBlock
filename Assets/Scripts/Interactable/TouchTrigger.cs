using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class TouchTrigger : NetworkBehaviour
{
    public event System.Action OnLocalPickup;
    public event System.Action OnServerPickup;
    public event System.Action OnPickup;

    public bool HideOnTouch;
    public float cooldown;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var net = collision.GetComponent<NetworkControls>();
        if(net && net.isLocalPlayer)
        {
            OnLocalPickup?.Invoke();
            if (isServer)
                ServerPickup();
            else
                CmdPickup();
        }
    }

    [Command(ignoreAuthority = true)]
    void CmdPickup() => ServerPickup();

    void ServerPickup()
    {
        if(gameObject.activeSelf)
        {
            OnServerPickup?.Invoke();
            RpcPickup();
        }
    }

    [ClientRpc] void RpcPickup()
    {
        OnPickup?.Invoke();
        if (HideOnTouch)
            gameObject.SetActive(false);
    }
}
