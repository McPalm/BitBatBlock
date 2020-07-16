using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName ="Character Data",  menuName ="Character Data", order = 0)]
public class CharacterData : ScriptableObject
{
    public Sprite Sprite;
    public PlatformingCharacterProperties PlatformingCharacterProperties;
}
