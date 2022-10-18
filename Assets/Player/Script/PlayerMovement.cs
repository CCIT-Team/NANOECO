using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Character
{
    public Rigidbody rigid;
    Animator ani;
    public float dash_force;
    bool isdash = false;
    public float jump_force;
    bool isjump = false;
   // public Camera maincamera;

    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }


    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
       
        if (horizontal > 0 || horizontal < 0 || vertical > 0 || vertical < 0)
        {
            ani.SetBool("Run", true);
        }
        else
        {
            ani.SetBool("Run", false);
        }
        Vector3 move = new Vector3(-horizontal * move_speed, 0f, -vertical * move_speed);
        rigid.velocity = move;
        Jump();
        Dash();
    }
    void Jump()
    {
        float jump = Input.GetAxisRaw("Jump");
        rigid.AddForce(Vector3.up * jump * jump_force);
        if (jump > 0)  { isjump = true;}
    }
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rigid.AddForce(Vector3.forward * dash_force);
            isdash = true;
        }
        else { isdash = false; }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Monster") { current_hp -= col.gameObject.GetComponent<Character>().damage;}
    }
}

public enum state
{
    IDLE,
    RUN,
    DASH,
    JUMP,




}
public class Wepon : PlayerMovement
{

}
