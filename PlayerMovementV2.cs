using UnityEngine;
using System.Collections;

public class PlayerMovementV2 : MonoBehaviour
{
    public float Speed;
    public Animator anim;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (v != 0 || h != 0)
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

        Vector3 movement = new Vector3(h, rb.velocity.y, v) * Speed;

        rb.AddForce(movement, ForceMode.VelocityChange);
    }
}