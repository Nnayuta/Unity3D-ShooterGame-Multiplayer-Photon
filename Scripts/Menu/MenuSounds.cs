using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuSounds : MonoBehaviour
{
    public List<Button> UI_Buttons = new List<Button>();
    public AudioClip OnEnter, onClick;
    public AudioMixerGroup InterfaceMixer;


    private void Start()
    {
        for (int i = 0; i < UI_Buttons.Count; i++)
        {
            if (UI_Buttons[i].interactable == true)
            {
                addComponent(i);
            }
        }
    }

    void addComponent(int i)
    {
        UI_Buttons[i].gameObject.AddComponent<MenuSoundsObject>();
        UI_Buttons[i].gameObject.AddComponent<AudioSource>();

        UI_Buttons[i].gameObject.GetComponent<AudioSource>().outputAudioMixerGroup = InterfaceMixer;


    }
}
