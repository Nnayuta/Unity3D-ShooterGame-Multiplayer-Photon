using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuSoundsObject : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public MenuSounds Msound;
    public AudioSource uiSound;

    private void FixedUpdate()
    {
        Msound = GameObject.Find("MenuSounds").GetComponent<MenuSounds>();
        uiSound = gameObject.GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiSound.clip = Msound.OnEnter;
        uiSound.Play();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            uiSound.clip = Msound.onClick;
            uiSound.Play();
        }
    }
}
