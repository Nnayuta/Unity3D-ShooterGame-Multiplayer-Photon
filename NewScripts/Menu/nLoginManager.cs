using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class login
{
    public string _userid;
    public int _actorID;
    public string _username;

    public login(string userid, int actorID, string username)
    {
        _userid = userid;
        _actorID = actorID;
        _username = username;
    }
}

public class nLoginManager : MonoBehaviourPunCallbacks
{
    public login accountInfo;

    #region singleton
    public static nLoginManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);

    }
    #endregion

    public InputField username;

    public void entrar()
    {
        if (username.text == " " || username.text == null || username.text == "")
        {
            Debug.Log("> [ERRO] Digite seu nome de usuario");
            return;
        }

        if (username.text.StartsWith(" ") || username.text.EndsWith(" "))
        {
            Debug.Log("> [ERRO] Não e permitido espaço no inicio ou no final do nome");
            return;
        }

        OnLoginEnter();
    }

    void OnLoginEnter()
    {
        PhotonNetwork.NickName = username.text;
        accountInfo._userid = PhotonNetwork.LocalPlayer.UserId;
        accountInfo._actorID = PhotonNetwork.LocalPlayer.ActorNumber;
        accountInfo._username = PhotonNetwork.LocalPlayer.NickName;
        NetworkManager.Instancia.InterfaceManager("Menu");
    }

}
