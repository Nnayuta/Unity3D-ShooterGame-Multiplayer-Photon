using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nMenuManager : MonoBehaviourPunCallbacks
{
    #region lista de amigos
    [Header("Friend List")]
    [SerializeField] private Animator FriendListAnimator;
    [SerializeField] private Text userName;
    [SerializeField] public GameObject[] ListaAmigos;

    public void Lista_Amigos()
    {
        bool open = FriendListAnimator.GetBool("Open");
        FriendListAnimator.SetBool("Open", !open);

        if (open)
        {
            updatePlayerInfo();
        }
    }

    void updatePlayerInfo()
    {
        userName.text = PhotonNetwork.NickName;
    }
    #endregion

    private void Start()
    {
        updatePlayerInfo();
    }

    #region Chat Config
    [Header("Chat")]
    public Animator ChatAnimator;

    public void Chat()
    {
        bool open = ChatAnimator.GetBool("Open");
        ChatAnimator.SetBool("Open", !open);
    }
    #endregion


}
