using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    float time = 0;
    private void OnTriggerStay(Collider other)
    {
        time += Time.deltaTime;
        if (time > 0.7f)
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                player.DeleteHP(20);
            }
            time = 0;
        }
    }
}
