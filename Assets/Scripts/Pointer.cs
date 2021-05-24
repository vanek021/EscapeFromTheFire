using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Lerp
{
    Linear = 0,
    Constant = 1
}

public class Pointer : MonoBehaviour
{
    public GameObject[] next = new GameObject[1];
    public Lerp lerp = Lerp.Linear;
    public Pointer SelectPath(out Lerp lerp)
    {
        lerp = this.lerp;
        var g = next[Random.Range(0, next.Length)];
        if (g != null)
            return g.GetComponent<Pointer>();
        return null;
    }
}
