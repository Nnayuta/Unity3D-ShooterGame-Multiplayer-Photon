using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviourPunCallbacks
{
    [Header("List Player Room")]
    [SerializeField] private Transform _content;
    [SerializeField] private CharacterSelectionListPlayers _charSelectListPlayer;
    public List<CharacterSelectionListPlayers> _listings = new List<CharacterSelectionListPlayers>();

    [Header("Character Selection Settings")]
    public float Timer;
    float _timer;

    public GameObject characterSelection;
    bool mapLoadStart = false;
    public Text UI_text;
    public Text UI_Text_MapName;

    [Header("loadScreen")]
    public GameObject loadScreen;
    public Slider loadSlider;
    public Text loadScreenMapTitle;
    public Image loadScreenIMG;

    private void Start()
    {
        GetCurrentPlayerRoom();

        UI_Text_MapName.text = NetworkManager.Instancia.MapLoad.Name;
        loadScreenMapTitle.text = NetworkManager.Instancia.MapLoad.Name;
        loadScreenIMG.sprite = NetworkManager.Instancia.MapLoad.Image;
        _timer = Timer;

    }

    private void GetCurrentPlayerRoom()
    {
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }

    private void AddPlayerListing(Player player)
    {
        CharacterSelectionListPlayers listing = Instantiate(_charSelectListPlayer, _content);

        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            _listings.Add(listing);
        }
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _timer = 0;

            if (_timer == 0)
            {
                if (!mapLoadStart)
                {
                    photonView.RPC("loadMap", RpcTarget.All);
                    mapLoadStart = true;
                }
            }
        }

        ShowTimer(_timer);
    }

    void ShowTimer(float timerValue)
    {
        timerValue += 1;
        float minutes = Mathf.FloorToInt(timerValue / 60);
        float seconds = Mathf.FloorToInt(timerValue % 60);

        UI_text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    [PunRPC]
    void loadMap()
    {
        loadScreen.SetActive(true);
        characterSelection.SetActive(false);
        StartCoroutine(SyncLoadMap());
    }

    IEnumerator SyncLoadMap()
    {
        PhotonNetwork.LoadLevel(NetworkManager.Instancia.MapLoad.LevelName);
        while (PhotonNetwork.LevelLoadingProgress < 0)
        {
            loadSlider.value = PhotonNetwork.LevelLoadingProgress;
            yield return new WaitForEndOfFrame();
        }

    }
}
