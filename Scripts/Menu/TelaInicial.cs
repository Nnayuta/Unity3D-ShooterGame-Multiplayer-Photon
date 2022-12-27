using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class TelaInicial : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text playerName;
    [SerializeField] private Image playerAvatar;
    [SerializeField] private Sprite[] listAvatar;

    public void setAvatar(int id)
    {
        playerAvatar.sprite = listAvatar[id];
    }

    private void Start()
    {
        playerName.text = PhotonNetwork.NickName;

        if (PhotonNetwork.NickName == "Nayuta" || PhotonNetwork.NickName == "nayuta")
        {
            playerAvatar.sprite = listAvatar[0];
        }
        else
        {
            playerAvatar.sprite = listAvatar[1];
        }

    }

}
