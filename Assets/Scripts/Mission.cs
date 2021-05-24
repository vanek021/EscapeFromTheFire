using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Mission : MonoBehaviour
{
    public string message;
    public UnityEvent OnActivate;
    public UnityEvent OnEntered;
    public Mission nextmission;
    Player own;
    public Player owner { get { return own; } set
        {
            own = value;
            OnActivate.AddListener(() => { DrawMessage(); });
            OnActivate.Invoke();
        }
    }
    private void Awake()
    {
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnEntered.Invoke();
            if (nextmission == null)
                return;
            owner.currentmission = nextmission;
            nextmission.owner = owner;
        }
    }
    public void DrawMessage()
    {
        GameObject.FindGameObjectWithTag("message").GetComponent<Text>().text = message;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        Application.LoadLevel("Menu");       
    }
}
