using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PartyStreamer : NetworkBehaviour
{

    float nextWhen;
    public ParticleSystem ParticleSystem;
    public float duration = .5f;

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem.Stop();
    }

    public void Pop(Vector2 location)
    {
        StopAllCoroutines();
        StartCoroutine(Pop_CR(location));
        CmdPop(location);
    }

    public IEnumerator Pop_CR(Vector2 location)
    {
        transform.position = location;
        ParticleSystem.Play();
        yield return new WaitForSeconds(duration);
        ParticleSystem.Stop();
    }

    [Command(channel = Channels.DefaultUnreliable, ignoreAuthority = true)] public void CmdPop(Vector2 location) => RpcPop(location);
    [ClientRpc(channel = Channels.DefaultUnreliable)]
    public void RpcPop(Vector2 location)
    {
        StopAllCoroutines();
        StartCoroutine(Pop_CR(location));
    }
}
