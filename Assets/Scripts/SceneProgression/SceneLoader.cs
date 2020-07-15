using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class SceneLoader : NetworkBehaviour
{
    string activeScene;

    private void Start()
    {
        FindObjectOfType<MyNetworkManager>().E_OnServerReady += SceneLoader_E_OnServerReady;
    }

    private void SceneLoader_E_OnServerReady(NetworkConnection conn)
    {
        if (!string.IsNullOrEmpty(activeScene))
        {
            var message = new SceneMessage()
            {
                sceneOperation = SceneOperation.LoadAdditive,
                sceneName = activeScene,
            };
            conn.Send(message);
        }
    }

    void UnloadScene()
    {
        if(!string.IsNullOrEmpty(activeScene))
        {
            SceneManager.UnloadSceneAsync(activeScene);
            var message = new SceneMessage()
            {
                sceneOperation = SceneOperation.UnloadAdditive,
                sceneName = activeScene,
            };
            foreach(var connection in NetworkServer.connections)
            {
                connection.Value.Send(message);
            }
        }
    }

    public void LoadScene(string name)
    {
        UnloadScene();
        activeScene = name;
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        var message = new SceneMessage()
        {
            sceneOperation = SceneOperation.LoadAdditive,
            sceneName = activeScene,
        };
        foreach (var connection in NetworkServer.connections)
        {
            connection.Value.Send(message);
        }
    }
}
