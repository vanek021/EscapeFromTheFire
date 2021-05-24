using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : ItemBehaviour
{
    public override void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,3f))
        {
            if (hit.transform.tag == "Flame")
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
