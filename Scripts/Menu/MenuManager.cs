using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public GameObject Inicio, Jogar;

    public InputField roomNameEntrar;
    public InputField roonNameCriar;

    public GameObject Salas;
    public GameObject Personalizado;

    void Start()
    {
        Inicio.SetActive(true);
        Jogar.SetActive(false);

        Salas.SetActive(true);
        Personalizado.SetActive(false);
    }

    public void btn_Jogar()
    {
        Inicio.SetActive(false);
        Jogar.SetActive(true);
    }

    public void btn_Inicio()
    {
        Inicio.SetActive(true);
        Jogar.SetActive(false);
    }

    public void btn_EntraSala()
    {
        NetworkManager.Instancia.joinRoom(roomNameEntrar.text);
    }

    public void btn_CriarSala()
    {
        NetworkManager.Instancia.createRoom(roonNameCriar.text);
    }
    
    public void leaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Salas.SetActive(true);
        Personalizado.SetActive(false);
    }

    public override void OnLeftRoom()
    {
        photonView.RPC("ListaJogadores", RpcTarget.AllBuffered);
    }
    public override void OnJoinedRoom()
    {
        Salas.SetActive(false);
        Personalizado.SetActive(true);
        NetworkManager.Instancia.photonView.RPC("ListaJogadores", RpcTarget.AllBuffered);
    }
}
