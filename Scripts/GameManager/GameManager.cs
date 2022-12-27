using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instancia { get; private set; }

    [Header("Lista de Jogadores")]
    public List<Client_Player> ListaDeJogadores;

    [Header("Game")]
    [SerializeField] private int playerValue;
    [SerializeField] private int playersIngame;
    [SerializeField] public Transform[] _spawns;

    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        photonView.RPC("SendPlayer", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void SendPlayer()
    {
        playerValue++;
        if (playerValue == PhotonNetwork.PlayerList.Length)
        {
            foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
            {
                addPlayer(playerInfo.Value);
            }
        }
    }

    private void addPlayer(Player player)
    {
        ListaDeJogadores.Add(new Client_Player(player.ActorNumber, player.NickName));

        ListaDeJogadores.Sort(sort);
    }

    [PunRPC]
    void addCharactertoPlayer(int id, string characterLocation)
    {
        ListaDeJogadores[id].characterLocation = characterLocation;
    }

    private int sort(Client_Player a, Client_Player b)
    {
        if (a.ID < b.ID)
        {
            return -1;
        }
        else if (a.ID > b.ID)
        {
            return 1;
        }
        return 0;
    }

    [PunRPC]
    private void SpawnPlayers()
    {
        playersIngame++;
        if(playersIngame == PhotonNetwork.PlayerList.Length)
        {
            createPlayers();
        }
    }

    private void createPlayers()
    {
        GameObject playerObj = PhotonNetwork.Instantiate(ListaDeJogadores[PhotonNetwork.LocalPlayer.ActorNumber].characterLocation, _spawns[Random.Range(0, _spawns.Length)].position, Quaternion.identity);
        playerObj.layer = 8;
        playerObj.name = $"ID: {PhotonNetwork.LocalPlayer.ActorNumber} - Nome: {PhotonNetwork.LocalPlayer.NickName}";
        ListaDeJogadores[PhotonNetwork.LocalPlayer.ActorNumber].playerObject = playerObj;
    }

}
