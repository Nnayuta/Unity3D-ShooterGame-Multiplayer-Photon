using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionListPlayers : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Image _CharImg;

    public Player Player { get; private set; }

    public void SetPlayerInfo(Player player)
    {
        Player = player;
        _text.text = player.NickName;

        this.gameObject.name = $"Player: '{player.NickName}'";
    }

    public void SetPlayerInfoSelectedCharacter(Player player, Sprite sprite)
    {
        Player = player;
        _text.text = player.NickName;
        _CharImg.sprite = sprite;

        this.gameObject.name = $"Player: '{player.NickName}'";
    }
}
