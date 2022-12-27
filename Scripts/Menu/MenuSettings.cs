using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuSettings : MonoBehaviour
{
    [SerializeField] private GameObject UI_Config;
    public bool menuStatus;

    [SerializeField] private List<GameObject> menuBotoes = new List<GameObject>();

    [Header("Sensibilidade")]
    public InputField SensText;
    public Slider sliderSens;

    [Header("Volume")]
    public AudioMixer MixerMaster;
    public AudioMixerGroup MixerInterface;
    public AudioMixerGroup MixerMusic;
    public AudioMixerGroup MixerEffects;

    public List<Slider> MixersSlide = new List<Slider>();

    public Character_Crossair charCross;

    public void MenuConfig()
    {
        menuStatus = !menuStatus;
        UI_Config.SetActive(menuStatus);
    }

    public void MenuBotoes(string botao)
    {
        switch (botao)
        {
            case "Geral":
                menuBotoes[0].SetActive(true);
                menuBotoes[1].SetActive(false);
                menuBotoes[2].SetActive(false);
                break;
            case "Audio":
                menuBotoes[0].SetActive(false);
                menuBotoes[1].SetActive(true);
                menuBotoes[2].SetActive(false);
                break;

            case "Mira":
                menuBotoes[0].SetActive(false);
                menuBotoes[1].SetActive(false);
                menuBotoes[2].SetActive(true);
                break;
            case "Sair":
                Application.Quit();
                break;
        }
    }

    private void Start()
    {
        //Deixa a interface por padrao em geral
        menuBotoes[0].SetActive(true);
        menuBotoes[1].SetActive(false);
        menuBotoes[2].SetActive(false);

        //desabilitar a interface ao iniciar
        UI_Config.SetActive(false);

        //seta os valores salvos no playerprefs

        LoadSoundPlayerPrefs();
        LoadSens();
    }

    // Configurações de sensibilidade

    private void LoadSens()
    {
        sliderSens.value = PlayerPrefs.GetFloat("sensibilidade");
        SensText.text = PlayerPrefs.GetFloat("sensibilidade").ToString();
    } 

    public void changeSens(float sens)
    {
        SensText.text = sens.ToString();
        PlayerPrefs.SetFloat("sensibilidade", sens);
    }

    // Configurações de audio

    private void LoadSoundPlayerPrefs()
    {
        MixerMaster.SetFloat("VolMaster", PlayerPrefs.GetFloat("VolMaster"));
        MixerInterface.audioMixer.SetFloat("VolInter", PlayerPrefs.GetFloat("VolInter"));
        MixerMusic.audioMixer.SetFloat("VolMusic", PlayerPrefs.GetFloat("VolMusic"));
        MixerEffects.audioMixer.SetFloat("VolEffect", PlayerPrefs.GetFloat("VolEffect"));

        MixersSlide[0].value = PlayerPrefs.GetFloat("VolMaster");
        MixersSlide[1].value = PlayerPrefs.GetFloat("VolInter");
        MixersSlide[2].value = PlayerPrefs.GetFloat("VolMusic");
        MixersSlide[3].value = PlayerPrefs.GetFloat("VolEffect");

        charCross.updatecrosshair();

    }


    public void MasterVolume(float volume)
    {
        MixerMaster.SetFloat("VolMaster", volume);
        PlayerPrefs.SetFloat("VolMaster", volume);
    }

    public void InterfaceVolume(float volume)
    {
        MixerInterface.audioMixer.SetFloat("VolInter", volume);
        PlayerPrefs.SetFloat("VolInter", volume);
    }

    public void MusicVolume(float volume)
    {
        MixerMusic.audioMixer.SetFloat("VolMusic", volume);
        PlayerPrefs.SetFloat("VolMusic", volume);
    }

    public void EffectVolume(float volume)
    {
        MixerEffects.audioMixer.SetFloat("VolEffect", volume);
        PlayerPrefs.SetFloat("VolEffect", volume);
    }
}
