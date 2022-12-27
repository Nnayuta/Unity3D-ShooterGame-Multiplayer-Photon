using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientConfig : MonoBehaviour
{

    public static ClientConfig Instancia { get; private set; }

    [Header("Client Config")]
    [SerializeField] public float Sens = 100f;

    private void Start()
    {
        Sens = PlayerPrefs.GetFloat("sensibilidade");
    }
}
