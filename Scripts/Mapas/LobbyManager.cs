using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Lobby Map Select")]
    [SerializeField]
    private Dropdown mapSelection;
    [SerializeField]
    private Image mapImage;
    [SerializeField]
    private Text mapText;
    [SerializeField]
    private List<MapObject> ListaDeMapas = new List<MapObject>();
    [SerializeField]
    private int selectedMapID = 0;

    [Header("LoadScreen")]
    [SerializeField]
    private GameObject loadScreen;
    [SerializeField]
    private Image LoadScreenImg;
    [SerializeField]
    private Text LoadScreenMapName;
    [SerializeField]
    private Slider LoadSceenLoadBar;

    [SerializeField] private GameObject Menu;

    [Header("Lobby Buttons")]
    [SerializeField]
    private List<Button> listaBotoes = new List<Button>();
    [SerializeField]
    private List<Dropdown> listaDrop = new List<Dropdown>();

    [Header("List Player Room")]
    [SerializeField] private Transform _content;
    [SerializeField] private Transform _content2;
    Transform thContent;

    [SerializeField] private playerList _playerList;
    private List<playerList> _listings = new List<playerList>();


    //Pega os players da sala ao iniciar
    private void GetCurrentPlayerRoom()
    {
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }

    private void AddPlayerListing(Player player)
    {
        if (_listings.Count <= 4)
        {
            thContent = _content;
        }
        else if (_listings.Count > 4)
        {
            thContent = _content2;
        }

        playerList listing = Instantiate(_playerList, thContent);

        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            _listings.Add(listing);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }

    public override void OnJoinedRoom()
    {
        photonView.RPC("MapaSelect", RpcTarget.All, 0);

        GetCurrentPlayerRoom();

        for (int i = 0; i < listaBotoes.Count; i++)
        {
            listaBotoes[i].interactable = NetworkManager.Instancia.MasterRoom();
        }

        for (int i = 0; i < listaDrop.Count; i++)
        {
            listaDrop[i].interactable = NetworkManager.Instancia.MasterRoom();
        }
    }

    public void SelecionaMapa(int ID)
    {
        photonView.RPC("MapaSelect", RpcTarget.All, ID);
    }

    [PunRPC]
    public void MapaSelect(int ID)
    {
        selectedMapID = ID;
        mapSelection.value = ID;
        mapText.text = ListaDeMapas[ID].Name;
        mapImage.sprite = ListaDeMapas[ID].Image;

        NetworkManager.Instancia.MapLoad = ListaDeMapas[ID];
    }

    public void startGame()
    {
        photonView.RPC("loadMap", RpcTarget.All);
    }

    [PunRPC]
    private void loadMap()
    {
        Menu.SetActive(false);
        LoadScreenMapName.text = ListaDeMapas[selectedMapID].Name;
        LoadScreenImg.sprite = ListaDeMapas[selectedMapID].Image;
        loadScreen.SetActive(true);

        StartCoroutine(SyncLoadMap());
    }

    IEnumerator SyncLoadMap()
    {
        PhotonNetwork.LoadLevel(ListaDeMapas[selectedMapID].CharacterSelectionName);
        while (PhotonNetwork.LevelLoadingProgress < 0)
        {
            LoadSceenLoadBar.value = PhotonNetwork.LevelLoadingProgress;
            yield return new WaitForEndOfFrame();
        }

    }
}
