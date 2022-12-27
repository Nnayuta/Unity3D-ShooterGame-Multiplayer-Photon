using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    [SerializeField]
    private Text _Name;
    [SerializeField]
    private Image _img;
    [SerializeField]
    private Object_Character _character;

    public Object_Character Character { get; private set; }

    public void SetCharacterInfo(Object_Character character)
    {
        Character = character;
        _character = character;
        _Name.text = character.Name;
        _img.sprite = character.Avatar;
    }
}
