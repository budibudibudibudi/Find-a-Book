using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class playermovement : MonoBehaviour
{
    CharacterController controller;

    private float speed = 5;
    private float gravity = -9.8f;
    private float grounddistance = 0.4f;
    private float jumpheight = 3f;
    private bool isgrounded;
    public bool CanbeTarget;

    public AudioClip[] all;
    public GameObject[] ui;
    public Transform groundcheck;
    public LayerMask groundmask;
    public Slider healthbar;
    public Slider staminabar;
    [HideInInspector] public AudioSource audioa;
    AudioSource audiob;
    Vector3 velocity;
    Rigidbody rb;
    [SerializeField] TextMeshProUGUI nama;
    [HideInInspector] public PhotonView view;
    public GameObject crown;

    private float health;
    private float stamina;
    private float regen_stamina;
    private float reduce_stamina;
    private bool canrun = false;
    private void Awake()
    {
        view = GetComponent<PhotonView>();
        PhotonNetwork.NickName = PlayerPrefs.GetString("Player_Name");
        if (!view.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            foreach (var item in ui)
            {
                Destroy(item);
            }
        }
        else
        {
            healthbar.value = health;
            staminabar.value = stamina;
        }

        view.TransferOwnership(PhotonNetwork.LocalPlayer);
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioa = gameObject.AddComponent<AudioSource>();
        audiob = gameObject.AddComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        health = 100;
        stamina = 10;
        regen_stamina = 1;
        reduce_stamina = 2;
        CanbeTarget = false;
        nama.text = PhotonNetwork.NickName;
    }



    private void FixedUpdate()
    {
        //staminabar.value = stamina;
        if (view.IsMine)
        {
            bergerak();
            staminabar.value = stamina;
            healthbar.value = health;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f))
            {
                if (hit.collider.tag == "hantu")
                {
                    if (audiob.isPlaying)
                        return;
                    audiob.PlayOneShot(all[0]);
                }
            }
            rb.velocity = Vector3.zero;


            if (stamina > 0)
                canrun = true;
            else if (stamina <= 0)
            {
                canrun = false;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (canrun)
                {
                    speed = 10;
                    stamina = Mathf.Clamp(stamina - reduce_stamina * Time.deltaTime, 0, 10);
                }
                else
                {
                    speed = 5;
                }
            }
            else
            {
                speed = 5;
                stamina = Mathf.Clamp(stamina + regen_stamina * Time.deltaTime, 0, 10);
            }

        }
        else
            return;
    }

    public void takedamage(float amount)
    {
        view.RPC("changehealth", RpcTarget.All, amount);
    }
    void bergerak()
    {
        isgrounded = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);
        if (isgrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * y;
        controller.Move(move * speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump"))
            jump();
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        if (x > 0f || x < 0 || y > 0 || y < 0)
        {
            if (!isgrounded)
                if (audioa.isPlaying)
                    audioa.Stop();
                else
                    return;
            else
                if (audioa.isPlaying)
                return;
            audioa.PlayOneShot(all[1]);
            audioa.loop = true;
        }
        else
        {
            audioa.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (view.IsMine)
        {
            if (collision.gameObject.tag == "batas")
            {
                death();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (view.IsMine)
        {
            if (other.gameObject.tag == "buku")
            {
                CanbeTarget = true;
                crown.SetActive(true);
                Destroy(other.gameObject);
            }
        }
    }
    [PunRPC]
    public void changehealth(float amount)
    {
        if(!view.IsMine)
            return;
        health = Mathf.Clamp(health + amount, 0, 100);
        healthbar.value = health;
        if(health <= 0)
        {
            death();
        }
    }
    public void jump()
    {
        if(isgrounded)
            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
    }

    public void death()
    {
        if (!view.IsMine)
            return;
        FindObjectOfType<spawnplayer>().bangkit(this.gameObject);
        if (CanbeTarget)
        {
            PhotonNetwork.Instantiate(FindObjectOfType<gamemanagerscript>().buku.name, this.transform.position, Quaternion.identity);
        }
        else
            return;
        Destroy(gameObject);
    }
}
