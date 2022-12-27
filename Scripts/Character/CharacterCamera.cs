using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterCamera : MonoBehaviourPunCallbacks
{
    [SerializeField] private CharacterManager CM;

    [Header("GameObjects")]

    [SerializeField] private GameObject Weapons;
    [SerializeField] private GameObject[] AllBody;

    [SerializeField] private Transform AllBodyChest;
    [SerializeField] private Transform Corpo;

    [Header("Variaveis da Camera")]

    [SerializeField] private float xRotate = 0f;

    private void Start()
    {
        this.gameObject.GetComponent<Camera>().enabled = CM.pv.IsMine;
        this.gameObject.GetComponent<AudioListener>().enabled = CM.pv.IsMine;
        AllBody[0].SetActive(!CM.pv.IsMine);
        AllBody[1].SetActive(!CM.pv.IsMine);
        AllBody[2].SetActive(!CM.pv.IsMine);
        Weapons.SetActive(CM.pv.IsMine);
    }

    private void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        if (CM.pv.IsMine)
        {
            float mouseX = Input.GetAxis("Mouse X") * PlayerPrefs.GetFloat("sensibilidade");
            float mouseY = Input.GetAxis("Mouse Y") * PlayerPrefs.GetFloat("sensibilidade");

            xRotate -= mouseY;
            transform.localRotation = Quaternion.Euler(xRotate, 0f, 0f);

            Corpo.Rotate(Vector3.up * mouseX);

        }

        AllBodyChest.localRotation = Quaternion.Euler(xRotate, 0f, 0f);

    }
}
