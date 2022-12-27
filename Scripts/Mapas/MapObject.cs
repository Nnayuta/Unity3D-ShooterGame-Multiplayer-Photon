using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new map", menuName = "mapa/criar")]
public class MapObject : ScriptableObject
{
    [Header("Character Selection")]
    public Object CharacterSelection;
    public string CharacterSelectionName = "CharacterSelection";

    [Header("Map to load")]
    public Object Object;
    public string LevelName;
    public int LevelID;

    [Header("Map to load Informations")]
    public string Name;
    public Sprite Image;
}
