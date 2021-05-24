using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public CharacterController controller;
    public Pointer startPoint;
    float gravity = -1f;
    float ySpeed = 0;
    private void Update()
    {
        if (DataHolder.isHintsActive) return;
        if (controller.isGrounded)
        {
            ySpeed = gravity * Time.deltaTime;
        }
        else ySpeed += gravity * Time.deltaTime;
        Vector3 direction = startPoint.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), 10f);
        direction.Normalize();
        direction *= 0.15f;
        direction.y = ySpeed;
        controller.Move(direction);
        if (Vector3.Distance(transform.position, startPoint.transform.position) < 1.6f)
        {
            Lerp lp;
            var val = startPoint.SelectPath(out lp);
            if (val != null)
            {
                startPoint = val;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
