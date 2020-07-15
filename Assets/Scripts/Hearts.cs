using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    Transform p1;
    Transform p2;
    public ParticleSystem ParticleSystem;

    float connect = 0f;

    public bool Enabled { get; set; }

    public float TimeClose => connect == 0f ? 0 : Time.timeSinceLevelLoad - connect;

    // Update is called once per frame
    void Update()
    {
        if(p1 == null || p2 == null)
        {
            ParticleSystem.Stop();
            var players = FindObjectsOfType<PlatformingCharacter>();
            if(players.Length == 2)
            {
                p1 = players[0].transform;
                p2 = players[1].transform;
            }
        }
        else
        {
            float distance = Vector2.SqrMagnitude(p1.transform.position - p2.transform.position);
            transform.position = (p1.transform.position + p2.transform.position) / 2f;
            if (ParticleSystem.isPlaying && (distance > 4f || !Enabled))
            {
                ParticleSystem.Stop();
                connect = 0f;
            }
            else if (!ParticleSystem.isPlaying && distance < 4f && Enabled)
            {
                ParticleSystem.Play();
                connect = Time.timeSinceLevelLoad;
            }

        }
    }
}
