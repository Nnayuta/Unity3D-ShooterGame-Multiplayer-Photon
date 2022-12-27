using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openMenu : MonoBehaviour
{

    public static void MenuAberto(bool abrir)
    {
        if (abrir)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
