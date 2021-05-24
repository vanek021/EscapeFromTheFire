using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBehaviour : MonoBehaviour
{
    [SerializeField]
    protected GameObject cam;
    [SerializeField]
    protected KeyCode hotkey;
    public abstract void Interact();
    private void Update()
    {
        if (Input.GetKeyDown(hotkey))
        {
            Interact();
        }
    }
}
