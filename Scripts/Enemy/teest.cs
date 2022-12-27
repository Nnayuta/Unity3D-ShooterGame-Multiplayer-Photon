using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teest : MonoBehaviour
{
    #region Singleton
    public static teest instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject player;
}
