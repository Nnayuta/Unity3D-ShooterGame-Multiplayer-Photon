using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Client_Player
{
    [Header("Player")]
    [SerializeField]
    public int ID;
    [SerializeField]
    public string Name;
    [SerializeField]
    public string characterLocation;
    [SerializeField]
    public GameObject playerObject;

    public Client_Player(int iD, string name)
    {
        ID = iD;
        Name = name;
    }
}
