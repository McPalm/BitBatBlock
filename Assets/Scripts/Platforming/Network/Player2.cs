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
        FindObjectOfType<CharacterAssigner>().Assign(this);
    }

    public void Assign(int number, CharacterData data)
    {
        playerNumber = number;
        SpriteRenderer.sprite = data.Sprite;
        GetComponent<PlatformingCharacter>().Properties = data.PlatformingCharacterProperties;
    }
}
