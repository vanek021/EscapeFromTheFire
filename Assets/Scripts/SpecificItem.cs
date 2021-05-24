using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificItem : MonoBehaviour
{
    private void Start()
    {
        transform.tag = "Item";
    }
    public Item item;
}

