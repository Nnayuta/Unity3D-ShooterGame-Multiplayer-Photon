using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MenuLogin : MonoBehaviourPunCallbacks
{
    public Text login;
    public GameObject error;
    public Text textError;
    int errorTime = 3;

    [Header("Menus")]
    public GameObject UI_Login;
    public GameObject UI_Menu;

    public void Logar()
    {
        if (login.text == " " || login.text == null || login.text == "")
        {
            StartCoroutine(ErrorMenu(errorTime, "Digite seu username"));
            return;
        }

        if (login.text.StartsWith(" ") || login.text.EndsWith(" "))
        {
            StartCoroutine(ErrorMenu(errorTime, "Remova o espaço do inicio ou do final"));
            return;
        }

        if (login.text.Length < 3)
        {
            StartCoroutine(ErrorMenu(errorTime, "Nome muito pequeno"));
            return;
        }

        if (login.text.Length > 10)
        {
            StartCoroutine(ErrorMenu(errorTime, "Nome muito grande"));
            return;
        }
        error.SetActive(false);

        ConnectionSucess();
    }


    void ConnectionSucess()
    {
      //  NetworkManager.Instancia.changeNickname(login.text);

        UI_Menu.SetActive(true);
        UI_Login.SetActive(false);
    }

    IEnumerator ErrorMenu(int wait, string erroText)
    {
        textError.text = erroText;
        error.SetActive(true);

        yield return new WaitForSeconds(wait);
        error.SetActive(false);
    }

}
