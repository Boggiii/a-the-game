using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Animator anim;

    private Rigidbody rb;

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if(v != 0 || h != 0)
        {
            if (v == -1)
            {
                anim.SetBool("Running", false);
                anim.SetBool("RunningReverse", true);
            }
            else
            {
                anim.SetBool("RunningReverse", false);
                anim.SetBool("Running", true);
            }
        }
        else
        {
            anim.SetBool("Running", false);
            anim.SetBool("RunningReverse", false);
        }

        Vector3 movement = new Vector3(h, 0f, v) * speed * Time.deltaTime;

        transform.Translate(movement, Space.Self);

    }
}
