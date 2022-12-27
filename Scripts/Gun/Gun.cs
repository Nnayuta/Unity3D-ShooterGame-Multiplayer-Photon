using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Gun : MonoBehaviourPunCallbacks
{
    public CharacterManager chaManager;

    public Object_Gun Obj_Gun;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public Animator anim;
    public AudioSource gunSound;
    public LayerMask Hitable;

    public bool isReloading;
    public bool podeAtirar = true;

    [Header("GUN")]
    public float Municao;
    public float MaxMunicao;

    public Text MunicaoTExt;

    private void Start()
    {
        Municao = Obj_Gun.Carregador;
        MaxMunicao = Obj_Gun.MaxCarregador;
        openMenu.MenuAberto(true);
    }

    private void Update()
    {

        if (photonView.IsMine)
        {
            MunicaoTExt.text = $"{Municao}/{MaxMunicao}";

            if (MaxMunicao <= 0)
            {
                Municao = Obj_Gun.Carregador;
                MaxMunicao = Obj_Gun.MaxCarregador;
                return;
            }

            if (Municao <= 0 && !isReloading)
            {
                StartCoroutine(reload());
                return;
            }

            if (Input.GetButtonDown("Fire1") && !isReloading)
            {
                Shoot();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                anim.SetTrigger("Visualizar");
            }
            if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            {
                StartCoroutine(reload());
            }
        }

    }

    IEnumerator reload()
    {
        Debug.Log("starting reload");
        anim.SetTrigger("Reload");
        isReloading = true;
        yield return new WaitForSeconds(1.5f);
        MaxMunicao -= Obj_Gun.Carregador;
        Municao = Obj_Gun.Carregador;
        isReloading = false;
        Debug.Log("ending reload");
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        anim.SetTrigger("Shoot");
        gunSound.Play();
        Municao--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, Hitable))
        {

            PhotonView Target = hit.transform.GetComponent<PhotonView>();

            if (Target != null)
            {
                Target.RPC("TakeDamage", RpcTarget.All, Obj_Gun.Damage);
            }


            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * Obj_Gun.impactForce);
            }


            GameObject impactGO = PhotonNetwork.Instantiate(Obj_Gun.ImpactEffectString, hit.point, Quaternion.LookRotation(hit.normal));
            impactGO.transform.parent = hit.transform;
            Destroy(impactGO, 3f);
        }
    }
}
