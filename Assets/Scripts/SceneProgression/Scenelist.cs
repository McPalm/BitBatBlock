using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="New Scene List", menuName ="Scene List", order = 0)]
public class Scenelist : ScriptableObject
{
    public string[] Scenes;
}
