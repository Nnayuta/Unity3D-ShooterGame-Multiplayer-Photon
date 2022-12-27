using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Object_Character Character;
    public PhotonView pv;
    public GameObject CharUI;
    public GameObject CameraMiniMap;


    [Header("Player Status")]
    [SerializeField] private float Health;
    [SerializeField] private float Shield;
    [SerializeField] private Text Health_UI, ShieldUI;
    [SerializeField] private GameObject dieSceen;
    [SerializeField] private GameObject[] cameraDisable;

    [Header("Animação")]
    public Animator AnimatorController;

    [Header("Movimento: Player")]
    [SerializeField] private CharacterController CharacterController;
    private float x;
    private float z;
    private Vector3 velocity;

    [Header("Movimento: Ground")]
    public Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] public LayerMask groundMask;
    private bool isGrounded;

    public Character_Crossair charCross;

    public MenuSettings configMenu;
    public bool configMenuStatus;

    private void Start()
    {
        pv = GetComponent<PhotonView>();

        CharUI.SetActive(pv.IsMine);
        groundCheck.gameObject.SetActive(pv.IsMine);
        CameraMiniMap.SetActive(pv.IsMine);

        Health = Character.Health;
    }

    private void Update()
    {
        Movimento();
        UpdateHealthUI();

        if (pv.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ConfigMenu();
            }
        }
    }

    void UpdateHealthUI()
    {
        Health_UI.text = Health.ToString();
        ShieldUI.text = Shield.ToString();
    }

    public void Movimento()
    {
        if (pv.IsMine)
        {
            Controller();
            charCross.PlayerUpdateCross();
        }
        else
        {
            AnimatorController.SetFloat("MovZ", z);
            AnimatorController.SetFloat("MovX", x);
            AnimatorController.SetFloat("isJumping", velocity.y);
        }
    }

    [PunRPC]
    private void TakeDamage(float Damage)
    {
        if (pv.IsMine)
        {
            Health -= Damage;

            if (Health <= 0)
            {
                Health = 0;
                StartCoroutine(Die());
            }
        }
    }

    IEnumerator Die()
    {
        CharacterController.enabled = false;
        AnimatorController.SetBool("Dead", true);
        dieSceen.SetActive(true);
        cameraDisable[0].GetComponent<CharacterCamera>().enabled = false;
        cameraDisable[1].gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        Health = Character.Health;
        yield return new WaitForSeconds(2f);
        AnimatorController.SetBool("Dead", false);
        cameraDisable[0].GetComponent<CharacterCamera>().enabled = true;
        cameraDisable[1].gameObject.SetActive(true);
        transform.position = new Vector3(GameManager.Instancia._spawns[Random.Range(0, GameManager.Instancia._spawns.Length)].transform.position.x, GameManager.Instancia._spawns[Random.Range(0, GameManager.Instancia._spawns.Length)].transform.position.y, GameManager.Instancia._spawns[Random.Range(0, GameManager.Instancia._spawns.Length)].transform.position.z);
        dieSceen.SetActive(false);
        yield return new WaitForSeconds(1f);
        CharacterController.enabled = true;
    }

    void Controller()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        velocity.y += Character.gravity * Time.deltaTime;
        CharacterController.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            onMove(move, Character.ShiftSpeed);
        }
        else
        {
            onMove(move, Character.MovSpeed);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(Character.JumpHeight * -2f * Character.gravity);
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void onMove(Vector3 move, float speed)
    {
        CharacterController.Move(move * speed * Time.deltaTime);
    }


    public void ConfigMenu()
    {
        openMenu.MenuAberto(configMenu.menuStatus);
        configMenu.MenuConfig();
        CharacterController.enabled = !configMenu.menuStatus;
        cameraDisable[0].GetComponent<CharacterCamera>().enabled = !configMenu.menuStatus;
        cameraDisable[1].GetComponentInChildren<Gun>().enabled = !configMenu.menuStatus;
    }
}
