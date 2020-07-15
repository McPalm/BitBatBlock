using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeartCounter : NetworkBehaviour
{
    public UnityEvent<string> OnValueChanged;
    public UnityEvent<string> OnMaxChanged;
    public UnityEvent<bool> OnCompletedChanged;
    private int _collected;
    private int _max;

    public int Collected
    {
        get => _collected;
        set
        {
            if (_collected != value)
            {
                OnValueChanged.Invoke(value.ToString());
                _collected = value;
                if (isServer)
                    RpcSetCollected(_collected, _max);
                OnCompletedChanged.Invoke(Left == 0);
            }

        }
    }
    public int Max
    {
        get => _max;
        set
        {
            if (_max != value)
            {
                OnMaxChanged.Invoke(value.ToString());
                _max = value;
                if (isServer)
                    RpcSetCollected(_collected, _max);
                OnCompletedChanged.Invoke(Left == 0);
            }
        }
    }
    public int Left => Max - Collected;

    [ClientRpc] void RpcSetCollected(int collected, int max)
    {
        _collected = collected;
        _max = max;
        OnValueChanged.Invoke(collected.ToString());
        OnMaxChanged.Invoke(max.ToString());
        OnCompletedChanged.Invoke(Left == 0);
    }

    public void ResetCounter()
    {
        _collected = 0;
        _max = 0;
        RpcSetCollected(0, 0);
        OnValueChanged.Invoke("0");
        OnMaxChanged.Invoke("0");
        OnCompletedChanged.Invoke(true);
    }
}
