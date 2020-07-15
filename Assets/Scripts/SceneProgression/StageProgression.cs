using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class StageProgression : NetworkBehaviour
{
    public Scenelist scenelist;
    public Hearts hearts;
    public HeartCounter heartCounter;

    float cooldown = 0f;
    int sceneCount = 0;

    void Start()
    {
        GetComponent<SceneLoader>().LoadScene(scenelist.Scenes[0]);
        cooldown = Time.timeSinceLevelLoad + 5f;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > cooldown && hearts.TimeClose > 1f && heartCounter.Left == 0)
            Win();
    }

    private void Win()
    {
        heartCounter.ResetCounter();
        cooldown = Time.timeSinceLevelLoad + 5f;
        sceneCount++;
        GetComponent<SceneLoader>().LoadScene(scenelist.Scenes[sceneCount % scenelist.Scenes.Length]);
    }
}
