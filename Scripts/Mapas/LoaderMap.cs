using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LoaderMap : MonoBehaviourPunCallbacks
{

    [SerializeField] public Transform[] _spawns;

    private void Start()
    {
        GameManager.Instancia._spawns = _spawns;

        GameManager.Instancia.photonView.RPC("SpawnPlayers", RpcTarget.AllBuffered);
    }
}
