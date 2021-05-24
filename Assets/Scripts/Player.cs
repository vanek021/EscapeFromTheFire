using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Mission currentmission;
    public Inventory inventory;
    CharacterController controller;
    [SerializeField]
    Camera cam;
    float rotx;
    float roty;
    public float sensitivity = 1f;
    public float speed = 5;
    public float gravityspeed = -1f;
    float ySpeed = 0;
    public float jumpspeed = 20;
    public GameObject hints;
    public GameObject audioSource;
    public GameObject hintFlame;
    float hp = 100;

    private void Start()
    {
        if (DataHolder.isLearnEnabled)
            DataHolder.isHintsActive = true;
        else 
        {
            hints.SetActive(false);
            DataHolder.isHintsActive = false;
        }
        if (DataHolder.isHintsActive)
        {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        }
        if (DataHolder.volume != audioSource.GetComponent<AudioSource>().volume)
            audioSource.GetComponent<AudioSource>().volume = DataHolder.volume;
        if (currentmission != null)
            currentmission.owner = this;
        controller = GetComponent<CharacterController>();
    }
    public Image img;
    void UpdateLabel()
    {
        img.fillAmount = hp/100f;
    }
    public void DeleteHP(int hp)
    {
        this.hp -= hp;
        UpdateLabel();
        if (this.hp <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
            return;
        }
    }
    void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,5f))
            {
                if (hit.transform.tag == "Item")
                {
                    inventory.AddItem(hit.transform.GetComponent<SpecificItem>().item);
                    Destroy(hit.transform.gameObject);
                    OpenHints();
                    hintFlame.SetActive(true);
                }
            }
        }
    }
    void Look()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rotx += Input.GetAxis("Mouse X") * sensitivity;
        roty -= Input.GetAxis("Mouse Y") * sensitivity;
        transform.localEulerAngles = new Vector3(0, rotx, 0);
        if (roty > 60)
            roty = 60;
        else if (roty < -60)
            roty = -60;
        float forward = Input.GetAxis("Horizontal");
        float right = Input.GetAxis("Vertical");
        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = 3f * Time.deltaTime;
            }
            else
            {
                ySpeed = gravityspeed * Time.deltaTime;
            }
        }
        else
        {
            ySpeed += gravityspeed * Time.deltaTime;
        }
        float run = 2f;
        if (Input.GetKey(KeyCode.LeftShift))
            run = 3f;
        Vector3 movement = new Vector3(forward * Time.deltaTime, ySpeed, right * Time.deltaTime* run) * speed;
        controller.Move(transform.rotation * movement);
        cam.transform.localEulerAngles = new Vector3(roty, 0, 0);
    }
    private void Update()
    {
        if (!DataHolder.isHintsActive)
        {
            Look();
            PickUp();
        }
    }

    public void CloseHints()
    {
        DataHolder.isHintsActive = false;
        Time.timeScale = 1;
    }

    public void OpenHints() 
    {
        DataHolder.isHintsActive = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void OpenForm() 
    {
        string url = "https://docs.google.com/forms/d/1PfSQViI2U4NZRX1hw-egN9OmLMwycqHNY9yehEFwTZI/viewform?edit_requested=true";
        Process.Start(url);
    }
}
