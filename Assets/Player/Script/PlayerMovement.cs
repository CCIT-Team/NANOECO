using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Character
{
    [Header("Status")]
    public float hp = 100;
    public float moveSpeed = 50;
    public float jumpforce = 10;
    bool jumpstate = false;
    public Rigidbody rigid;
    Animator ani;

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
        float jump = Input.GetAxisRaw("Jump");
        if (jump > 0)
        {
            jumpstate = true;
        }
        if (horizontal > 0 || horizontal < 0 || vertical > 0 || vertical < 0)
        {
            ani.SetBool("Run", true);
        }
        else
        {
            ani.SetBool("Run", false);
        }
        Vector3 move = new Vector3(-horizontal * moveSpeed, 0f, -vertical * moveSpeed);
        rigid.velocity = move;
        rigid.AddForce(Vector3.up * jump * jumpforce);


        MouseRotation();
    }

    void MouseRotation()
    {
        float speed = 3;
        //Vector3 mousePos = Input.mousePosition;
        //Vector3 playerPos = transform.position;
        //mousePos.z = playerPos.z - Camera.main.transform.position.z;
        //Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);

        //float dx = target.x - playerPos.x;
        //float dy = target.y - playerPos.y;

        //float rotateDegree = Mathf.Atan2(dx, dy) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Euler(0f, rotateDegree, 0f);
        transform.Rotate(0f, Input.GetAxis("Mouse X") * speed, 0f, Space.World);
        transform.Rotate(0f, -Input.GetAxis("Mouse Y") * speed, 0f, Space.World);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Monster")
        {
            current_hp -= col.gameObject.GetComponent<Character>().damage;
        }
    }
}



public enum state
{
    IDLE,
        RUN,
        DASH,
        JUMP,




}
public class Wepon:PlayerMovement
{
    
}
