using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player2 : MonoBehaviour
{
    public int playerNumber = 0;

    public Sprite Player2Sprite;
    public SpriteRenderer SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        var id = GetComponent<NetworkIdentity>();
        if(id.isLocalPlayer == id.isServer)
        {
            SpriteRenderer.sprite = Player2Sprite;
            playerNumber = 1;
        }
    }
}
