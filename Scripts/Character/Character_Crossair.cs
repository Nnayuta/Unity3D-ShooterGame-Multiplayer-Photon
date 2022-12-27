using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Character_Crossair : MonoBehaviourPunCallbacks
{

    public RectTransform[] Crosshairs;
    public Outline[] outlineCross;

    [SerializeField] private Slider[] Sliders;
    [SerializeField] private Toggle[] Toogles;

    public Color linhaInternaCor;
    public Color contornoOpacidade;

    public PhotonView phview;


    public void updatecrosshair()
    {

            Sliders[5].value = PlayerPrefs.GetInt("ComprimentoLinhaInterna");
            Sliders[6].value = PlayerPrefs.GetInt("EspessuraLinhaInterna");
            Sliders[7].value = PlayerPrefs.GetInt("DeslocamentoLinhaInterna");
            Sliders[4].value = PlayerPrefs.GetFloat("OpacidadeLinhasInternas");
            Sliders[1].value = PlayerPrefs.GetFloat("EspessuraContorno");
            Sliders[0].value = PlayerPrefs.GetFloat("OpacidadeContorno");
            Toogles[0].enabled = NayPrefs.GetBool("Contorno");

    }

    public void PlayerUpdateCross()
    {

            Crosshairs[0].localScale = new Vector3(PlayerPrefs.GetInt("ComprimentoLinhaInterna"), Crosshairs[0].localScale.y, Crosshairs[0].localScale.z);
            Crosshairs[1].localScale = new Vector3(PlayerPrefs.GetInt("ComprimentoLinhaInterna"), Crosshairs[1].localScale.y, Crosshairs[1].localScale.z);
            Crosshairs[2].localScale = new Vector3(Crosshairs[2].localScale.x, PlayerPrefs.GetInt("ComprimentoLinhaInterna"), Crosshairs[2].localScale.z);
            Crosshairs[3].localScale = new Vector3(Crosshairs[3].localScale.x, PlayerPrefs.GetInt("ComprimentoLinhaInterna"), Crosshairs[3].localScale.z);


            Crosshairs[0].localScale = new Vector3(Crosshairs[0].localScale.x, PlayerPrefs.GetInt("EspessuraLinhaInterna"), Crosshairs[0].localScale.z);
            Crosshairs[1].localScale = new Vector3(Crosshairs[1].localScale.x, PlayerPrefs.GetInt("EspessuraLinhaInterna"), Crosshairs[1].localScale.z);
            Crosshairs[2].localScale = new Vector3(PlayerPrefs.GetInt("EspessuraLinhaInterna"), Crosshairs[2].localScale.y, Crosshairs[2].localScale.z);
            Crosshairs[3].localScale = new Vector3(PlayerPrefs.GetInt("EspessuraLinhaInterna"), Crosshairs[3].localScale.y, Crosshairs[3].localScale.z);


            Crosshairs[0].localPosition = new Vector3(-PlayerPrefs.GetInt("DeslocamentoLinhaInterna"), Crosshairs[0].localPosition.y, Crosshairs[0].localPosition.z);
            Crosshairs[1].localPosition = new Vector3(PlayerPrefs.GetInt("DeslocamentoLinhaInterna"), Crosshairs[1].localPosition.y, Crosshairs[1].localPosition.z);
            Crosshairs[2].localPosition = new Vector3(Crosshairs[2].localPosition.z, PlayerPrefs.GetInt("DeslocamentoLinhaInterna"), Crosshairs[2].localPosition.z);
            Crosshairs[3].localPosition = new Vector3(Crosshairs[3].localPosition.z, -PlayerPrefs.GetInt("DeslocamentoLinhaInterna"), Crosshairs[3].localPosition.z);

            linhaInternaCor.a = PlayerPrefs.GetFloat("OpacidadeLinhasInternas");

            Crosshairs[0].gameObject.GetComponentInChildren<Image>().color = linhaInternaCor;
            Crosshairs[1].gameObject.GetComponentInChildren<Image>().color = linhaInternaCor;
            Crosshairs[2].gameObject.GetComponentInChildren<Image>().color = linhaInternaCor;
            Crosshairs[3].gameObject.GetComponentInChildren<Image>().color = linhaInternaCor;

            outlineCross[0].effectDistance = new Vector2(PlayerPrefs.GetFloat("EspessuraContorno") / 4, -PlayerPrefs.GetFloat("EspessuraContorno") / 1.5f);
            outlineCross[1].effectDistance = new Vector2(PlayerPrefs.GetFloat("EspessuraContorno") / 4, -PlayerPrefs.GetFloat("EspessuraContorno") / 1.5f);
            outlineCross[2].effectDistance = new Vector2(-PlayerPrefs.GetFloat("EspessuraContorno") / 1.5f, PlayerPrefs.GetFloat("EspessuraContorno") / 4);
            outlineCross[3].effectDistance = new Vector2(-PlayerPrefs.GetFloat("EspessuraContorno") / 1.5f, PlayerPrefs.GetFloat("EspessuraContorno") / 4);

            contornoOpacidade.a = PlayerPrefs.GetFloat("OpacidadeContorno");
            outlineCross[0].effectColor = contornoOpacidade;
            outlineCross[1].effectColor = contornoOpacidade;
            outlineCross[2].effectColor = contornoOpacidade;
            outlineCross[3].effectColor = contornoOpacidade;

            outlineCross[0].enabled = NayPrefs.GetBool("Contorno");
            outlineCross[1].enabled = NayPrefs.GetBool("Contorno");
            outlineCross[2].enabled = NayPrefs.GetBool("Contorno");
            outlineCross[3].enabled = NayPrefs.GetBool("Contorno");
    }

    public void Update_ComprimentoLinhaInterna(float value)
    {
        PlayerPrefs.SetInt("ComprimentoLinhaInterna", (int)Mathf.Round(value));
        PlayerUpdateCross();
    }

    public void Update_EspessuraLinhaInterna(float value)
    {
        PlayerPrefs.SetInt("EspessuraLinhaInterna", (int)Mathf.Round(value));
        PlayerUpdateCross();
    }

    public void Update_DeslocamentoLinhaInterna(float value)
    {
        PlayerPrefs.SetInt("DeslocamentoLinhaInterna", Mathf.FloorToInt(value));
        PlayerUpdateCross();
    }

    public void Update_OpacidadeLinhasInternas(float value)
    {
        PlayerPrefs.SetFloat("OpacidadeLinhasInternas", value);
        PlayerUpdateCross();
    }

    public void Update_EspessuraContorno(float value)
    {
        PlayerPrefs.SetFloat("EspessuraContorno", value);
        PlayerUpdateCross();
    }

    public void Update_OpacidadeContorno(float value)
    {
        PlayerPrefs.SetFloat("OpacidadeContorno", value);
        PlayerUpdateCross();
    }

    public void Update_Contorno(bool value)
    {
        NayPrefs.SetBool("Contorno", value);
        PlayerUpdateCross();

    }

}
