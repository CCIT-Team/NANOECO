using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Character
{
    public Rigidbody rigid;
    Animator ani;
    public float dash_force;
    bool isdash = false;
   // public Camera maincamera;

    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        is_jump = false;
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
        MouseRotation();
    }
    void Jump()
    {
        float jump = Input.GetAxisRaw("Jump");
        rigid.AddForce(Vector3.up * jump * jump_force);
        if (jump > 0)  { is_jump = true;}
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
    void MouseRotation()
    {
        ////���� ����� ���� ���콺�� ���� ������Ʈ�� ������ ��ǥ�� �ӽ÷� �����մϴ�.
        //Vector3 mPosition = Input.mousePosition; //���콺 ��ǥ ����
        //Vector3 oPosition = transform.position; //���� ������Ʈ ��ǥ ����

        ////ī�޶� �ո鿡�� �ڷ� ���� �ֱ� ������, ���콺 position�� z�� ������ 
        ////���� ������Ʈ�� ī�޶���� z���� ���̸� �Է½������ �մϴ�.
        //mPosition.y = oPosition.y - maincamera.transform.position.y;

        ////ȭ���� �ȼ����� ��ȭ�Ǵ� ���콺�� ��ǥ�� ����Ƽ�� ��ǥ�� ��ȭ�� ��� �մϴ�.
        ////�׷���, ��ġ�� ã�ư� �� �ְڽ��ϴ�.
        //Vector3 target = maincamera.ScreenToWorldPoint(mPosition);

        ////������ ��ũź��Ʈ(arctan, ��ź��Ʈ)�� ���� ������Ʈ�� ��ǥ�� ���콺 ����Ʈ�� ��ǥ��
        ////�̿��Ͽ� ������ ���� ��, ���Ϸ�(Euler)ȸ�� �Լ��� ����Ͽ� ���� ������Ʈ�� ȸ����Ű��
        ////����, �� ���� �Ÿ����� ���� �� ���Ϸ� ȸ���Լ��� �����ŵ�ϴ�.

        ////�켱 �� ���� �Ÿ��� ����Ͽ�, dy, dx�� ������ �Ӵϴ�.
        //float dz = target.z - oPosition.z;
        //float dx = target.x - oPosition.x;

        ////������ ȸ�� �Լ��� 0���� 180 �Ǵ� 0���� -180�� ������ �Է� �޴µ� ���Ͽ�
        ////(���� 270�� ���� ���� �Էµ� ���� ���������ϴ�.) ��ũź��Ʈ Atan2()�Լ��� ��� ���� 
        ////���� ��(180���� ����(3.141592654...)��)���� ��µǹǷ�
        ////���� ���� ������ ��ȭ�ϱ� ���� Rad2Deg�� �����־�� ������ �˴ϴ�.
        //float rotateDegree = Mathf.Atan2(dz, dx) * Mathf.Rad2Deg;

        ////������ ������ ���Ϸ� ȸ�� �Լ��� �����Ͽ� z���� �������� ���� ������Ʈ�� ȸ����ŵ�ϴ�.
        //transform.rotation = Quaternion.Euler(0f, rotateDegree, 0f);
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
