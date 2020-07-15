using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchStatus : NetworkBehaviour
{
    [SyncVar]
    public bool isOn;
    public event System.Action<bool> EventOnSet;

    public bool IsBlue => isOn;
    public bool IsOrange => !isOn;

    float cooldown = 0f;
    bool Avilable => cooldown < Time.timeSinceLevelLoad;

    public void HitSwitch()
    {
        if(Avilable)
        {
            cooldown = Time.timeSinceLevelLoad + .5f;
            CmdHitSwitch();
        }
    }

    [Command(ignoreAuthority =true)] void CmdHitSwitch()
    {
        isOn = !isOn;
        RpcHitSwitch(isOn);
    }
    [ClientRpc] void RpcHitSwitch(bool isOn) => EventOnSet(isOn);

}
