using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instancia { get; private set; }

    [Header("Server Status")]
    public string serverLog;

    public GameObject UI_connecting;
    public GameObject UI_Login;
    public GameObject UI_Menu;

    [Header("Map")]
    [SerializeField] public MapObject MapLoad;


    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(this);
            return;
        }
        Instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InterfaceManager("Connecting");
        serverLog = "Starting connection to server";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        InterfaceManager("Login");
        serverLog = "Connected to server.";
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void InterfaceManager(string toUI)
    {
        switch (toUI)
        {
            case "Connecting":
                UI_connecting.SetActive(true);
                UI_Login.SetActive(false);
                UI_Menu.SetActive(false);
                break;
            case "Login":
                UI_connecting.SetActive(false);
                UI_Login.SetActive(true);
                UI_Menu.SetActive(false);
                break;
            case "Menu":
                UI_connecting.SetActive(false);
                UI_Login.SetActive(false);
                UI_Menu.SetActive(true);
                break;
        }
    }

    // GameSettings

    public void joinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void createRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }

    public bool MasterRoom()
    {
        return PhotonNetwork.IsMasterClient;
    }
}