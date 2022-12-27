using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class ListCharacter : MonoBehaviourPunCallbacks
{
    public Transform content;
    public SelectCharacter _characterSeleted;

    public List<Object_Character> Characters = new List<Object_Character>();

    [SerializeField]
    private List<SelectCharacter> _listing = new List<SelectCharacter>();

    private void Start()
    {
        foreach (Object_Character _character in Characters)
        {
            listCharacters(_character);
        }
    }

    void listCharacters(Object_Character character)
    {
        SelectCharacter listing = Instantiate(_characterSeleted, content);

        if (listing != null)
        {
            listing.SetCharacterInfo(character);
            listing.GetComponent<Button>().onClick.AddListener(delegate { setcharacter(listing); });
            _listing.Add(listing);
        }
    }

    public void setcharacter(SelectCharacter listing)
    {
        GameManager.Instancia.photonView.RPC("addCharactertoPlayer", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber ,listing.Character.Location);
    }
}
