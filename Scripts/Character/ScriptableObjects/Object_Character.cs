using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/New Character")]
[System.Serializable]
public class Object_Character : ScriptableObject
{
    [Header("Profile")]
    [SerializeField] public int ID;
    [SerializeField] public string Name;
    [SerializeField] public Sprite Avatar;

    [Header("Prefab")]
    [SerializeField] public string Location;
    [SerializeField] public GameObject Object;

    [Header("Status Padrao")]
    [SerializeField] public float Health;
    [SerializeField] public float Stamina;

    [Header("Status")]
    [SerializeField] public float MovSpeed;
    [SerializeField] public float ShiftSpeed;     //PADRAO: shiftspeed = movspeed / 2f
    [SerializeField] public float AirSpeed;      //PADRAO: AirSeed = movspeed / 1.5f
    [SerializeField] public float JumpHeight;
    [SerializeField] public float gravity = -9.81f;

    [Header("Survival Status")]
    [SerializeField] public float Strength;
    [SerializeField] public float Agility;
    [SerializeField] public float Intellect;

    [Header("Habilidades")]
    [SerializeField] public Object_Skills[] Skills;
}
