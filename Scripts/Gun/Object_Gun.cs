using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Character/New Gun")]
public class Object_Gun : ScriptableObject
{
    [Header("Gun Info")]
    public int ID;
    public string Name;
    public int Price;
    public enum WeaponType { Leve, Pesada }
    public WeaponType TipoDeArma;

    [Header("Gun Status")]
    public float Damage;
    public float Cadencia;
    public float VelocidadeDeCorrida;
    public float VelocidadeParaEquipar;
    public float DispersaoTiroInicial;
    public float VelocidadeParaRecarregar;
    public float Carregador;
    public float MaxCarregador;
    public float impactForce;

    
    [Header("Gun Prefab")]
    public GameObject[] WeaponObject;
    public GameObject ImpactEffect;
    public string ImpactEffectString;
}
